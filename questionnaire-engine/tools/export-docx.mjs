import fs from 'node:fs';
import path from 'node:path';
import zlib from 'node:zlib';

const crcTable = buildCrcTable();

const defaultPackageFolder = 'questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session';
const packageFolder = process.argv[2] ?? defaultPackageFolder;
const manifestPath = path.join(packageFolder, 'manifest.json');
const manifest = readJson(manifestPath);

const exportsFolder = path.join(packageFolder, 'exports');
fs.mkdirSync(exportsFolder, { recursive: true });

const finalSynthesisSource = manifest.files?.source?.finalSynthesis;
const actionPlanSource = manifest.files?.source?.actionPlan;

const finalSynthesisDocx = path.join(exportsFolder, 'CAP-SYNTHESE-FINALE.docx');
const actionPlanDocx = path.join(exportsFolder, 'CAP-PLAN-ACTION.docx');

writeDocx(finalSynthesisDocx, 'Synthèse finale CAP', fs.readFileSync(finalSynthesisSource, 'utf8'));
writeDocx(actionPlanDocx, 'Plan d action CAP', fs.readFileSync(actionPlanSource, 'utf8'));

manifest.files.exports.finalSynthesisDocx = finalSynthesisDocx;
manifest.files.exports.actionPlanDocx = actionPlanDocx;
manifest.checks.exportsGenerated = true;
manifest.generatedAt = manifest.generatedAt ?? new Date().toISOString();
manifest.lastExportedAt = new Date().toISOString();

fs.writeFileSync(manifestPath, JSON.stringify(manifest, null, 2), 'utf8');

console.log('DOCX exports generated');
console.log(`Final synthesis DOCX: ${finalSynthesisDocx}`);
console.log(`Action plan DOCX: ${actionPlanDocx}`);

function writeDocx(outputPath, title, markdown) {
  const documentXml = renderDocumentXml(title, markdown);
  const entries = [
    {
      path: '[Content_Types].xml',
      content: xml(`<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">
  <Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>
  <Default Extension="xml" ContentType="application/xml"/>
  <Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"/>
  <Override PartName="/docProps/core.xml" ContentType="application/vnd.openxmlformats-package.core-properties+xml"/>
  <Override PartName="/docProps/app.xml" ContentType="application/vnd.openxmlformats-officedocument.extended-properties+xml"/>
</Types>`)
    },
    {
      path: '_rels/.rels',
      content: xml(`<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
  <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/>
  <Relationship Id="rId2" Type="http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties" Target="docProps/core.xml"/>
  <Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties" Target="docProps/app.xml"/>
</Relationships>`)
    },
    {
      path: 'word/document.xml',
      content: xml(documentXml)
    },
    {
      path: 'docProps/core.xml',
      content: xml(renderCoreXml(title))
    },
    {
      path: 'docProps/app.xml',
      content: xml(`<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Properties xmlns="http://schemas.openxmlformats.org/officeDocument/2006/extended-properties" xmlns:vt="http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes">
  <Application>CAP Method</Application>
</Properties>`)
    }
  ];

  fs.writeFileSync(outputPath, createZip(entries));
}

function renderDocumentXml(title, markdown) {
  const body = [
    paragraph(title, 'Title'),
    ...markdownToParagraphs(markdown),
    sectionProperties()
  ].join('');

  return `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<w:document xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
  <w:body>${body}</w:body>
</w:document>`;
}

function markdownToParagraphs(markdown) {
  return markdown
    .split(/\r?\n/)
    .filter(line => !line.trim().startsWith('<details>') && !line.trim().startsWith('</details>') && !line.trim().startsWith('<summary>'))
    .map(line => line.trim())
    .filter(line => line.length > 0)
    .map(line => {
      if (line.startsWith('# ')) {
        return paragraph(line.slice(2), 'Heading1');
      }
      if (line.startsWith('## ')) {
        return paragraph(line.slice(3), 'Heading2');
      }
      if (line.startsWith('### ')) {
        return paragraph(line.slice(4), 'Heading3');
      }
      if (line.startsWith('- [ ] ')) {
        return paragraph(`☐ ${line.slice(6)}`, null);
      }
      if (line.startsWith('- ')) {
        return paragraph(`• ${line.slice(2)}`, null);
      }
      if (line.startsWith('|')) {
        return paragraph(line, null);
      }
      if (line.startsWith('```')) {
        return paragraph(line, null);
      }
      if (line.startsWith('> ')) {
        return paragraph(line.slice(2), null);
      }
      return paragraph(line, null);
    });
}

function paragraph(text, style) {
  const styleXml = style ? `<w:pPr><w:pStyle w:val="${style}"/></w:pPr>` : '';
  return `<w:p>${styleXml}<w:r><w:t xml:space="preserve">${escapeXml(text)}</w:t></w:r></w:p>`;
}

