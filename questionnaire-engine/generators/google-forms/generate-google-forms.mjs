import fs from 'node:fs';
import path from 'node:path';

const defaultInput = 'questionnaire-engine/cmdl/examples';
const defaultOutput = 'questionnaire-engine/generators/google-forms/generated/cap_method_generated_suite.gs';

const input = process.argv[2] ?? defaultInput;
const output = process.argv[3] ?? defaultOutput;

const definitions = loadDefinitions(input);
const script = renderSuite(definitions);

fs.mkdirSync(path.dirname(output), { recursive: true });
fs.writeFileSync(output, script, 'utf8');

console.log(`Generated ${output}`);
console.log(`Forms: ${definitions.length}`);

function loadDefinitions(inputPath) {
  const stats = fs.statSync(inputPath);

  if (stats.isFile()) {
    return [parseCmdl(fs.readFileSync(inputPath, 'utf8'))];
  }

  return fs.readdirSync(inputPath)
    .filter(file => file.endsWith('.cmdl.yaml'))
    .sort()
    .map(file => path.join(inputPath, file))
    .map(file => parseCmdl(fs.readFileSync(file, 'utf8')));
}

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

function renderSuite(definitions) {
  const lines = [];
  lines.push('// GENERATED FILE - DO NOT EDIT MANUALLY');
  lines.push('// Source: CAP Method CMDL questionnaire definitions');
  lines.push('');
  lines.push('function createCapMethodGeneratedFormsSuite() {');
  lines.push('  const forms = [];');
  lines.push('');

  for (const definition of definitions) {
    const builderName = buildFunctionName(definition);
    lines.push(`  const form_${safeId(definition.id)} = FormApp.create('${escapeJs(definition.id + ' - ' + definition.title)}');`);
    lines.push(`  ${builderName}(form_${safeId(definition.id)});`);
    lines.push(`  forms.push(form_${safeId(definition.id)});`);
    lines.push('');
  }

  lines.push('  return forms;');
  lines.push('}');
  lines.push('');

  for (const definition of definitions) {
    lines.push(renderBuilder(definition));
  }

  return lines.join('\n');
}

function renderBuilder(definition) {
  const lines = [];
  lines.push(`function ${buildFunctionName(definition)}(form) {`);
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

function buildFunctionName(definition) {
  return `build_${safeId(definition.id)}_`;
}

function safeId(id) {
  return id.replaceAll('-', '_');
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
