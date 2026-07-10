import fs from 'node:fs';
import path from 'node:path';

const defaultSnapshotPath = 'questionnaire-engine/analysis/generated/sample.analysis-snapshot.json';
const defaultDraftPath = 'questionnaire-engine/synthesis/generated/sample.synthesis-draft.md';
const defaultOutputPath = 'questionnaire-engine/synthesis/generated/sample.final-synthesis.md';

const snapshotPath = process.argv[2] ?? defaultSnapshotPath;
const draftPath = process.argv[3] ?? defaultDraftPath;
const outputPath = process.argv[4] ?? defaultOutputPath;

const snapshot = JSON.parse(fs.readFileSync(snapshotPath, 'utf8'));
const draft = fs.readFileSync(draftPath, 'utf8');
const markdown = renderFinalSynthesis(snapshot, draft);

fs.mkdirSync(path.dirname(outputPath), { recursive: true });
fs.writeFileSync(outputPath, markdown, 'utf8');

console.log(`Final synthesis: ${outputPath}`);
console.log(`Snapshot: ${snapshot.id}`);
console.log(`Status: ${snapshot.status}`);

function renderFinalSynthesis(snapshot, draft) {
  const completion = indicator(snapshot, 'forms_completion_rate');
  const rating5 = indicator(snapshot, 'rating5_average');
  const rating10 = indicator(snapshot, 'rating10_average');
  const longTextCount = indicator(snapshot, 'long_text_answer_count');

  return [
    '# Synthèse finale structurée',
    '',
    '## Statut',
    '',
    'Ce document est une synthèse finale structurée générée à partir du brouillon consultant et de l analyse initiale.',
    '',
    'Il doit être relu, complété et validé par le consultant avant toute remise au bénéficiaire.',
    '',
    '## 1. Rappel du contexte',
    '',
    `Cette synthèse concerne la session ${snapshot.sessionId} du bénéficiaire ${snapshot.beneficiaryId}.`,
    '',
    `La couverture du parcours est de ${valueOrFallback(completion?.value, 'n/a')} % avec ${snapshot.coverage?.formsImported ?? 'n/a'} formulaire(s) importé(s) sur ${snapshot.coverage?.formsExpected ?? 'n/a'}.`,
    '',
    '## 2. Objectifs du bilan',
    '',
    'À compléter par le consultant à partir de l entretien initial et des réponses du bénéficiaire :',
    '',
    '- clarifier la demande initiale ;',
    '- préciser les attentes professionnelles ;',
    '- identifier les enjeux personnels et professionnels ;',
    '- formuler les objectifs de travail retenus.',
    '',
    '## 3. Parcours et éléments recueillis',
    '',
    `Le parcours exploité contient ${snapshot.coverage?.answersImported ?? 'n/a'} réponse(s) normalisée(s).`,
    '',
    `Nombre de réponses longues exploitables : ${valueOrFallback(longTextCount?.value, 'n/a')}.`,
    '',
    'Les éléments recueillis devront être consolidés par le consultant afin de distinguer les faits, les perceptions, les hypothèses et les pistes à valider.',
    '',
    '## 4. Compétences identifiées',
    '',
    renderHighlightSection(snapshot, 'skills', 'Aucune compétence spécifique n a encore été isolée automatiquement. Le consultant doit compléter cette section avec les réalisations, savoir-faire et preuves observables.'),
    '',
    '## 5. Valeurs, besoins et motivations',
    '',
    'À compléter à partir des formulaires sur les valeurs, besoins, motivations et intérêts professionnels.',
    '',
    'Points à documenter :',
    '',
    '- valeurs professionnelles importantes ;',
    '- besoins de fonctionnement ;',
    '- moteurs de motivation ;',
    '- environnements favorables ;',
    '- éléments non négociables.',
    '',
    '## 6. Freins et contraintes',
    '',
    renderHighlightSection(snapshot, 'constraints', 'Aucune contrainte explicite n a été détectée automatiquement. Le consultant doit confirmer les contraintes réelles avec le bénéficiaire.'),
    '',
    renderRisks(snapshot.risks ?? []),
    '',
    '## 7. Pistes professionnelles étudiées',
    '',
    renderHighlightSection(snapshot, 'career_evolution', 'Les pistes professionnelles doivent être précisées par le consultant à partir des réponses et des entretiens.'),
    '',
    'Pour chaque piste, compléter :',
    '',
    '- intérêt de la piste ;',
    '- cohérence avec les compétences ;',
    '- cohérence avec les valeurs et besoins ;',
    '- conditions de faisabilité ;',
    '- risques ou écarts à combler.',
    '',
    '## 8. Projet retenu',
    '',
    'À compléter après arbitrage avec le bénéficiaire.',
    '',
    'Le projet retenu doit être formulé de manière claire, réaliste et actionnable.',
    '',
    '## 9. Conditions de réussite',
    '',
    'Conditions à vérifier :',
    '',
    '- niveau de confiance ;',
    '- ressources disponibles ;',
    '- contraintes personnelles et professionnelles ;',
    '- compétences à renforcer ;',
    '- démarches à engager ;',
    '- temporalité réaliste.',
    '',
    `Moyenne des notes sur 5 : ${formatIndicator(rating5)}.`,
    '',
    `Moyenne des notes sur 10 : ${formatIndicator(rating10)}.`,
    '',
    '## 10. Conclusion consultant',
    '',
    'À rédiger par le consultant après validation des informations avec le bénéficiaire.',
    '',
    'La conclusion doit rappeler :',
    '',
    '- la demande initiale ;',
    '- les ressources principales ;',
    '- les pistes écartées ou retenues ;',
    '- le projet cible ;',
    '- les prochaines actions.',
    '',
    '## 11. Points à valider avant remise',
    '',
    '- [ ] Les informations factuelles sont exactes',
    '- [ ] Le bénéficiaire reconnaît la synthèse comme fidèle',
    '- [ ] Les pistes sont formulées sans sur-promesse',
    '- [ ] Les contraintes sont explicites',
    '- [ ] Le plan d action est cohérent avec le projet retenu',
    '- [ ] La synthèse est validée par le consultant',
    '',
    '## Annexe - Questions de préparation consultant',
    '',
    renderConsultantPrompts(snapshot.consultantPrompts ?? []),
    '',
    '## Annexe - Source brouillon consultant',
    '',
    '<details>',
    '<summary>Afficher le brouillon utilisé comme source</summary>',
    '',
    '```markdown',
    trimForAppendix(draft),
    '```',
    '',
    '</details>'
  ].join('\n');
}

