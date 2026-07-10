import fs from 'node:fs';
import path from 'node:path';

const defaultCmdlPath = 'questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml';
const defaultCsvPath = 'questionnaire-engine/responses/samples/FORM-001.responses.sample.csv';
const defaultOutputPath = 'questionnaire-engine/responses/generated/FORM-001.response.normalized.json';

const cmdlPath = process.argv[2] ?? defaultCmdlPath;
const csvPath = process.argv[3] ?? defaultCsvPath;
const outputPath = process.argv[4] ?? defaultOutputPath;

const definition = parseCmdl(fs.readFileSync(cmdlPath, 'utf8'));
const csvRows = parseCsv(fs.readFileSync(csvPath, 'utf8'));

if (csvRows.length < 2) {
  throw new Error('CSV must contain one header row and at least one response row.');
}

const normalized = normalizeResponse(definition, csvRows[0], csvRows[1]);

fs.mkdirSync(path.dirname(outputPath), { recursive: true });
fs.writeFileSync(outputPath, JSON.stringify(normalized, null, 2), 'utf8');

console.log(`Imported ${definition.id}`);
console.log(`Answers: ${normalized.answers.length}`);
console.log(`Warnings: ${normalized.import.warnings.length}`);
console.log(`Errors: ${normalized.import.errors.length}`);
console.log(`Output: ${outputPath}`);

if (normalized.import.errors.length > 0) {
  process.exit(1);
}

function parseCmdl(source) {
  const lines = source.split('\n');
  const definition = {
    id: readRootValue(lines, 'id'),
    title: readRootValue(lines, 'title'),
    version: readRootValue(lines, 'version'),
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

function parseCsv(source) {
  const rows = [];
  let row = [];
  let value = '';
  let inQuotes = false;

  for (let index = 0; index < source.length; index += 1) {
    const char = source[index];
    const next = source[index + 1];

    if (char === '"' && inQuotes && next === '"') {
      value += '"';
      index += 1;
      continue;
    }

    if (char === '"') {
      inQuotes = !inQuotes;
      continue;
    }

    if (char === ',' && !inQuotes) {
      row.push(value);
      value = '';
      continue;
    }

    if ((char === '\n' || char === '\r') && !inQuotes) {
      if (char === '\r' && next === '\n') {
        index += 1;
      }
      row.push(value);
      rows.push(row);
      row = [];
      value = '';
      continue;
    }

    value += char;
  }

  if (value.length > 0 || row.length > 0) {
    row.push(value);
    rows.push(row);
  }

  return rows.filter(item => item.some(cell => cell.trim().length > 0));
}

function normalizeResponse(definition, headers, row) {
  const warnings = [];
  const errors = [];
  const submittedAt = valueAt(headers, row, 'Timestamp') ?? valueAt(headers, row, 'Horodateur') ?? null;
  const answerByLabel = buildAnswerByLabel(headers, row);
  const answers = [];

  for (const question of definition.questions) {
    if (question.type === 'information') {
      continue;
    }

    const rawValue = answerByLabel.get(question.label) ?? '';
    const normalizedValue = normalizeValue(question, rawValue, warnings, errors);

    if (question.required && isMissing(normalizedValue)) {
      errors.push({
        code: 'missing_required_answer',
        questionId: question.id,
        questionLabel: question.label
      });
    }

    answers.push({
      id: `${definition.id}.${question.id}`,
      sessionId: 'sample-session',
      formId: definition.id,
      questionId: question.id,
      questionLabel: question.label,
      questionType: question.type,
      rawValue,
      normalizedValue,
      answeredAt: submittedAt,
      source: 'google_forms'
    });
  }

  for (const header of headers) {
    if (header === 'Timestamp' || header === 'Horodateur') {
      continue;
    }

    if (!definition.questions.some(question => question.label === header)) {
      warnings.push({
        code: 'unknown_spreadsheet_column',
        column: header
      });
    }
  }

  return {
    id: `${definition.id}.sample-response`,
    sessionId: 'sample-session',
    formId: definition.id,
    formTitle: definition.title,
    submittedAt,
    sourceSheetId: null,
    sourceRowNumber: 2,
    answers,
    import: {
      status: errors.length === 0 && warnings.length === 0 ? 'success' : errors.length === 0 ? 'success_with_warnings' : 'failed',
      warnings,
      errors
    }
  };
}

function buildAnswerByLabel(headers, row) {
  const map = new Map();

  headers.forEach((header, index) => {
    map.set(header, row[index] ?? '');
  });

  return map;
}

function valueAt(headers, row, header) {
  const index = headers.indexOf(header);
  return index >= 0 ? row[index] : null;
}

function normalizeValue(question, rawValue, warnings, errors) {
  const value = rawValue.trim();

  if (value.length === 0) {
    return null;
  }

  if (question.type === 'text' || question.type === 'email' || question.type === 'phone' || question.type === 'longText' || question.type === 'singleChoice') {
    if (question.type === 'singleChoice') {
      validateOption(question, value, errors);
    }
    return value;
  }

  if (question.type === 'multipleChoice') {
    const values = value.split(',').map(item => item.trim()).filter(item => item.length > 0);
    for (const item of values) {
      validateOption(question, item, errors);
    }
    return values;
  }

  if (question.type === 'rating5') {
    return normalizeRating(question, value, 1, 5, errors);
  }

  if (question.type === 'rating10') {
    return normalizeRating(question, value, 1, 10, errors);
  }

  if (question.type === 'date') {
    const parsed = new Date(value);
    if (Number.isNaN(parsed.getTime())) {
      warnings.push({ code: 'date_parse_warning', questionId: question.id, rawValue });
      return value;
    }
    return parsed.toISOString().slice(0, 10);
  }

  warnings.push({ code: 'unsupported_type', questionId: question.id, questionType: question.type });
  return value;
}

function validateOption(question, value, errors) {
  if (question.options.length === 0) {
    return;
  }

  if (!question.options.includes(value)) {
    errors.push({
      code: 'invalid_option',
      questionId: question.id,
      questionLabel: question.label,
      value
    });
  }
}

function normalizeRating(question, value, min, max, errors) {
  const parsed = Number.parseInt(value, 10);

  if (Number.isNaN(parsed) || parsed < min || parsed > max) {
    errors.push({
      code: 'invalid_rating',
      questionId: question.id,
      questionLabel: question.label,
      value
    });
    return null;
  }

  return parsed;
}

function isMissing(value) {
  if (value === null || value === undefined) {
    return true;
  }

  if (Array.isArray(value)) {
    return value.length === 0;
  }

  return false;
}
