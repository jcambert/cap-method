import fs from 'node:fs';
import path from 'node:path';

const defaultCmdlFolder = 'questionnaire-engine/cmdl/examples';
const defaultOutputFolder = 'questionnaire-engine/responses/generated/samples';

const cmdlFolder = process.argv[2] ?? defaultCmdlFolder;
const outputFolder = process.argv[3] ?? defaultOutputFolder;

fs.mkdirSync(outputFolder, { recursive: true });

const definitions = fs.readdirSync(cmdlFolder)
  .filter(file => file.endsWith('.cmdl.yaml'))
  .sort()
  .map(file => path.join(cmdlFolder, file))
  .map(file => parseCmdl(fs.readFileSync(file, 'utf8')));

for (const definition of definitions) {
  const csv = renderSampleCsv(definition);
  const outputPath = path.join(outputFolder, `${definition.id}.responses.sample.csv`);
  fs.writeFileSync(outputPath, csv, 'utf8');
  console.log(`Generated ${outputPath}`);
}

console.log(`Sample CSV files: ${definitions.length}`);

function parseCmdl(source) {
  const lines = source.split('\n');
  const definition = {
    id: readRootValue(lines, 'id'),
    title: readRootValue(lines, 'title'),
    questions: []
  };

  let currentQuestion = null;
  let currentList = null;

  for (const line of lines) {
    const trimmed = line.trim();

    if (line.startsWith('      - id: ')) {
      currentQuestion = {
        id: valueAfter(trimmed, '- id:'),
        type: null,
        label: null,
        required: false,
        options: []
      };
      definition.questions.push(currentQuestion);
      currentList = null;
      continue;
    }

    if (!currentQuestion) {
      continue;
    }

    if (trimmed.startsWith('type:')) {
      currentQuestion.type = valueAfter(trimmed, 'type:');
      currentList = null;
      continue;
    }

    if (trimmed.startsWith('label:')) {
      currentQuestion.label = valueAfter(trimmed, 'label:');
      currentList = null;
      continue;
    }

    if (trimmed.startsWith('required:')) {
      currentQuestion.required = valueAfter(trimmed, 'required:') === 'true';
      currentList = null;
      continue;
    }

    if (trimmed === 'options:') {
      currentList = 'options';
      continue;
    }

    if (currentList === 'options' && trimmed.startsWith('- ')) {
      currentQuestion.options.push(trimmed.slice(2).trim());
    }
  }

  return definition;
}

function readRootValue(lines, field) {
  const prefix = `${field}: `;
  const line = lines.find(item => item.startsWith(prefix));
  return line ? line.slice(prefix.length).trim() : '';
}

function valueAfter(text, prefix) {
  return text.slice(prefix.length).trim();
}

function renderSampleCsv(definition) {
  const headers = ['Timestamp'];
  const values = ['2026-07-10 10:00:00'];

  for (const question of definition.questions) {
    if (question.type === 'information') {
      continue;
    }

    headers.push(question.label);
    values.push(sampleValue(definition, question));
  }

  return `${headers.map(escapeCsv).join(',')}\n${values.map(escapeCsv).join(',')}\n`;
}

function sampleValue(definition, question) {
  if (question.type === 'email') {
    return 'beneficiary.test@example.com';
  }

  if (question.type === 'phone') {
    return '0600000000';
  }

  if (question.type === 'date') {
    return '2026-07-10';
  }

  if (question.type === 'singleChoice') {
    return question.options[0] ?? 'Option test';
  }

  if (question.type === 'multipleChoice') {
    return question.options.slice(0, 2).join(', ') || 'Option test';
  }

  if (question.type === 'rating5') {
    return '4';
  }

  if (question.type === 'rating10') {
    return '7';
  }

  if (question.type === 'longText') {
    return `Reponse test pour ${definition.id} ${question.id}`;
  }

  return `Valeur test ${definition.id} ${question.id}`;
}

function escapeCsv(value) {
  const text = String(value ?? '');
  if (text.includes(',') || text.includes('"') || text.includes('\n')) {
    return `"${text.replaceAll('"', '""')}"`;
  }
  return text;
}
