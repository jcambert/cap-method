import fs from 'node:fs';
import path from 'node:path';

const defaultInputPath = 'questionnaire-engine/responses/generated/session.response.normalized.json';
const defaultOutputPath = 'questionnaire-engine/analysis/generated/sample.analysis-snapshot.json';

const inputPath = process.argv[2] ?? defaultInputPath;
const outputPath = process.argv[3] ?? defaultOutputPath;

const session = JSON.parse(fs.readFileSync(inputPath, 'utf8'));
const snapshot = analyzeSession(session);

fs.mkdirSync(path.dirname(outputPath), { recursive: true });
fs.writeFileSync(outputPath, JSON.stringify(snapshot, null, 2), 'utf8');

console.log(`Analysis snapshot: ${snapshot.id}`);
console.log(`Indicators: ${snapshot.indicators.length}`);
console.log(`Highlights: ${snapshot.highlights.length}`);
console.log(`Risks: ${snapshot.risks.length}`);
console.log(`Consultant prompts: ${snapshot.consultantPrompts.length}`);
console.log(`Output: ${outputPath}`);

function analyzeSession(session) {
  const answers = flattenAnswers(session);
  const indicators = buildIndicators(session, answers);
  const highlights = buildHighlights(answers);
  const risks = buildRisks(indicators, answers);
  const consultantPrompts = buildConsultantPrompts(indicators, highlights, risks);

  return {
    id: `${session.id}.analysis-snapshot`,
    sessionId: session.id,
    beneficiaryId: session.beneficiaryId,
    consultantId: session.consultantId,
    generatedAt: new Date().toISOString(),
    source: 'response_session',
    status: session.analysisReadiness?.ready ? 'ready_for_consultant_review' : 'incomplete',
    coverage: {
      formsImported: session.import?.formsImported ?? session.forms?.length ?? 0,
      formsExpected: session.import?.formsExpected ?? null,
      answersImported: answers.length,
      complete: session.completeness?.complete ?? false,
      analysisReady: session.analysisReadiness?.ready ?? false
    },
    indicators,
    highlights,
    risks,
    consultantPrompts,
    traceability: {
      sourceSessionId: session.id,
      formIds: (session.forms ?? []).map(form => form.formId)
    }
  };
}

function flattenAnswers(session) {
  return (session.forms ?? []).flatMap(form => form.answers ?? []);
}

function buildIndicators(session, answers) {
  const numericAnswers = answers.filter(answer => typeof answer.normalizedValue === 'number');
  const rating5Values = numericAnswers.filter(answer => answer.questionType === 'rating5').map(answer => answer.normalizedValue);
  const rating10Values = numericAnswers.filter(answer => answer.questionType === 'rating10').map(answer => answer.normalizedValue);

  return [
    {
      code: 'forms_completion_rate',
      label: 'Taux de completion des formulaires',
      value: completionRate(session),
      unit: 'percent',
      interpretation: session.completeness?.complete ? 'Parcours questionnaire complet.' : 'Parcours questionnaire incomplet.'
    },
    {
      code: 'rating5_average',
      label: 'Moyenne des notes sur 5',
      value: averageOrNull(rating5Values),
      unit: 'score_1_5',
      interpretation: ratingInterpretation(averageOrNull(rating5Values), 5)
    },
    {
      code: 'rating10_average',
      label: 'Moyenne des notes sur 10',
      value: averageOrNull(rating10Values),
      unit: 'score_1_10',
      interpretation: ratingInterpretation(averageOrNull(rating10Values), 10)
    },
    {
      code: 'long_text_answer_count',
      label: 'Nombre de réponses longues',
      value: answers.filter(answer => answer.questionType === 'longText' && hasText(answer.normalizedValue)).length,
      unit: 'count',
      interpretation: 'Volume de matière qualitative disponible pour la synthèse.'
    },
    {
      code: 'analysis_readiness',
      label: 'Prêt pour analyse',
      value: session.analysisReadiness?.ready === true,
      unit: 'boolean',
      interpretation: session.analysisReadiness?.ready ? 'Les prérequis minimaux sont présents.' : 'Des éléments bloquent encore l analyse.'
    }
  ];
}

