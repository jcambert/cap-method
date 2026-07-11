import fs from 'node:fs';

const defaultInputPath = 'questionnaire-engine/ai/generated/sample.ai-analysis-draft.md';
const inputPath = process.argv[2] ?? defaultInputPath;

if (!fs.existsSync(inputPath)) {
  console.error(`AI analysis draft not found: ${inputPath}`);
  process.exit(1);
}

const markdown = fs.readFileSync(inputPath, 'utf8');
const errors = [];

const requiredSections = [
  '# Analyse IA assistée',
  '## Avertissement méthodologique',
  '## Traçabilité',
  '## Synthèse neutre des réponses',
  '## Thèmes récurrents',
  '## Valeurs exprimées',
  '## Motivations apparentes',
  '## Compétences évoquées',
  '## Contraintes et freins',
  '## Hypothèses professionnelles',
  '## Points à clarifier',
  '## Questions d entretien',
  '## Risques d interprétation',
  '## Préparation consultant',
  '## Validation consultant obligatoire'
];

const forbiddenPhrases = [
  'Cette personne est',
  'Cette personne doit',
  'Le bon métier est',
  'Il faut absolument',
  'Son problème principal est',
  'Le diagnostic est',
  'Le profil psychologique est'
];

const requiredPhrases = [
  'brouillon',
  'validation consultant obligatoire',
  'Les réponses suggèrent',
  'Ce point mérite validation',
  'Priorités d entretien',
  'Hypothèses à valider',
  'Hypothèses à écarter ou nuancer'
];

for (const section of requiredSections) {
  if (!markdown.includes(section)) {
    errors.push(`Missing required section: ${section}`);
  }
}

for (const phrase of forbiddenPhrases) {
  if (containsCaseInsensitive(markdown, phrase)) {
    errors.push(`Forbidden phrase detected: ${phrase}`);
  }
}

for (const phrase of requiredPhrases) {
  if (!containsCaseInsensitive(markdown, phrase)) {
    errors.push(`Missing required guardrail phrase: ${phrase}`);
  }
}

if (!isOrdered(markdown, requiredSections)) {
  errors.push('Required sections are not in the expected order.');
}

if (errors.length > 0) {
  console.error('AI analysis draft validation failed.');
  for (const error of errors) {
    console.error(`- ${error}`);
  }
  process.exit(1);
}

console.log('AI analysis draft validation succeeded.');
console.log(`Checked file: ${inputPath}`);
console.log(`Required sections: ${requiredSections.length}`);
console.log(`Forbidden phrases: ${forbiddenPhrases.length}`);
console.log(`Required guardrail phrases: ${requiredPhrases.length}`);

function containsCaseInsensitive(value, pattern) {
  return value.toLocaleLowerCase('fr-FR').includes(pattern.toLocaleLowerCase('fr-FR'));
}

function isOrdered(value, sections) {
  let currentIndex = -1;

  for (const section of sections) {
    const nextIndex = value.indexOf(section);
    if (nextIndex === -1 || nextIndex <= currentIndex) {
      return false;
    }
    currentIndex = nextIndex;
  }

  return true;
}
