import fs from 'node:fs';
import path from 'node:path';

const input = process.argv[2] ?? 'questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml';
const output = process.argv[3] ?? 'questionnaire-engine/generators/google-forms/generated/FORM-001.generated.gs';

const source = fs.readFileSync(input, 'utf8');
const definition = parseCmdl(source);
const script = renderAppsScript(definition);

fs.mkdirSync(path.dirname(output), { recursive: true });
fs.writeFileSync(output, script, 'utf8');

console.log(`Generated ${output}`);

function parseCmdl(source) {
  const lines = source.split('\n');
  const definition = {
    id: readRootValue(lines, 'id'),
    title: readRootValue(lines, 'title'),
    version: readRootValue(lines, 'version'),
    sections: []
  };

  let currentSection = null;
  let currentQuestion = null;
  let currentList = null;

  for (const line of lines) {
    const trimmed = line.trim();

    if (line.startsWith('  - id: ')) {
      currentSection = {
        id: valueAfter(trimmed, '- id:'),
        title: '',
        questions: []
      };
      definition.sections.push(currentSection);
      currentQuestion = null;
      currentList = null;
      continue;
    }

    if (line.startsWith('      - id: ')) {
      currentQuestion = {
        id: valueAfter(trimmed, '- id:'),
        type: '',
        label: '',
        required: false,
        options: []
      };
      currentSection.questions.push(currentQuestion);
      currentList = null;
      continue;
    }

    if (currentQuestion && trimmed.startsWith('type:')) {
      currentQuestion.type = valueAfter(trimmed, 'type:');
      continue;
    }

    if (currentQuestion && trimmed.startsWith('label:')) {
      currentQuestion.label = valueAfter(trimmed, 'label:');
      continue;
    }

    if (currentQuestion && trimmed.startsWith('required:')) {
      currentQuestion.required = valueAfter(trimmed, 'required:') === 'true';
      continue;
    }

    if (currentQuestion && trimmed === 'options:') {
      currentList = 'options';
      continue;
    }

    if (currentQuestion && currentList === 'options' && trimmed.startsWith('- ')) {
      currentQuestion.options.push(trimmed.slice(2));
      continue;
    }

    if (currentSection && !currentQuestion && trimmed.startsWith('title:')) {
      currentSection.title = valueAfter(trimmed, 'title:');
      continue;
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

function renderAppsScript(definition) {
  const lines = [];
  lines.push('// GENERATED FILE - DO NOT EDIT MANUALLY');
  lines.push(`// Source: ${definition.id}.cmdl.yaml`);
  lines.push(`// Version: ${definition.version}`);
  lines.push('');
  lines.push(`function build_${definition.id.replace('-', '_')}_(form) {`);
  lines.push(`  form.setTitle('${escapeJs(definition.id + ' - ' + definition.title)}');`);
  lines.push(`  form.setDescription('${escapeJs('Generated from CAP Method CMDL.')}');`);
  lines.push('');

  for (const section of definition.sections) {
    lines.push(`  form.addSectionHeaderItem().setTitle('${escapeJs(section.title)}');`);

    for (const question of section.questions) {
      renderQuestion(lines, question);
    }

    lines.push('');
  }

  lines.push('}');
  lines.push('');
  return lines.join('\n');
}

function renderQuestion(lines, question) {
  if (question.type === 'information') {
    lines.push(`  form.addSectionHeaderItem().setTitle('${escapeJs(question.label)}');`);
    return;
  }

  if (question.type === 'text' || question.type === 'email' || question.type === 'phone') {
    lines.push(`  form.addTextItem().setTitle('${escapeJs(question.label)}').setRequired(${question.required});`);
    return;
  }

  if (question.type === 'longText') {
    lines.push(`  form.addParagraphTextItem().setTitle('${escapeJs(question.label)}').setRequired(${question.required});`);
    return;
  }

  if (question.type === 'singleChoice') {
    lines.push(`  form.addMultipleChoiceItem().setTitle('${escapeJs(question.label)}').setChoiceValues([${renderArray(question.options)}]).setRequired(${question.required});`);
    return;
  }

  if (question.type === 'multipleChoice') {
    lines.push(`  form.addCheckboxItem().setTitle('${escapeJs(question.label)}').setChoiceValues([${renderArray(question.options)}]).setRequired(${question.required});`);
    return;
  }

  if (question.type === 'rating10') {
    lines.push(`  form.addScaleItem().setTitle('${escapeJs(question.label)}').setBounds(1, 10).setRequired(${question.required});`);
    return;
  }

  if (question.type === 'rating5') {
    lines.push(`  form.addScaleItem().setTitle('${escapeJs(question.label)}').setBounds(1, 5).setRequired(${question.required});`);
    return;
  }

  lines.push(`  // Unsupported type for ${question.id}: ${question.type}`);
}

function renderArray(values) {
  return values.map(value => `'${escapeJs(value)}'`).join(', ');
}

function escapeJs(value) {
  return value.replaceAll('\\', '\\\\').replaceAll("'", "\\'");
}
