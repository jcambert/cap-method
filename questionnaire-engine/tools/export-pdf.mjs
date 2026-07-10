import fs from 'node:fs';
import path from 'node:path';
import zlib from 'node:zlib';

const defaultPackageFolder = 'questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session';
const packageFolder = process.argv[2] ?? defaultPackageFolder;
const manifestPath = path.join(packageFolder, 'manifest.json');
const manifest = readJson(manifestPath);

const exportsFolder = path.join(packageFolder, 'exports');
fs.mkdirSync(exportsFolder, { recursive: true });

const finalSynthesisSource = manifest.files?.source?.finalSynthesis;
const actionPlanSource = manifest.files?.source?.actionPlan;

const finalSynthesisPdf = path.join(exportsFolder, 'CAP-SYNTHESE-FINALE.pdf');
const actionPlanPdf = path.join(exportsFolder, 'CAP-PLAN-ACTION.pdf');

writePdf(finalSynthesisPdf, 'Synthese finale CAP', fs.readFileSync(finalSynthesisSource, 'utf8'));
writePdf(actionPlanPdf, 'Plan d action CAP', fs.readFileSync(actionPlanSource, 'utf8'));

manifest.files.exports.finalSynthesisPdf = finalSynthesisPdf;
manifest.files.exports.actionPlanPdf = actionPlanPdf;
manifest.checks.pdfGenerated = true;
manifest.lastPdfExportedAt = new Date().toISOString();

fs.writeFileSync(manifestPath, JSON.stringify(manifest, null, 2), 'utf8');

console.log('PDF exports generated');
console.log(`Final synthesis PDF: ${finalSynthesisPdf}`);
console.log(`Action plan PDF: ${actionPlanPdf}`);

function writePdf(outputPath, title, markdown) {
  const lines = renderLines(title, markdown);
  const pages = paginate(lines, 42);
  const objects = [];
  const pageObjectIds = [];

  const catalogId = addObject(objects, '');
  const pagesId = addObject(objects, '');
  const fontId = addObject(objects, '<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>');

  for (const pageLines of pages) {
    const content = renderPageContent(pageLines);
    const contentBuffer = Buffer.from(content, 'utf8');
    const compressed = zlib.deflateSync(contentBuffer);
    const contentId = addObject(objects, `<< /Length ${compressed.length} /Filter /FlateDecode >>\nstream\n${compressed.toString('binary')}\nendstream`, true);
    const pageId = addObject(objects, `<< /Type /Page /Parent ${pagesId} 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 ${fontId} 0 R >> >> /Contents ${contentId} 0 R >>`);
    pageObjectIds.push(pageId);
  }

  objects[catalogId - 1].content = `<< /Type /Catalog /Pages ${pagesId} 0 R >>`;
  objects[pagesId - 1].content = `<< /Type /Pages /Kids [${pageObjectIds.map(id => `${id} 0 R`).join(' ')}] /Count ${pageObjectIds.length} >>`;

  fs.writeFileSync(outputPath, buildPdf(objects));
}

function renderLines(title, markdown) {
  const lines = [title, ''];
  for (const rawLine of markdown.split(/\r?\n/)) {
    const line = rawLine.trim();
    if (line.length === 0) {
      lines.push('');
      continue;
    }
    if (line.startsWith('<details>') || line.startsWith('</details>') || line.startsWith('<summary>') || line.startsWith('```')) {
      continue;
    }
    if (line.startsWith('# ')) {
      lines.push('', line.slice(2).toUpperCase(), '');
      continue;
    }
    if (line.startsWith('## ')) {
      lines.push('', line.slice(3), '');
      continue;
    }
    if (line.startsWith('### ')) {
      lines.push(line.slice(4));
      continue;
    }
    if (line.startsWith('- [ ] ')) {
      lines.push(`[ ] ${line.slice(6)}`);
      continue;
    }
    if (line.startsWith('- ')) {
      lines.push(`- ${line.slice(2)}`);
      continue;
    }
    if (line.startsWith('> ')) {
      lines.push(line.slice(2));
      continue;
    }
    lines.push(line);
  }

  return lines.flatMap(line => wrapLine(toPdfSafeText(line), 92));
}

function paginate(lines, maxLines) {
  const pages = [];
  for (let index = 0; index < lines.length; index += maxLines) {
    pages.push(lines.slice(index, index + maxLines));
  }
  return pages.length > 0 ? pages : [[]];
}

function wrapLine(line, maxLength) {
  if (line.length <= maxLength) {
    return [line];
  }

  const words = line.split(' ');
  const result = [];
  let current = '';

  for (const word of words) {
    const candidate = current.length === 0 ? word : `${current} ${word}`;
    if (candidate.length > maxLength) {
      if (current.length > 0) {
        result.push(current);
      }
      current = word;
    } else {
      current = candidate;
    }
  }

  if (current.length > 0) {
    result.push(current);
  }

  return result;
}

function renderPageContent(lines) {
  const operations = ['BT', '/F1 10 Tf', '50 790 Td', '14 TL'];
  for (const line of lines) {
    operations.push(`(${escapePdfText(line)}) Tj`, 'T*');
  }
  operations.push('ET');
  return operations.join('\n');
}

function buildPdf(objects) {
  const chunks = [Buffer.from('%PDF-1.4\n%CAP\n', 'binary')];
  const offsets = [0];
  let offset = chunks[0].length;

  for (const object of objects) {
    offsets.push(offset);
    const header = Buffer.from(`${object.id} 0 obj\n`, 'binary');
    const body = object.binary ? Buffer.from(object.content, 'binary') : Buffer.from(object.content, 'utf8');
    const footer = Buffer.from('\nendobj\n', 'binary');
    chunks.push(header, body, footer);
    offset += header.length + body.length + footer.length;
  }

  const xrefOffset = offset;
  const xrefLines = ['xref', `0 ${objects.length + 1}`, '0000000000 65535 f '];
  for (let index = 1; index < offsets.length; index += 1) {
    xrefLines.push(`${String(offsets[index]).padStart(10, '0')} 00000 n `);
  }
  const trailer = [
    ...xrefLines,
    'trailer',
    `<< /Size ${objects.length + 1} /Root 1 0 R >>`,
    'startxref',
    String(xrefOffset),
    '%%EOF',
    ''
  ].join('\n');
  chunks.push(Buffer.from(trailer, 'binary'));

  return Buffer.concat(chunks);
}

function addObject(objects, content, binary = false) {
  const id = objects.length + 1;
  objects.push({ id, content, binary });
  return id;
}

function toPdfSafeText(value) {
  return String(value ?? '')
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .replace(/[“”]/g, '"')
    .replace(/[‘’]/g, "'")
    .replace(/[–—]/g, '-')
    .replace(/[^\x20-\x7E]/g, '');
}

function escapePdfText(value) {
  return String(value ?? '')
    .replaceAll('\\', '\\\\')
    .replaceAll('(', '\\(')
    .replaceAll(')', '\\)');
}

function readJson(filePath) {
  if (!fs.existsSync(filePath)) {
    throw new Error(`Missing manifest: ${filePath}`);
  }

  return JSON.parse(fs.readFileSync(filePath, 'utf8'));
}
