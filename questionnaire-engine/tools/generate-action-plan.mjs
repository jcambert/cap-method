import fs from 'node:fs';
import path from 'node:path';

const defaultSnapshotPath = 'questionnaire-engine/analysis/generated/sample.analysis-snapshot.json';
const defaultFinalSynthesisPath = 'questionnaire-engine/synthesis/generated/sample.final-synthesis.md';
const defaultOutputPath = 'questionnaire-engine/synthesis/generated/sample.action-plan.md';

const snapshotPath = process.argv[2] ?? defaultSnapshotPath;
const finalSynthesisPath = process.argv[3] ?? defaultFinalSynthesisPath;
const outputPath = process.argv[4] ?? defaultOutputPath;

const snapshot = JSON.parse(fs.readFileSync(snapshotPath, 'utf8'));
const finalSynthesis = fs.readFileSync(finalSynthesisPath, 'utf8');
const markdown = renderActionPlan(snapshot, finalSynthesis);

fs.mkdirSync(path.dirname(outputPath), { recursive: true });
fs.writeFileSync(outputPath, markdown, 'utf8');

console.log(`Action plan: ${outputPath}`);
console.log(`Snapshot: ${snapshot.id}`);
console.log(`Status: ${snapshot.status}`);

function renderActionPlan(snapshot, finalSynthesis) {
  const rating5 = indicator(snapshot, 'rating5_average');
  const rating10 = indicator(snapshot, 'rating10_average');
  const completion = indicator(snapshot, 'forms_completion_rate');
  const risks = snapshot.risks ?? [];
  const prompts = snapshot.consultantPrompts ?? [];

  return [
    '# Plan d action professionnel',
    '',
    '## Statut',
    '',
    'Ce document est un plan d action structuré généré à partir de la synthèse finale et de l analyse initiale.',
    '',
    'Il doit être ajusté et validé avec le bénéficiaire avant toute utilisation comme support final.',
    '',
    '## 1. Projet professionnel cible',
    '',
    'À compléter avec le projet retenu dans la synthèse finale.',
    '',
    '- Projet cible : à préciser',
    '- Métier / rôle visé : à préciser',
    '- Secteur ou environnement cible : à préciser',
    '- Niveau de priorité : à préciser',
    '- Horizon souhaité : à préciser',
    '',
    '## 2. Objectif général',
    '',
    'Formuler ici l objectif professionnel principal sous une forme claire, réaliste et mesurable.',
    '',
    'Exemple de formulation à adapter :',
    '',
    '> Construire et sécuriser une évolution professionnelle cohérente avec les compétences, les valeurs, les contraintes et les motivations identifiées pendant le bilan.',
    '',
    '## 3. Indicateurs de départ',
    '',
    renderInitialIndicators(completion, rating5, rating10, snapshot),
    '',
    '## 4. Actions court terme - 0 à 30 jours',
    '',
    renderActions([
      action('Clarifier le projet retenu', 'Reformuler le projet cible en une phrase simple et validée avec le consultant.', 'Bénéficiaire + consultant', 'Projet formulé par écrit'),
      action('Identifier les informations manquantes', 'Lister les points encore incertains : métier, secteur, contraintes, compétences à renforcer.', 'Bénéficiaire', 'Liste de questions ouverte'),
      action('Préparer les premières enquêtes métier', 'Identifier 3 personnes ou sources fiables à contacter.', 'Bénéficiaire', '3 contacts ou sources identifiés')
    ]),
    '',
    '## 5. Actions moyen terme - 1 à 3 mois',
    '',
    renderActions([
      action('Réaliser les enquêtes métier', 'Conduire les échanges ou recherches prévues pour valider la réalité du projet.', 'Bénéficiaire', 'Au moins 2 retours exploitables'),
      action('Évaluer les écarts de compétences', 'Comparer les compétences actuelles avec les exigences du projet cible.', 'Bénéficiaire + consultant', 'Liste des écarts priorisés'),
      action('Construire un scénario de transition', 'Définir un chemin réaliste : formation, mobilité, candidature, projet interne ou reconversion.', 'Bénéficiaire + consultant', 'Scénario documenté')
    ]),
    '',
    '## 6. Actions long terme - 3 à 6 mois',
    '',
    renderActions([
      action('Engager les démarches structurantes', 'Lancer les démarches validées : formation, réseau, candidature, immersion ou mobilité.', 'Bénéficiaire', 'Démarches engagées'),
      action('Suivre les avancées', 'Mettre à jour le plan d action et mesurer les progrès réalisés.', 'Bénéficiaire', 'Point de suivi mensuel'),
      action('Ajuster le projet si nécessaire', 'Réviser le projet en fonction des retours terrain et contraintes réelles.', 'Bénéficiaire + consultant', 'Projet ajusté si besoin')
    ]),
    '',
    '## 7. Compétences à renforcer',
    '',
    'À compléter après validation du projet cible :',
    '',
    '| Compétence | Niveau actuel | Niveau attendu | Action prévue | Échéance |',
    '| --- | --- | --- | --- | --- |',
    '| À compléter | À compléter | À compléter | À compléter | À compléter |',
    '',
    '## 8. Formations ou ressources possibles',
    '',
    'À compléter selon les écarts constatés :',
    '',
    '- formation courte ;',
    '- certification ;',
    '- mentorat ;',
    '- lecture ou ressource métier ;',
    '- immersion ou observation terrain ;',
    '- accompagnement complémentaire.',
    '',
    '## 9. Réseau et enquêtes métier',
    '',
    '| Contact / source | Objectif | Date cible | Résultat attendu | Statut |',
    '| --- | --- | --- | --- | --- |',
    '| À compléter | Valider la réalité du métier | À compléter | Retour terrain | À planifier |',
    '',
    '## 10. Contraintes et points de vigilance',
    '',
    renderRisks(risks),
    '',
    '## 11. Conditions de réussite',
    '',
    '- Le projet est formulé clairement.',
    '- Les contraintes sont explicites et traitées.',
    '- Les compétences nécessaires sont identifiées.',
    '- Les actions sont datées et réalistes.',
    '- Le bénéficiaire sait quelle première action engager.',
    '- Un point de suivi est prévu.',
    '',
    '## 12. Échéancier synthétique',
    '',
    '| Période | Priorité | Résultat attendu |',
    '| --- | --- | --- |',
    '| 0 à 30 jours | Clarification et validation initiale | Projet cible formulé et questions ouvertes identifiées |',
    '| 1 à 3 mois | Investigation et scénario | Retours terrain, écarts de compétences, scénario de transition |',
    '| 3 à 6 mois | Mise en mouvement | Démarches engagées et projet ajusté |',
    '',
    '## 13. Questions de suivi',
    '',
    renderPrompts(prompts),
    '',
    '## 14. Validation du plan',
    '',
    '- [ ] Le projet cible est validé avec le bénéficiaire',
    '- [ ] Les actions sont réalistes',
    '- [ ] Les échéances sont adaptées',
    '- [ ] Les contraintes sont prises en compte',
    '- [ ] Les critères de réussite sont clairs',
    '- [ ] Le bénéficiaire connaît sa première action',
    '',
    '## Annexe - Source synthèse finale',
    '',
    '<details>',
    '<summary>Afficher la synthèse finale utilisée comme source</summary>',
    '',
    '```markdown',
    trimForAppendix(finalSynthesis),
    '```',
    '',
    '</details>'
  ].join('\n');
}

