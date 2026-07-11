import fs from 'node:fs';
import path from 'node:path';

const defaultInputPath = 'questionnaire-engine/analysis/generated/sample.analysis-snapshot.json';
const defaultOutputPath = 'questionnaire-engine/ai/generated/sample.ai-analysis-draft.md';

const provider = 'deterministic-local-draft';
const model = 'none-local-deterministic';
const status = 'draft';
const requiresConsultantValidation = true;
const guardrailsApplied = true;

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

const requiredGuardrailPhrases = [
  'brouillon',
  'validation consultant obligatoire',
  'Les réponses suggèrent',
  'Ce point mérite validation',
  'Lecture factuelle',
  'Utilisation en entretien',
  'Priorités d entretien',
  'Hypothèses à valider',
  'Hypothèses à écarter ou nuancer'
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

const inputPath = process.argv[2] ?? defaultInputPath;
const outputPath = process.argv[3] ?? defaultOutputPath;
const manifestPath = process.argv[4] ?? defaultManifestPathFrom(outputPath);
const generatedAt = new Date().toISOString();

if (!fs.existsSync(inputPath)) {
  console.error(`Input analysis snapshot not found: ${inputPath}`);
  process.exit(1);
}

const snapshot = JSON.parse(fs.readFileSync(inputPath, 'utf8'));
const markdown = renderAiAnalysisDraft(snapshot, generatedAt);
const manifest = buildManifest(snapshot, inputPath, outputPath, manifestPath, generatedAt);

fs.mkdirSync(path.dirname(outputPath), { recursive: true });
fs.writeFileSync(outputPath, markdown, 'utf8');
fs.mkdirSync(path.dirname(manifestPath), { recursive: true });
fs.writeFileSync(manifestPath, JSON.stringify(manifest, null, 2), 'utf8');

console.log(`AI analysis draft: ${outputPath}`);
console.log(`AI analysis manifest: ${manifestPath}`);
console.log(`Snapshot: ${snapshot.id ?? 'n/a'}`);
console.log(`Provider: ${provider}`);
console.log(`Status: ${status}`);
console.log(`Requires consultant validation: ${requiresConsultantValidation}`);

function renderAiAnalysisDraft(snapshot, generatedAt) {
  const completion = indicator(snapshot, 'forms_completion_rate');
  const rating5 = indicator(snapshot, 'rating5_average');
  const rating10 = indicator(snapshot, 'rating10_average');
  const longTextCount = indicator(snapshot, 'long_text_answer_count');
  const readiness = indicator(snapshot, 'analysis_readiness');
  const highlights = snapshot.highlights ?? [];
  const risks = snapshot.risks ?? [];
  const prompts = snapshot.consultantPrompts ?? [];

  return [
    '# Analyse IA assistée',
    '',
    '## Avertissement méthodologique',
    '',
    'Ce document est un brouillon IA assisté généré en mode local déterministe à partir de l analyse structurée CAP.',
    '',
    'Il ne constitue pas une synthèse finale, un diagnostic, une décision d orientation ou une recommandation professionnelle définitive.',
    '',
    'Les réponses suggèrent des pistes de lecture qui doivent être relues, nuancées et validées par le consultant avec le bénéficiaire.',
    '',
    '## Traçabilité',
    '',
    renderTraceability(snapshot, generatedAt),
    '',
    '## Synthèse neutre des réponses',
    '',
    renderNeutralSummary([completion, readiness, longTextCount, rating5, rating10], highlights, risks),
    '',
    '## Thèmes récurrents',
    '',
    renderRecurringThemes(highlights),
    '',
    '## Valeurs exprimées',
    '',
    renderValueHypotheses(highlights),
    '',
    '## Motivations apparentes',
    '',
    renderMotivationHypotheses(highlights),
    '',
    '## Compétences évoquées',
    '',
    renderSkills(highlights),
    '',
    '## Contraintes et freins',
    '',
    renderConstraints(highlights, risks),
    '',
    '## Hypothèses professionnelles',
    '',
    renderProfessionalHypotheses(highlights, risks),
    '',
    '## Points à clarifier',
    '',
    renderClarificationPoints(risks, completion, readiness),
    '',
    '## Questions d entretien',
    '',
    renderInterviewQuestions(prompts, risks),
    '',
    '## Risques d interprétation',
    '',
    renderInterpretationRisks(risks),
    '',
    '## Préparation consultant',
    '',
    renderConsultantPreparation(highlights, risks, prompts, completion, readiness),
    '',
    '## Validation consultant obligatoire',
    '',
    renderConsultantValidation()
  ].join('\n');
}

function buildManifest(snapshot, inputPath, outputPath, manifestPath, generatedAt) {
  return {
    id: `${snapshot.sessionId ?? 'unknown-session'}.ai-analysis-manifest`,
    draftId: `${snapshot.sessionId ?? 'unknown-session'}.ai-analysis-draft`,
    sessionId: snapshot.sessionId ?? null,
    beneficiaryId: snapshot.beneficiaryId ?? null,
    consultantId: snapshot.consultantId ?? null,
    generatedAt,
    source: {
      type: 'analysis-snapshot',
      id: snapshot.id ?? null,
      path: inputPath,
      status: snapshot.status ?? null
    },
    output: {
      draftPath: outputPath,
      manifestPath
    },
    provider,
    model,
    status,
    requiresConsultantValidation,
    guardrailsApplied,
    guardrails: {
      requiredSections,
      requiredGuardrailPhrases,
      forbiddenPhrases
    },
    checks: {
      draftGenerated: true,
      manifestGenerated: true,
      externalProviderRequired: false,
      consultantOnlyDraft: true,
      readyForBeneficiaryDelivery: false
    },
    compatibility: {
      v1ChainRequired: false,
      aiOptional: true,
      canRunWithoutApiKey: true
    }
  };
}

function renderTraceability(snapshot, generatedAt) {
  return [
    `- Session : ${snapshot.sessionId ?? 'n/a'}`,
    `- Bénéficiaire : ${snapshot.beneficiaryId ?? 'n/a'}`,
    `- Consultant : ${snapshot.consultantId ?? 'n/a'}`,
    `- Source : ${snapshot.id ?? 'analysis-snapshot'}`,
    `- Statut source : ${snapshot.status ?? 'n/a'}`,
    `- Génération IA : ${generatedAt}`,
    `- Provider : ${provider}`,
    '- Modèle : aucun fournisseur externe',
    `- Statut : ${status}`,
    '- Validation consultant obligatoire : oui'
  ].join('\n');
}

function renderNeutralSummary(indicators, highlights, risks) {
  const available = indicators.filter(Boolean);

  if (available.length === 0) {
    return [
      '### Lecture factuelle',
      '',
      'Les données disponibles ne permettent pas encore de produire une synthèse neutre exploitable. Ce point mérite validation avec le consultant.',
      '',
      '### Éléments observables',
      '',
      '- Aucun indicateur exploitable n est disponible dans le snapshot.',
      '',
      '### Points à ne pas surinterpréter',
      '',
      '- Ne pas déduire une orientation professionnelle à partir d une matière insuffisante.'
    ].join('\n');
  }

  return [
    '### Lecture factuelle',
    '',
    'Les réponses suggèrent une première matière de travail pour préparer la lecture consultant.',
    '',
    'Cette synthèse reste descriptive : elle regroupe des signaux observables sans produire de conclusion définitive.',
    '',
    '### Éléments observables',
    '',
    ...available.map(item => `- ${item.label} : ${formatValue(item.value, item.unit)}. ${item.interpretation ?? ''}`.trim()),
    '',
    `- Nombre de thèmes détectés : ${highlights.length}.`,
    `- Nombre de points de vigilance détectés : ${risks.length}.`,
    '',
    '### Points à ne pas surinterpréter',
    '',
    '- Un score ou un indicateur ne suffit pas à conclure sur un projet professionnel.',
    '- Un thème récurrent doit être confirmé par le bénéficiaire avant d être repris dans la synthèse finale.',
    '- Une contrainte exprimée doit être distinguée d une contrainte réellement non négociable.',
    '',
    'Cette lecture doit être complétée par une analyse qualitative humaine.'
  ].join('\n');
}

function renderRecurringThemes(highlights) {
  if (highlights.length === 0) {
    return [
      'Aucun thème récurrent automatique n a été identifié.',
      '',
      '### Utilisation en entretien',
      '',
      '- Le consultant pourra explorer les réponses longues pour repérer les thèmes réellement structurants.',
      '- Le consultant pourra demander au bénéficiaire quels sujets reviennent spontanément dans son récit.'
    ].join('\n');
  }

  return highlights.map(highlight => [
    `### ${highlight.label}`,
    '',
    '#### Signal observé',
    '',
    `- Occurrences détectées : ${highlight.count ?? 0}`,
    `- Code source : ${highlight.code ?? 'n/a'}`,
    '',
    '#### Lecture prudente',
    '',
    `Les réponses suggèrent un thème récurrent autour de : ${highlight.label}.`,
    '',
    'Ce thème doit être confirmé en entretien avant toute reprise dans une synthèse finale.',
    '',
    '#### Exemples sources',
    '',
    renderExamples(highlight.examples ?? []),
    '',
    '#### Utilisation en entretien',
    '',
    `- Vérifier si le thème "${highlight.label}" est réellement important pour le bénéficiaire.`,
    '- Identifier si ce thème correspond à un besoin, une contrainte, une motivation ou une compétence.',
    '- Demander au bénéficiaire de nuancer ou prioriser ce thème.'
  ].join('\n')).join('\n\n');
}

function renderValueHypotheses(highlights) {
  const valuesRelated = highlights.filter(highlight => ['confidence', 'career_evolution'].includes(highlight.code));
  if (valuesRelated.length === 0) {
    return 'Aucune valeur dominante ne peut être isolée automatiquement à ce stade. Une hypothèse possible est de clarifier en entretien ce qui compte réellement pour le bénéficiaire.';
  }

  return valuesRelated.map(highlight => `- Une hypothèse possible est que le thème "${highlight.label}" porte une valeur ou une attente importante à valider en entretien.`).join('\n');
}

function renderMotivationHypotheses(highlights) {
  const motivationRelated = highlights.filter(highlight => ['career_evolution', 'confidence'].includes(highlight.code));
  if (motivationRelated.length === 0) {
    return 'Les motivations apparentes ne ressortent pas suffisamment du signal automatique. Le consultant pourra explorer les envies, les priorités et les critères de choix du bénéficiaire.';
  }

  return motivationRelated.map(highlight => `- Les réponses suggèrent une motivation possible liée à "${highlight.label}". Ce point mérite validation avec le bénéficiaire.`).join('\n');
}

function renderSkills(highlights) {
  const skills = highlights.filter(highlight => highlight.code === 'skills');
  if (skills.length === 0) {
    return 'Aucune compétence forte n est détectée automatiquement. Le consultant pourra compléter cette section à partir des réponses détaillées et de l entretien.';
  }

  return skills.map(highlight => [
    `- Les réponses suggèrent des compétences ou savoir-faire liés à "${highlight.label}".`,
    renderExamples(highlight.examples ?? [])
  ].join('\n')).join('\n\n');
}

function renderConstraints(highlights, risks) {
  const constraints = highlights.filter(highlight => highlight.code === 'constraints');
  const riskLines = risks.map(risk => `- Point de vigilance : ${risk.label}. ${risk.recommendation ?? ''}`.trim());

  if (constraints.length === 0 && riskLines.length === 0) {
    return 'Aucune contrainte majeure n est détectée automatiquement. Ce point doit rester ouvert en entretien.';
  }

  return [
    ...constraints.map(highlight => `- Les réponses suggèrent une contrainte possible autour de "${highlight.label}".`),
    ...riskLines
  ].join('\n');
}

function renderProfessionalHypotheses(highlights, risks) {
  const lines = [];

  if (highlights.some(highlight => highlight.code === 'skills')) {
    lines.push('- Une hypothèse possible est de construire des pistes professionnelles à partir des compétences déjà exprimées.');
  }

  if (highlights.some(highlight => highlight.code === 'career_evolution')) {
    lines.push('- Une hypothèse possible est d explorer une évolution professionnelle progressive plutôt qu une rupture immédiate.');
  }

  if (highlights.some(highlight => highlight.code === 'constraints') || risks.length > 0) {
    lines.push('- Une hypothèse possible est de filtrer les pistes professionnelles avec les contraintes identifiées avant de les approfondir.');
  }

  if (lines.length === 0) {
    lines.push('- Les hypothèses professionnelles ne sont pas suffisamment établies automatiquement. Le consultant pourra les construire après clarification des objectifs et des motivations.');
  }

  return lines.map(line => `${line} Ce point mérite validation.`).join('\n');
}

function renderClarificationPoints(risks, completion, readiness) {
  const points = [];

  if (completion && completion.value < 100) {
    points.push('- Clarifier les formulaires ou réponses manquantes avant de produire une synthèse finale.');
  }

  if (readiness && readiness.value === false) {
    points.push('- Vérifier les prérequis d analyse avant d utiliser les résultats comme base de restitution.');
  }

  for (const risk of risks) {
    points.push(`- Clarifier : ${risk.label}. ${risk.recommendation ?? ''}`.trim());
  }

  if (points.length === 0) {
    points.push('- Clarifier avec le bénéficiaire les priorités, les contraintes réelles et les critères de choix avant toute conclusion.');
  }

  return points.join('\n');
}

function renderInterviewQuestions(prompts, risks) {
  const questions = prompts.map(prompt => `- ${prompt.question}`);

  if (risks.length > 0) {
    questions.push('- Quels points de vigilance doivent être confirmés, nuancés ou écartés avec le bénéficiaire ?');
  }

  questions.push('- Quelles pistes le bénéficiaire reconnaît-il comme réalistes, motivantes et cohérentes avec ses contraintes ?');

  return questions.join('\n');
}

function renderInterpretationRisks(risks) {
  const base = [
    '- Ne pas transformer un thème récurrent en certitude.',
    '- Ne pas confondre hypothèse professionnelle et recommandation définitive.',
    '- Ne pas interpréter une réponse isolée comme un trait stable.',
    '- Toujours valider les points sensibles avec le bénéficiaire.'
  ];

  const detected = risks.map(risk => `- Risque détecté : ${risk.label}. À traiter comme un point de vigilance, pas comme une conclusion.`);

  return [...base, ...detected].join('\n');
}

function renderConsultantPreparation(highlights, risks, prompts, completion, readiness) {
  return [
    '### Priorités d entretien',
    '',
    renderInterviewPriorities(highlights, risks, completion, readiness),
    '',
    '### Hypothèses à valider',
    '',
    renderHypothesesToValidate(highlights, risks),
    '',
    '### Hypothèses à écarter ou nuancer',
    '',
    renderHypothesesToChallenge(risks, completion, readiness),
    '',
    '### Questions ciblées',
    '',
    renderTargetedQuestions(prompts, highlights, risks),
    '',
    '### Points de vigilance consultant',
    '',
    renderConsultantWatchPoints(risks)
  ].join('\n');
}

function renderInterviewPriorities(highlights, risks, completion, readiness) {
  const priorities = [];

  if (completion && completion.value < 100) {
    priorities.push('- Prioriser la vérification des réponses ou formulaires manquants.');
  }

  if (readiness && readiness.value === false) {
    priorities.push('- Confirmer que la matière disponible est suffisante avant toute synthèse.');
  }

  if (highlights.some(highlight => highlight.code === 'skills')) {
    priorities.push('- Prioriser la validation des compétences réellement mobilisées et transférables.');
  }

  if (highlights.some(highlight => highlight.code === 'career_evolution')) {
    priorities.push('- Explorer les attentes d évolution, le rythme souhaité et le niveau de rupture acceptable.');
  }

  if (highlights.some(highlight => highlight.code === 'constraints') || risks.length > 0) {
    priorities.push('- Clarifier les contraintes non négociables avant de retenir des pistes professionnelles.');
  }

  if (priorities.length === 0) {
    priorities.push('- Prioriser la compréhension des motivations, des critères de choix et des éléments à confirmer avec le bénéficiaire.');
  }

  return priorities.join('\n');
}

function renderHypothesesToValidate(highlights, risks) {
  const hypotheses = [];

  for (const highlight of highlights) {
    hypotheses.push(`- Valider si le thème "${highlight.label}" est réellement structurant pour le bénéficiaire.`);
  }

  if (risks.length > 0) {
    hypotheses.push('- Valider si les points de vigilance détectés sont confirmés, secondaires ou à écarter.');
  }

  if (hypotheses.length === 0) {
    hypotheses.push('- Valider les priorités professionnelles avant de formuler des hypothèses de projet.');
  }

  return hypotheses.join('\n');
}

function renderHypothesesToChallenge(risks, completion, readiness) {
  const hypotheses = [];

  if (risks.length > 0) {
    hypotheses.push('- Écarter toute conclusion fondée uniquement sur un risque ou un signal faible.');
  }

  if (completion && completion.value < 100) {
    hypotheses.push('- Nuancer les hypothèses tant que toutes les réponses attendues ne sont pas disponibles.');
  }

  if (readiness && readiness.value === false) {
    hypotheses.push('- Écarter les conclusions de synthèse tant que le snapshot n est pas prêt pour revue.');
  }

  hypotheses.push('- Nuancer toute piste qui ne serait pas reconnue par le bénéficiaire comme réaliste ou motivante.');

  return hypotheses.join('\n');
}

function renderTargetedQuestions(prompts, highlights, risks) {
  const questions = [];

  for (const prompt of prompts.slice(0, 5)) {
    questions.push(`- ${prompt.question}`);
  }

  for (const highlight of highlights.slice(0, 3)) {
    questions.push(`- En quoi le thème "${highlight.label}" est-il important dans votre réflexion actuelle ?`);
  }

  if (risks.length > 0) {
    questions.push('- Quels points de vigilance souhaitez-vous confirmer, corriger ou relativiser ?');
  }

  questions.push('- Quelle première action concrète permettrait de tester une piste sans engagement irréversible ?');

  return questions.join('\n');
}

function renderConsultantWatchPoints(risks) {
  const points = [
    '- Ne pas reprendre une hypothèse sans validation humaine.',
    '- Ne pas transformer une préférence exprimée en recommandation définitive.',
    '- Distinguer les faits, les interprétations et les questions ouvertes.'
  ];

  for (const risk of risks) {
    points.push(`- Vigilance spécifique : ${risk.label}. ${risk.recommendation ?? ''}`.trim());
  }

  return points.join('\n');
}

function renderConsultantValidation() {
  return [
    '- [ ] Relire le brouillon IA.',
    '- [ ] Supprimer ou reformuler les éléments trop affirmatifs.',
    '- [ ] Valider les hypothèses avec les réponses sources.',
    '- [ ] Préparer les questions d entretien.',
    '- [ ] Confirmer les éléments retenus avec le bénéficiaire.',
    '- [ ] Ne reprendre dans la synthèse finale que les éléments validés humainement.',
    '',
    'Ce brouillon ne doit pas être remis tel quel au bénéficiaire.'
  ].join('\n');
}

function defaultManifestPathFrom(outputPath) {
  if (outputPath.endsWith('ai-analysis-draft.md')) {
    return outputPath.replace('ai-analysis-draft.md', 'ai-analysis-manifest.json');
  }

  if (outputPath.endsWith('.md')) {
    return outputPath.replace(/\.md$/, '.manifest.json');
  }

  return `${outputPath}.manifest.json`;
}

function renderExamples(examples) {
  if (examples.length === 0) {
    return 'Aucun exemple source disponible dans le snapshot.';
  }

  return examples.slice(0, 3).map(example => `- ${example.formId} / ${example.questionId} : ${sanitizeInline(example.value)}`).join('\n');
}

function indicator(snapshot, code) {
  return (snapshot.indicators ?? []).find(item => item.code === code) ?? null;
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

function sanitizeInline(value) {
  return String(value ?? '').replaceAll('\n', ' ').trim();
}
