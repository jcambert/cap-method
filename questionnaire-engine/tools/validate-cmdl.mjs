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

  const types = readTypes(content);
  if (types.length === 0) {
    issues.push('No question type found');
  }

  for (const type of types) {
    if (!supportedTypes.has(type)) {
      issues.push(`Unsupported question type: ${type}`);
    }
  }

  checkDuplicates(readSectionIds(content), 'section id', issues);
  checkDuplicates(readQuestionIds(content), 'question id', issues);
  checkQuestionBlocks(content, issues);

  return issues;
}

function readValue(content, field) {
  const match = content.match(new RegExp(`^\\s*${field}:\\s*(.+)$`, 'm'));
  return match ? match[1].trim() : null;
}

function readTypes(content) {
  return [...content.matchAll(/^\s*type:\s*(.+)$/gm)]
    .map(match => match[1].trim());
}

function readSectionIds(content) {
  return [...content.matchAll(/^\s*-\s+id:\s*([A-Z][A-Z0-9_]*)\s*$/gm)]
    .map(match => match[1].trim());
}

function readQuestionIds(content) {
  return [...content.matchAll(/^\s*-\s+id:\s*(Q\d{3})\s*$/gm)]
    .map(match => match[1].trim());
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

function checkQuestionBlocks(content, issues) {
  const blocks = content.split(/\n\s*-\s+id:\s+/).slice(1);

  for (const block of blocks) {
    const id = block.split('\n')[0].trim();
    const type = readValue(block, 'type');

    if (!id.startsWith('Q')) {
      continue;
    }

    if (!type) {
      issues.push(`Question ${id} has no type`);
      continue;
    }

    if (!block.includes('label:')) {
      issues.push(`Question ${id} has no label`);
    }

    if (type !== 'information' && !block.includes('required:')) {
      issues.push(`Question ${id} has no required flag`);
    }

    if ((type === 'singleChoice' || type === 'multipleChoice') && !block.includes('options:')) {
      issues.push(`Question ${id} uses ${type} without options`);
    }

    if (type === 'matrix' && !block.includes('rows:')) {
      issues.push(`Question ${id} uses matrix without rows`);
    }
  }
}