function indicator(snapshot, code) {
  return (snapshot.indicators ?? []).find(item => item.code === code) ?? null;
}

function renderInitialIndicators(completion, rating5, rating10, snapshot) {
  return [
    '| Indicateur | Valeur | Lecture |',
    '| --- | --- | --- |',
    `| Couverture du parcours | ${formatValue(completion?.value, '%')} | ${completion?.interpretation ?? 'À vérifier'} |`,
    `| Moyenne notes sur 5 | ${formatValue(rating5?.value)} | ${rating5?.interpretation ?? 'À vérifier'} |`,
    `| Moyenne notes sur 10 | ${formatValue(rating10?.value)} | ${rating10?.interpretation ?? 'À vérifier'} |`,
    `| Réponses importées | ${snapshot.coverage?.answersImported ?? 'n/a'} | Volume de données exploité |`
  ].join('\n');
}

function action(title, description, owner, successCriteria) {
  return { title, description, owner, successCriteria };
}

function renderActions(actions) {
  return [
    '| Action | Description | Responsable | Critère de réussite |',
    '| --- | --- | --- | --- |',
    ...actions.map(item => `| ${item.title} | ${item.description} | ${item.owner} | ${item.successCriteria} |`)
  ].join('\n');
}

function renderRisks(risks) {
  if (risks.length === 0) {
    return 'Aucun point de vigilance automatique majeur n a été détecté. Le consultant doit néanmoins vérifier les contraintes réelles avec le bénéficiaire.';
  }

  return [
    '| Point de vigilance | Sévérité | Action de sécurisation |',
    '| --- | --- | --- |',
    ...risks.map(risk => `| ${risk.label} | ${risk.severity} | ${risk.recommendation} |`)
  ].join('\n');
}

function renderPrompts(prompts) {
  if (prompts.length === 0) {
    return 'Aucune question de suivi générée automatiquement.';
  }

  return prompts.map(prompt => `- ${prompt.question}`).join('\n');
}

function formatValue(value, suffix = '') {
  if (value === null || value === undefined) {
    return 'n/a';
  }

  return `${value}${suffix}`;
}

function trimForAppendix(value) {
  const maxLength = 12000;
  if (value.length <= maxLength) {
    return value;
  }
  return `${value.slice(0, maxLength)}\n\n... contenu tronqué ...`;
}