function sectionProperties() {
  return `<w:sectPr><w:pgSz w:w="11906" w:h="16838"/><w:pgMar w:top="1440" w:right="1440" w:bottom="1440" w:left="1440" w:header="720" w:footer="720" w:gutter="0"/></w:sectPr>`;
}

function renderCoreXml(title) {
  const now = new Date().toISOString();
  return `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<cp:coreProperties xmlns:cp="http://schemas.openxmlformats.org/package/2006/metadata/core-properties" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:dcterms="http://purl.org/dc/terms/" xmlns:dcmitype="http://purl.org/dc/dcmitype/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <dc:title>${escapeXml(title)}</dc:title>
  <dc:creator>CAP Method</dc:creator>
  <cp:lastModifiedBy>CAP Method</cp:lastModifiedBy>
  <dcterms:created xsi:type="dcterms:W3CDTF">${now}</dcterms:created>
  <dcterms:modified xsi:type="dcterms:W3CDTF">${now}</dcterms:modified>
</cp:coreProperties>`;
}

function createZip(entries) {
  const fileRecords = [];
  const centralRecords = [];
  let offset = 0;

  for (const entry of entries) {
    const name = Buffer.from(entry.path, 'utf8');
    const data = Buffer.isBuffer(entry.content) ? entry.content : Buffer.from(entry.content, 'utf8');
    const compressed = zlib.deflateRawSync(data);
    const crc = crc32(data);
    const localHeader = Buffer.alloc(30);

    localHeader.writeUInt32LE(0x04034b50, 0);
    localHeader.writeUInt16LE(20, 4);
    localHeader.writeUInt16LE(0, 6);
    localHeader.writeUInt16LE(8, 8);
    localHeader.writeUInt16LE(0, 10);
    localHeader.writeUInt16LE(0, 12);
    localHeader.writeUInt32LE(crc, 14);
    localHeader.writeUInt32LE(compressed.length, 18);
    localHeader.writeUInt32LE(data.length, 22);
    localHeader.writeUInt16LE(name.length, 26);
    localHeader.writeUInt16LE(0, 28);

    fileRecords.push(localHeader, name, compressed);

    const centralHeader = Buffer.alloc(46);
    centralHeader.writeUInt32LE(0x02014b50, 0);
    centralHeader.writeUInt16LE(20, 4);
    centralHeader.writeUInt16LE(20, 6);
    centralHeader.writeUInt16LE(0, 8);
    centralHeader.writeUInt16LE(8, 10);
    centralHeader.writeUInt16LE(0, 12);
    centralHeader.writeUInt16LE(0, 14);
    centralHeader.writeUInt32LE(crc, 16);
    centralHeader.writeUInt32LE(compressed.length, 20);
    centralHeader.writeUInt32LE(data.length, 24);
    centralHeader.writeUInt16LE(name.length, 28);
    centralHeader.writeUInt16LE(0, 30);
    centralHeader.writeUInt16LE(0, 32);
    centralHeader.writeUInt16LE(0, 34);
    centralHeader.writeUInt16LE(0, 36);
    centralHeader.writeUInt32LE(0, 38);
    centralHeader.writeUInt32LE(offset, 42);

    centralRecords.push(centralHeader, name);
    offset += localHeader.length + name.length + compressed.length;
  }

  const centralDirectory = Buffer.concat(centralRecords);
  const endRecord = Buffer.alloc(22);
  endRecord.writeUInt32LE(0x06054b50, 0);
  endRecord.writeUInt16LE(0, 4);
  endRecord.writeUInt16LE(0, 6);
  endRecord.writeUInt16LE(entries.length, 8);
  endRecord.writeUInt16LE(entries.length, 10);
  endRecord.writeUInt32LE(centralDirectory.length, 12);
  endRecord.writeUInt32LE(offset, 16);
  endRecord.writeUInt16LE(0, 20);

  return Buffer.concat([...fileRecords, centralDirectory, endRecord]);
}

function crc32(buffer) {
  let crc = 0xffffffff;
  for (const byte of buffer) {
    crc = (crc >>> 8) ^ crcTable[(crc ^ byte) & 0xff];
  }
  return (crc ^ 0xffffffff) >>> 0;
}

function buildCrcTable() {
  return Array.from({ length: 256 }, (_, index) => {
    let value = index;
    for (let bit = 0; bit < 8; bit += 1) {
      value = value & 1 ? 0xedb88320 ^ (value >>> 1) : value >>> 1;
    }
    return value >>> 0;
  });
}

function xml(value) {
  return Buffer.from(value, 'utf8');
}

function escapeXml(value) {
  return String(value ?? '')
    .replaceAll('&', '&amp;')
    .replaceAll('<', '&lt;')
    .replaceAll('>', '&gt;')
    .replaceAll('"', '&quot;')
    .replaceAll("'", '&apos;');
}

function readJson(filePath) {
  if (!fs.existsSync(filePath)) {
    throw new Error(`Missing manifest: ${filePath}`);
  }

  return JSON.parse(fs.readFileSync(filePath, 'utf8'));
}
