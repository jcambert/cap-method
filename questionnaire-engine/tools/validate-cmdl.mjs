import fs from 'node:fs';
import path from 'node:path';

const root = process.argv[2] ?? 'questionnaire-engine/cmdl/examples';
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

  if (!content.includes('type:')) {
    issues.push('No question type found');
  }

  return issues;
}

function readValue(content, field) {
  const match = content.match(new RegExp(`^${field}:\\s*(.+)$`, 'm'));
  return match ? match[1].trim() : null;
}
