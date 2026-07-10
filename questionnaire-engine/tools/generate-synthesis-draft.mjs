import fs from 'node:fs';
import path from 'node:path';

const defaultInputPath = 'questionnaire-engine/analysis/generated/sample.analysis-snapshot.json';
const defaultOutputPath = 'questionnaire-engine/synthesis/generated/sample.synthesis-draft.md';

const inputPath = process.argv[2] ?? defaultInputPath;
const outputPath = process.argv[3] ?? defaultOutputPath;

const snapshot = JSON.parse(fs.readFileSync(inputPath, 'utf8'));
const markdown = renderSynthesisDraft(snapshot);

fs.mkdirSync(path.dirname(outputPath), { recursive: true });
fs.writeFileSync(outputPath, markdown, 'utf8');

console.log(`Synthesis draft: ${outputPath}`);
console.log(`Snapshot: ${snapshot.id}`);
console.log(`Status: ${snapshot.status}`);

function renderSynthesisDraft(snapshot) {
  const completion = indicator(snapshot, 'forms_completion_rate');
  const rating5 = indicator(snapshot, 'rating5_average');
  const rating10 = indicator(snapshot, 'rating10_average');
  const longTextCount = indicator(snapshot, 'long_text_answer_count');
  const readiness = indicator(snapshot, 'analysis_readiness');

  return [
    '# Brouillon de synthèse consultant',
    '',
    '## Statut du document',
    '',
    'Ce document est un brouillon de travail généré automatiquement à partir des réponses normalisées et de l analyse initiale.',
    '',
    'Il doit être relu, corrigé et complété par le consultant avant toute remise au bénéficiaire.',
    '',
    '## Traçabilité',
    '',
    `- Session : ${snapshot.sessionId}`,
    `- Bénéficiaire : ${snapshot.beneficiaryId}`,
    `- Consultant : ${snapshot.consultantId}`,
    `- Statut analyse : ${snapshot.status}`,
    `- Formulaires importés : ${snapshot.coverage?.formsImported ?? 'n/a'} / ${snapshot.coverage?.formsExpected ?? 'n/a'}`,
    `- Réponses importées : ${snapshot.coverage?.answersImported ?? 'n/a'}`,
    '',
    '## Vue d ensemble',
    '',
    paragraph([
      sentenceFromIndicator(completion),
      sentenceFromIndicator(readiness),
      sentenceFromIndicator(longTextCount)
    ]),
    '',
    '## Indicateurs principaux',
    '',
    renderIndicators([completion, rating5, rating10, longTextCount, readiness]),
    '',
    '## Points saillants détectés',
    '',
    renderHighlights(snapshot.highlights ?? []),
    '',
    '## Points de vigilance',
    '',
    renderRisks(snapshot.risks ?? []),
    '',
    '## Questions de préparation pour le consultant',
    '',
    renderConsultantPrompts(snapshot.consultantPrompts ?? []),
    '',
    '## Première lecture consultant',
    '',
    'À compléter par le consultant après lecture qualitative des réponses :',
    '',
    '- éléments confirmés par les réponses ;',
    '- éléments à clarifier en entretien ;',
    '- hypothèses de pistes professionnelles ;',
    '- freins à lever ;',
    '- ressources mobilisables ;',
    '- premières actions à envisager.',
    '',
    '## Décision de suite',
    '',
    '- [ ] Valider les informations clés avec le bénéficiaire',
    '- [ ] Compléter les points insuffisamment documentés',
    '- [ ] Préparer la synthèse narrative',
    '- [ ] Préparer le plan d action',
    '- [ ] Vérifier la cohérence avec les objectifs initiaux',
    '',
    '## Limite importante',
    '',
    'Ce brouillon ne constitue pas une synthèse finale de bilan de compétences. Il sert de support structuré à la revue consultant.'
  ].join('\n');
}

function indicator(snapshot, code) {
  return (snapshot.indicators ?? []).find(item => item.code === code) ?? null;
}

function sentenceFromIndicator(item) {
  if (!item) {
    return null;
  }

  if (item.value === null || item.value === undefined) {
    return `${item.label} : aucune donnée exploitable à ce stade.`;
  }

  return `${item.label} : ${formatValue(item.value, item.unit)}. ${item.interpretation ?? ''}`.trim();
}

function renderIndicators(items) {
  const available = items.filter(Boolean);

  if (available.length === 0) {
    return 'Aucun indicateur disponible.';
  }

  return [
    '| Indicateur | Valeur | Interprétation |',
    '| --- | --- | --- |',
    ...available.map(item => `| ${escapeTable(item.label)} | ${escapeTable(formatValue(item.value, item.unit))} | ${escapeTable(item.interpretation ?? '')} |`)
  ].join('\n');
}

function renderHighlights(highlights) {
  if (highlights.length === 0) {
    return 'Aucun point saillant automatique détecté. Le consultant doit compléter l analyse qualitative.';
  }

  return highlights.map(highlight => {
    const examples = (highlight.examples ?? []).map(example => `  - ${example.formId} / ${example.questionId} : ${example.value}`).join('\n');
    return [`### ${highlight.label}`, '', `Occurrences détectées : ${highlight.count}`, '', examples].join('\n');
  }).join('\n\n');
}

function renderRisks(risks) {
  if (risks.length === 0) {
    return 'Aucun point de vigilance automatique majeur détecté à ce stade.';
  }

  return risks.map(risk => [
    `### ${risk.label}`,
    '',
    `- Sévérité : ${risk.severity}`,
    `- Recommandation : ${risk.recommendation}`
  ].join('\n')).join('\n\n');
}

function renderConsultantPrompts(prompts) {
  if (prompts.length === 0) {
    return 'Aucune question de préparation générée.';
  }

  return prompts.map(prompt => `- ${prompt.question}`).join('\n');
}

function paragraph(sentences) {
  const values = sentences.filter(Boolean);
  if (values.length === 0) {
    return 'Aucune donnée de synthèse disponible.';
  }
  return values.join(' ');
}

function formatValue(value, unit) {
  if (value === null || value === undefined) {
    return 'n/a';
  }

  if (unit === 'percent') {
    return `${value} %`;
  }

  if (unit === 'boolean') {
    return value ? 'oui' : 'non';
  }

  return String(value);
}

function escapeTable(value) {
  return String(value ?? '').replaceAll('|', '\\|');
}