function indicator(snapshot, code) {
  return (snapshot.indicators ?? []).find(item => item.code === code) ?? null;
}

function renderHighlightSection(snapshot, code, fallback) {
  const highlight = (snapshot.highlights ?? []).find(item => item.code === code);
  if (!highlight) {
    return fallback;
  }

  const examples = (highlight.examples ?? []).map(example => `- ${example.formId} / ${example.questionId} : ${example.value}`).join('\n');

  return [
    `${highlight.label} : ${highlight.count} élément(s) détecté(s).`,
    '',
    examples || fallback
  ].join('\n');
}

function renderRisks(risks) {
  if (risks.length === 0) {
    return 'Aucun point de vigilance automatique majeur n a été détecté. Cette absence ne dispense pas d une revue qualitative.';
  }

  return [
    'Points de vigilance détectés :',
    '',
    ...risks.map(risk => `- ${risk.label} (${risk.severity}) : ${risk.recommendation}`)
  ].join('\n');
}

function renderConsultantPrompts(prompts) {
  if (prompts.length === 0) {
    return 'Aucune question de préparation générée.';
  }

  return prompts.map(prompt => `- ${prompt.question}`).join('\n');
}

function formatIndicator(item) {
  if (!item || item.value === null || item.value === undefined) {
    return 'n/a';
  }

  return `${item.value} (${item.interpretation ?? 'sans interprétation'})`;
}

function valueOrFallback(value, fallback) {
  return value === null || value === undefined ? fallback : value;
}

function trimForAppendix(value) {
  const maxLength = 12000;
  if (value.length <= maxLength) {
    return value;
  }
  return `${value.slice(0, maxLength)}\n\n... contenu tronqué ...`;
}