function buildHighlights(answers) {
  const keywords = [
    { code: 'skills', label: 'Compétences exprimées', patterns: ['competence', 'compétence', 'maitrise', 'maîtrise', 'expertise'] },
    { code: 'career_evolution', label: 'Évolution professionnelle', patterns: ['evolution', 'évolution', 'poste', 'role', 'rôle', 'projet'] },
    { code: 'constraints', label: 'Contraintes identifiées', patterns: ['contrainte', 'familiale', 'geographique', 'géographique', 'temps'] },
    { code: 'confidence', label: 'Confiance et projection', patterns: ['confiance', 'projette', 'projection', 'objectif'] }
  ];

  const textAnswers = answers.filter(answer => typeof answer.normalizedValue === 'string' && answer.normalizedValue.trim().length > 0);
  const highlights = [];

  for (const keyword of keywords) {
    const matches = textAnswers.filter(answer => containsAny(answer.normalizedValue, keyword.patterns));
    if (matches.length > 0) {
      highlights.push({
        code: keyword.code,
        label: keyword.label,
        count: matches.length,
        examples: matches.slice(0, 3).map(answer => ({
          formId: answer.formId,
          questionId: answer.questionId,
          questionLabel: answer.questionLabel,
          value: answer.normalizedValue
        }))
      });
    }
  }

  return highlights;
}

function buildRisks(indicators, answers) {
  const risks = [];
  const rating5 = indicators.find(indicator => indicator.code === 'rating5_average')?.value;
  const rating10 = indicators.find(indicator => indicator.code === 'rating10_average')?.value;

  if (rating5 !== null && rating5 <= 2.5) {
    risks.push({
      code: 'low_rating5_average',
      label: 'Moyenne faible sur les questions notées sur 5',
      severity: 'medium',
      recommendation: 'Explorer les causes de faible satisfaction ou de faible confiance.'
    });
  }

  if (rating10 !== null && rating10 <= 5) {
    risks.push({
      code: 'low_rating10_average',
      label: 'Moyenne faible sur les questions notées sur 10',
      severity: 'medium',
      recommendation: 'Identifier les freins majeurs et les besoins d accompagnement.'
    });
  }

  const lowMaterial = answers.filter(answer => answer.questionType === 'longText' && hasText(answer.normalizedValue)).length < 3;
  if (lowMaterial) {
    risks.push({
      code: 'low_qualitative_material',
      label: 'Matière qualitative limitée',
      severity: 'low',
      recommendation: 'Prévoir des relances en entretien pour enrichir la compréhension.'
    });
  }

  return risks;
}

function buildConsultantPrompts(indicators, highlights, risks) {
  const prompts = [];

  prompts.push({
    code: 'clarify_expectations',
    question: 'Quelles attentes doivent absolument être clarifiées lors du prochain entretien ?',
    source: 'analysis_baseline'
  });

  prompts.push({
    code: 'validate_strengths',
    question: 'Quelles compétences fortes semblent confirmées par les réponses ?',
    source: 'analysis_baseline'
  });

  if (highlights.some(highlight => highlight.code === 'constraints')) {
    prompts.push({
      code: 'explore_constraints',
      question: 'Quelles contraintes risquent de limiter les pistes professionnelles ?',
      source: 'detected_constraints'
    });
  }

  if (risks.length > 0) {
    prompts.push({
      code: 'handle_risks',
      question: 'Quels signaux faibles ou points de vigilance doivent être traités en priorité ?',
      source: 'detected_risks'
    });
  }

  return prompts;
}

function completionRate(session) {
  const imported = session.import?.formsImported ?? session.forms?.length ?? 0;
  const expected = session.import?.formsExpected ?? imported;
  if (expected === 0) {
    return 0;
  }
  return Math.round((imported / expected) * 100);
}

function averageOrNull(values) {
  if (values.length === 0) {
    return null;
  }
  const total = values.reduce((sum, value) => sum + value, 0);
  return Math.round((total / values.length) * 100) / 100;
}

function ratingInterpretation(value, max) {
  if (value === null) {
    return 'Aucune note disponible.';
  }

  const ratio = value / max;
  if (ratio >= 0.75) {
    return 'Signal globalement favorable.';
  }
  if (ratio >= 0.5) {
    return 'Signal intermédiaire à approfondir.';
  }
  return 'Signal faible ou point de vigilance.';
}

function containsAny(value, patterns) {
  const normalized = normalizeText(value);
  return patterns.some(pattern => normalized.includes(normalizeText(pattern)));
}

function normalizeText(value) {
  return String(value ?? '')
    .toLowerCase()
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '');
}

function hasText(value) {
  return typeof value === 'string' && value.trim().length > 0;
}
