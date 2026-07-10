import fs from 'node:fs';
import path from 'node:path';

const defaultCmdlFolder = 'questionnaire-engine/cmdl/examples';
const defaultCsvFolder = 'questionnaire-engine/responses/generated/samples';
const defaultOutputPath = 'questionnaire-engine/responses/generated/session.response.normalized.json';

const cmdlFolder = process.argv[2] ?? defaultCmdlFolder;
const csvFolder = process.argv[3] ?? defaultCsvFolder;
const outputPath = process.argv[4] ?? defaultOutputPath;

const definitions = loadDefinitions(cmdlFolder);
const forms = [];
const warnings = [];
const errors = [];

for (const definition of definitions) {
  const csvPath = path.join(csvFolder, `${definition.id}.responses.sample.csv`);

  if (!fs.existsSync(csvPath)) {
    errors.push({ code: 'missing_response_csv', formId: definition.id, path: csvPath });
    continue;
  }

  const rows = parseCsv(fs.readFileSync(csvPath, 'utf8'));
  if (rows.length < 2) {
    errors.push({ code: 'empty_response_csv', formId: definition.id, path: csvPath });
    continue;
  }

  const form = normalizeForm(definition, rows[0], rows[1]);
  forms.push(form);
  warnings.push(...form.import.warnings);
  errors.push(...form.import.errors);
}

const session = {
  id: 'sample-session',
  beneficiaryId: 'beneficiary-test',
  consultantId: 'consultant-test',
  startedAt: firstSubmittedAt(forms),
  completedAt: forms.length === definitions.length ? lastSubmittedAt(forms) : null,
  status: errors.length === 0 && forms.length === definitions.length ? 'completed' : 'active',
  source: 'google_forms',
  forms,
  completeness: buildCompleteness(definitions, forms),
  analysisReadiness: buildAnalysisReadiness(forms, errors),
  import: {
    status: errors.length === 0 && warnings.length === 0 ? 'success' : errors.length === 0 ? 'success_with_warnings' : 'failed',
    formsExpected: definitions.length,
    formsImported: forms.length,
    answersImported: forms.reduce((total, form) => total + form.answers.length, 0),
    warnings,
    errors
  }
};

fs.mkdirSync(path.dirname(outputPath), { recursive: true });
fs.writeFileSync(outputPath, JSON.stringify(session, null, 2), 'utf8');

console.log(`Session: ${session.id}`);
console.log(`Forms imported: ${session.import.formsImported}/${session.import.formsExpected}`);
console.log(`Answers imported: ${session.import.answersImported}`);
console.log(`Analysis ready: ${session.analysisReadiness.ready}`);
console.log(`Warnings: ${warnings.length}`);
console.log(`Errors: ${errors.length}`);
console.log(`Output: ${outputPath}`);

if (errors.length > 0) {
  process.exit(1);
}

function loadDefinitions(folder) {
  return fs.readdirSync(folder)
    .filter(file => file.endsWith('.cmdl.yaml'))
    .sort()
    .map(file => path.join(folder, file))
    .map(file => parseCmdl(fs.readFileSync(file, 'utf8')));
}

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

function normalizeForm(definition, headers, row) {
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
    const normalizedValue = normalizeValue(question, rawValue, errors, warnings);

    if (question.required && isMissing(normalizedValue)) {
      errors.push({ code: 'missing_required_answer', formId: definition.id, questionId: question.id });
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
      warnings.push({ code: 'unknown_spreadsheet_column', formId: definition.id, column: header });
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
  headers.forEach((header, index) => map.set(header, row[index] ?? ''));
  return map;
}

function valueAt(headers, row, header) {
  const index = headers.indexOf(header);
  return index >= 0 ? row[index] : null;
}

function normalizeValue(question, rawValue, errors, warnings) {
  const value = rawValue.trim();

  if (value.length === 0) {
    return null;
  }

  if (question.type === 'singleChoice') {
    validateOption(question, value, errors);
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

  return value;
}

function validateOption(question, value, errors) {
  if (question.options.length > 0 && !question.options.includes(value)) {
    errors.push({ code: 'invalid_option', questionId: question.id, questionLabel: question.label, value });
  }
}

function normalizeRating(question, value, min, max, errors) {
  const parsed = Number.parseInt(value, 10);
  if (Number.isNaN(parsed) || parsed < min || parsed > max) {
    errors.push({ code: 'invalid_rating', questionId: question.id, questionLabel: question.label, value });
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

function buildCompleteness(definitions, forms) {
  const importedIds = new Set(forms.map(form => form.formId));
  const missingFormIds = definitions.map(definition => definition.id).filter(id => !importedIds.has(id));

  return {
    formsExpected: definitions.length,
    formsImported: forms.length,
    complete: missingFormIds.length === 0,
    missingFormIds
  };
}

function buildAnalysisReadiness(forms, errors) {
  const formIds = new Set(forms.map(form => form.formId));
  const requiredFormIds = ['FORM-001', 'FORM-002', 'FORM-003'];
  const missingRequiredFormIds = requiredFormIds.filter(id => !formIds.has(id));
  const hasProjectForm = formIds.has('FORM-006') || formIds.has('FORM-007');
  const ready = errors.length === 0 && missingRequiredFormIds.length === 0 && hasProjectForm;

  return {
    ready,
    missingRequiredFormIds,
    hasProjectForm,
    blockers: buildReadinessBlockers(errors, missingRequiredFormIds, hasProjectForm)
  };
}

function buildReadinessBlockers(errors, missingRequiredFormIds, hasProjectForm) {
  const blockers = [];

  if (errors.length > 0) {
    blockers.push('import_errors');
  }

  if (missingRequiredFormIds.length > 0) {
    blockers.push('missing_required_forms');
  }

  if (!hasProjectForm) {
    blockers.push('missing_project_or_action_plan_form');
  }

  return blockers;
}

function firstSubmittedAt(forms) {
  const values = forms.map(form => form.submittedAt).filter(Boolean).sort();
  return values[0] ?? null;
}

function lastSubmittedAt(forms) {
  const values = forms.map(form => form.submittedAt).filter(Boolean).sort();
  return values[values.length - 1] ?? null;
}
