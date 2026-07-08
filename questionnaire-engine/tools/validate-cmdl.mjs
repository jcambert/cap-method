import fs from 'node:fs';
import path from 'node:path';

const root = process.argv[2] ?? 'questionnaire-engine/cmdl/examples';
const supportedTypes = new Set([
  'text',
  'longText',
  'email',
  'phone',
  'date',
  'singleChoice',
  'multipleChoice',
  'rating5',
  'rating10',
  'matrix',
  'information',
  'consent',
  'fileReference'
]);

const files = fs.readdirSync(root)
  .filter(file => file.endsWith('.cmdl.yaml'))
  .map(file => path.join(root, file));

let errors = 0;

for (const file of files) {
  const content = fs.readFileSync(file, 'utf8');
  const result = validate(file, content);

  if (result.length === 0) {
    console.log(`OK ${file}`);
    continue;
  }

  console.log(`FAIL ${file}`);
  for (const item of result) {
    console.log(`  - ${item}`);
  }
  errors += result.length;
}

if (errors > 0) {
  console.error(`CMDL validation failed: ${errors} error(s).`);
  process.exit(1);
}

console.log(`CMDL validation passed: ${files.length} file(s).`);

function validate(file, content) {
  const issues = [];

  for (const field of ['id', 'title', 'version', 'language', 'status', 'sections']) {
    if (!content.includes(`${field}:`)) {
      issues.push(`Missing field: ${field}`);
    }
  }

  const id = readValue(content, 'id');
  if (id && !/^FORM-\d{3}$/.test(id)) {
    issues.push(`Invalid form id: ${id}`);
  }

  const version = readValue(content, 'version');
  if (version && !/^\d+\.\d+\.\d+$/.test(version)) {
    issues.push(`Invalid version: ${version}`);
  }

  const questionBlocks = readQuestionBlocks(content);
  const types = questionBlocks.map(block => block.type).filter(Boolean);

  if (types.length === 0) {
    issues.push('No question type found');
  }

  for (const type of types) {
    if (!supportedTypes.has(type)) {
      issues.push(`Unsupported question type: ${type}`);
    }
  }

  checkDuplicates(readSectionIds(content), 'section id', issues);
  checkDuplicates(questionBlocks.map(block => block.id), 'question id', issues);
  checkQuestionBlocks(questionBlocks, issues);

  return issues;
}

function readValue(content, field) {
  const match = content.match(new RegExp(`^\\s*${field}:\\s*(.+)$`, 'm'));
  return match ? match[1].trim() : null;
}

function readSectionIds(content) {
  return [...content.matchAll(/^\s*-\s+id:\s*([A-Z][A-Z0-9_]*)\s*$/gm)]
    .map(match => match[1].trim())
    .filter(value => !/^Q\d{3}$/.test(value));
}

function readQuestionBlocks(content) {
  const blocks = [];
  const lines = content.split('\n');
  let current = null;

  for (const line of lines) {
    const idMatch = line.match(/^\s*-\s+id:\s*(Q\d{3})\s*$/);
    if (idMatch) {
      current = {
        id: idMatch[1],
        type: null,
        label: null,
        required: null,
        hasOptions: false,
        hasRows: false
      };
      blocks.push(current);
      continue;
    }

    if (!current) {
      continue;
    }

    const trimmed = line.trim();

    if (trimmed.startsWith('type:')) {
      current.type = trimmed.slice('type:'.length).trim();
      continue;
    }

    if (trimmed.startsWith('label:')) {
      current.label = trimmed.slice('label:'.length).trim();
      continue;
    }

    if (trimmed.startsWith('required:')) {
      current.required = trimmed.slice('required:'.length).trim();
      continue;
    }

    if (trimmed === 'options:') {
      current.hasOptions = true;
      continue;
    }

    if (trimmed === 'rows:') {
      current.hasRows = true;
    }
  }

  return blocks;
}

function checkDuplicates(values, label, issues) {
  const seen = new Set();
  for (const value of values) {
    if (seen.has(value)) {
      issues.push(`Duplicate ${label}: ${value}`);
    }
    seen.add(value);
  }
}

function checkQuestionBlocks(blocks, issues) {
  for (const block of blocks) {
    if (!block.type) {
      issues.push(`Question ${block.id} has no type`);
      continue;
    }

    if (!block.label) {
      issues.push(`Question ${block.id} has no label`);
    }

    if (block.type !== 'information' && block.required === null) {
      issues.push(`Question ${block.id} has no required flag`);
    }

    if ((block.type === 'singleChoice' || block.type === 'multipleChoice') && !block.hasOptions) {
      issues.push(`Question ${block.id} uses ${block.type} without options`);
    }

    if (block.type === 'matrix' && !block.hasRows) {
      issues.push(`Question ${block.id} uses matrix without rows`);
    }
  }
}
