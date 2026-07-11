import fs from 'node:fs';

const defaultInputPath = 'questionnaire-engine/ai/generated/sample.ai-analysis-manifest.json';
const inputPath = process.argv[2] ?? defaultInputPath;

if (!fs.existsSync(inputPath)) {
  console.error(`AI analysis manifest not found: ${inputPath}`);
  process.exit(1);
}

const manifest = JSON.parse(fs.readFileSync(inputPath, 'utf8'));
const errors = [];

requiredString('id');
requiredString('draftId');
requiredString('generatedAt');
requiredString('provider');
requiredString('model');
requiredString('status');
requiredBoolean('requiresConsultantValidation');
requiredBoolean('guardrailsApplied');

requiredNestedString(['source', 'type']);
requiredNestedString(['source', 'path']);
requiredNestedString(['output', 'draftPath']);
requiredNestedString(['output', 'manifestPath']);

requiredNestedBoolean(['checks', 'draftGenerated']);
requiredNestedBoolean(['checks', 'manifestGenerated']);
requiredNestedBoolean(['checks', 'externalProviderRequired']);
requiredNestedBoolean(['checks', 'consultantOnlyDraft']);
requiredNestedBoolean(['checks', 'readyForBeneficiaryDelivery']);
requiredNestedBoolean(['compatibility', 'aiOptional']);
requiredNestedBoolean(['compatibility', 'canRunWithoutApiKey']);

requiredArray(['guardrails', 'requiredSections']);
requiredArray(['guardrails', 'requiredGuardrailPhrases']);
requiredArray(['guardrails', 'forbiddenPhrases']);

expectValue('status', 'draft');
expectValue('requiresConsultantValidation', true);
expectValue('guardrailsApplied', true);
expectValue('provider', 'deterministic-local-draft');
expectValue('model', 'none-local-deterministic');
expectNestedValue(['source', 'type'], 'analysis-snapshot');
expectNestedValue(['checks', 'draftGenerated'], true);
expectNestedValue(['checks', 'manifestGenerated'], true);
expectNestedValue(['checks', 'externalProviderRequired'], false);
expectNestedValue(['checks', 'consultantOnlyDraft'], true);
expectNestedValue(['checks', 'readyForBeneficiaryDelivery'], false);
expectNestedValue(['compatibility', 'aiOptional'], true);
expectNestedValue(['compatibility', 'canRunWithoutApiKey'], true);

if (!isIsoDate(manifest.generatedAt)) {
  errors.push('generatedAt must be a valid ISO date.');
}

if (!includesNested(['guardrails', 'requiredSections'], '## Validation consultant obligatoire')) {
  errors.push('Manifest guardrails must include required section: ## Validation consultant obligatoire');
}

if (!includesNested(['guardrails', 'requiredGuardrailPhrases'], 'Ce point mérite validation')) {
  errors.push('Manifest guardrails must include required phrase: Ce point mérite validation');
}

if (!includesNested(['guardrails', 'forbiddenPhrases'], 'Le diagnostic est')) {
  errors.push('Manifest guardrails must include forbidden phrase: Le diagnostic est');
}

if (errors.length > 0) {
  console.error('AI analysis manifest validation failed.');
  for (const error of errors) {
    console.error(`- ${error}`);
  }
  process.exit(1);
}

console.log('AI analysis manifest validation succeeded.');
console.log(`Checked file: ${inputPath}`);
console.log(`Provider: ${manifest.provider}`);
console.log(`Status: ${manifest.status}`);
console.log(`Requires consultant validation: ${manifest.requiresConsultantValidation}`);
console.log(`Guardrails applied: ${manifest.guardrailsApplied}`);

function requiredString(field) {
  if (typeof manifest[field] !== 'string' || manifest[field].trim().length === 0) {
    errors.push(`Missing or invalid string field: ${field}`);
  }
}

function requiredBoolean(field) {
  if (typeof manifest[field] !== 'boolean') {
    errors.push(`Missing or invalid boolean field: ${field}`);
  }
}

function requiredNestedString(path) {
  const value = getNested(path);
  if (typeof value !== 'string' || value.trim().length === 0) {
    errors.push(`Missing or invalid string field: ${path.join('.')}`);
  }
}

function requiredNestedBoolean(path) {
  const value = getNested(path);
  if (typeof value !== 'boolean') {
    errors.push(`Missing or invalid boolean field: ${path.join('.')}`);
  }
}

function requiredArray(path) {
  const value = getNested(path);
  if (!Array.isArray(value) || value.length === 0) {
    errors.push(`Missing or invalid array field: ${path.join('.')}`);
  }
}

function expectValue(field, expected) {
  if (manifest[field] !== expected) {
    errors.push(`Invalid value for ${field}: expected ${expected}, got ${manifest[field]}`);
  }
}

function expectNestedValue(path, expected) {
  const value = getNested(path);
  if (value !== expected) {
    errors.push(`Invalid value for ${path.join('.')}: expected ${expected}, got ${value}`);
  }
}

function includesNested(path, expected) {
  const value = getNested(path);
  return Array.isArray(value) && value.includes(expected);
}

function getNested(path) {
  return path.reduce((current, segment) => current?.[segment], manifest);
}

function isIsoDate(value) {
  const timestamp = Date.parse(value);
  return !Number.isNaN(timestamp) && new Date(timestamp).toISOString() === value;
}
