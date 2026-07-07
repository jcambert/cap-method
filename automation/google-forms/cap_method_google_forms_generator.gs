/**
 * CAP Method® - Google Forms Generator
 * Version: 1.0.0
 *
 * Creates the complete Google Forms suite for CAP Method®:
 * - 10 Google Forms
 * - 10 linked Google Sheets response files
 * - 1 Google Drive folder containing every generated resource
 *
 * Usage:
 * 1. Open https://script.google.com/
 * 2. Create a new project
 * 3. Paste this file into Code.gs
 * 4. Run createCapMethodFormsSuite()
 * 5. Accept Google permissions
 * 6. Read the execution logs to retrieve form and sheet URLs
 */

const CAP_METHOD_CONFIG = {
  brandName: 'CAP Method®',
  folderName: 'CAP Method - Google Forms Suite',
  collectEmail: true,
  requireLogin: false,
  allowResponseEdits: true,
  showProgressBar: true,
  confirmationMessage: 'Merci pour vos réponses. Votre consultant CAP Method® pourra les utiliser pour préparer et personnaliser votre accompagnement.'
};

function createCapMethodFormsSuite() {
  const folder = getOrCreateFolder_(CAP_METHOD_CONFIG.folderName);
  const results = [];
  const definitions = [
    form001_(), form002_(), form003_(), form004_(), form005_(),
    form006_(), form007_(), form008_(), form009_(), form010_()
  ];

  definitions.forEach(function(definition) {
    const form = createForm_(definition);
    const sheet = SpreadsheetApp.create(definition.code + ' - Réponses - ' + definition.title);
    form.setDestination(FormApp.DestinationType.SPREADSHEET, sheet.getId());
    moveFileToFolder_(DriveApp.getFileById(form.getId()), folder);
    moveFileToFolder_(DriveApp.getFileById(sheet.getId()), folder);
    results.push({
      code: definition.code,
      title: definition.title,
      editUrl: form.getEditUrl(),
      publicUrl: form.getPublishedUrl(),
      sheetUrl: sheet.getUrl()
    });
  });

  Logger.log('CAP Method® - Google Forms Suite created successfully.');
  Logger.log(JSON.stringify(results, null, 2));
  return results;
}

function createForm_(definition) {
  const form = FormApp.create(definition.code + ' - ' + definition.title);
  form.setDescription(definition.description || '');
  form.setCollectEmail(CAP_METHOD_CONFIG.collectEmail);
  form.setRequireLogin(CAP_METHOD_CONFIG.requireLogin);
  form.setAllowResponseEdits(CAP_METHOD_CONFIG.allowResponseEdits);
  form.setProgressBar(CAP_METHOD_CONFIG.showProgressBar);
  form.setConfirmationMessage(CAP_METHOD_CONFIG.confirmationMessage);

  definition.sections.forEach(function(section, sectionIndex) {
    if (sectionIndex === 0) {
      form.addSectionHeaderItem().setTitle(section.title).setHelpText(section.description || '');
    } else {
      form.addPageBreakItem().setTitle(section.title).setHelpText(section.description || '');
    }
    section.questions.forEach(function(question) { addQuestion_(form, question); });
  });
  return form;
}

function addQuestion_(form, question) {
  let item;
  if (question.type === 'TEXT') item = form.addTextItem();
  if (question.type === 'PARAGRAPH') item = form.addParagraphTextItem();
  if (question.type === 'DATE') item = form.addDateItem();
  if (question.type === 'MULTIPLE_CHOICE') item = form.addMultipleChoiceItem().setChoiceValues(question.choices || []);
  if (question.type === 'CHECKBOX') item = form.addCheckboxItem().setChoiceValues(question.choices || []);
  if (question.type === 'LIST') item = form.addListItem().setChoiceValues(question.choices || []);
  if (question.type === 'SCALE_10') item = form.addScaleItem().setBounds(1, 10).setLabels(question.lowLabel || '1', question.highLabel || '10');
  if (question.type === 'GRID_10') item = form.addGridItem().setRows(question.rows || []).setColumns(['1','2','3','4','5','6','7','8','9','10']);
  if (!item) throw new Error('Unsupported question type: ' + question.type);
  item.setTitle(question.title);
  if (question.helpText) item.setHelpText(question.helpText);
  item.setRequired(Boolean(question.required));
}

function getOrCreateFolder_(folderName) {
  const folders = DriveApp.getFoldersByName(folderName);
  return folders.hasNext() ? folders.next() : DriveApp.createFolder(folderName);
}

function moveFileToFolder_(file, folder) {
  folder.addFile(file);
  DriveApp.getRootFolder().removeFile(file);
}

function text_(title, required) { return { type: 'TEXT', title: title, required: required !== false }; }
function optionalText_(title) { return { type: 'TEXT', title: title, required: false }; }
function paragraph_(title, required) { return { type: 'PARAGRAPH', title: title, required: required !== false }; }
function optionalParagraph_(title) { return { type: 'PARAGRAPH', title: title, required: false }; }
function scale_(title, lowLabel, highLabel, required) { return { type: 'SCALE_10', title: title, lowLabel: lowLabel, highLabel: highLabel, required: required !== false }; }
function grid_(title, rows) { return { type: 'GRID_10', title: title, rows: rows, required: true }; }
function choice_(title, choices, required) { return { type: 'MULTIPLE_CHOICE', title: title, choices: choices, required: required !== false }; }
function checks_(title, choices, required) { return { type: 'CHECKBOX', title: title, choices: choices, required: required !== false }; }
function list_(title, choices, required) { return { type: 'LIST', title: title, choices: choices, required: required !== false }; }

function form001_() { return {
  code: 'FORM-001', title: 'Diagnostic initial du bénéficiaire',
  description: 'Questionnaire de préparation du premier entretien CAP Method®.',
  sections: [
    { title: 'Bienvenue', description: 'Temps estimé : 20 à 30 minutes. Les informations recueillies sont confidentielles.', questions: [] },
    { title: 'Vos informations', questions: [text_('Nom'), text_('Prénom'), text_('Adresse e-mail'), optionalText_('Téléphone'), { type: 'DATE', title: 'Date de naissance', required: false }, optionalText_('Ville de résidence'), list_('Niveau de diplôme le plus élevé obtenu', ['Aucun diplôme','CAP / BEP','Bac','Bac +2','Bac +3','Bac +5','Doctorat','Autre'], false)] },
    { title: 'Votre situation professionnelle', questions: [choice_('Quelle est votre situation actuelle ?', ['Salarié(e)','Fonctionnaire','Travailleur indépendant','Demandeur d’emploi','Étudiant(e)','En reconversion','En arrêt de travail','Autre']), text_('Poste actuel ou dernière fonction'), optionalText_('Secteur d’activité'), optionalText_('Ancienneté dans ce poste'), optionalText_('Ancienneté dans l’entreprise actuelle'), choice_('Effectif approximatif de l’entreprise', ['Moins de 10 salariés','10 à 49','50 à 249','250 à 999','1 000 salariés et plus','Je ne sais pas'], false)] },
    { title: 'Satisfaction professionnelle', questions: [grid_('Indiquez votre niveau actuel pour chaque élément.', ['Satisfaction au travail','Motivation','Reconnaissance','Perspectives d’évolution','Équilibre vie professionnelle / vie personnelle','Sentiment d’utilité','Confiance dans l’avenir professionnel'])] },
    { title: 'Raisons de la démarche', questions: [checks_('Pourquoi souhaitez-vous réaliser un bilan de compétences ?', ['Évoluer professionnellement','Changer de métier','Retrouver du sens','Préparer une reconversion','Préparer une formation','Créer ou reprendre une entreprise','Faire le point sur mon parcours','Gagner en confiance','Préparer une mobilité interne','Préparer une mobilité externe','Autre']), paragraph_('Expliquez ce qui vous a conduit à entreprendre cette démarche.')] },
    { title: 'Attentes', questions: [paragraph_('Qu’attendez-vous principalement de ce bilan de compétences ?'), paragraph_('À la fin de cet accompagnement, quels résultats souhaiteriez-vous avoir obtenus ?')] },
    { title: 'Compétences et réussites', questions: [paragraph_('Quelles sont aujourd’hui vos principales compétences ?'), paragraph_('Quelles sont les réalisations professionnelles dont vous êtes le plus fier ?'), optionalParagraph_('Quelles activités vous procurent le plus de satisfaction ?')] },
    { title: 'Difficultés et freins', questions: [checks_('Quels obstacles freinent votre évolution professionnelle ?', ['Manque de confiance','Manque d’expérience','Manque de diplôme ou certification','Difficultés financières','Contraintes familiales','Mobilité géographique','Santé','Manque d’information','Difficulté à décider','Autre'], false), optionalParagraph_('Souhaitez-vous apporter des précisions ?')] },
    { title: 'Projection', questions: [grid_('Votre perception actuelle', ['Je connais bien mes compétences','Je connais clairement mes motivations','Je sais vers quel projet professionnel évoluer','Je me sens capable de réussir mon projet','Je suis prêt(e) à m’investir dans ce bilan']), optionalParagraph_('Sujet prioritaire à aborder au premier entretien'), optionalParagraph_('Information complémentaire pour le consultant')] }
  ]
}; }

function form002_() { return {
  code: 'FORM-002', title: 'Valeurs professionnelles', description: 'Identifier les valeurs prioritaires qui guideront le projet professionnel.',
  sections: [
    { title: 'Sélection des valeurs', questions: [checks_('Sélectionnez les valeurs les plus importantes pour vous.', values_()), paragraph_('Expliquez pourquoi ces valeurs sont importantes dans votre vie professionnelle.')] },
    { title: 'Priorisation', questions: [optionalText_('Valeur prioritaire n°1'), optionalText_('Valeur prioritaire n°2'), optionalText_('Valeur prioritaire n°3'), optionalText_('Valeur prioritaire n°4'), optionalText_('Valeur prioritaire n°5'), paragraph_('Quelles valeurs sont aujourd’hui insuffisamment respectées dans votre situation actuelle ?')] },
    { title: 'Projection', questions: [paragraph_('Dans quel environnement vos valeurs pourraient-elles être mieux respectées ?'), scale_('Clarté de vos valeurs professionnelles', 'Peu clair', 'Très clair', true)] }
  ]
}; }

function form003_() { return {
  code: 'FORM-003', title: 'Compétences et réalisations', description: 'Recenser les compétences, réussites et compétences à développer.',
  sections: [
    { title: 'Compétences techniques', questions: [paragraph_('Listez vos principales compétences techniques.'), optionalParagraph_('Quels outils, méthodes ou technologies maîtrisez-vous ?')] },
    { title: 'Compétences comportementales', questions: [paragraph_('Listez vos principales compétences comportementales.'), grid_('Auto-évaluation', ['Communication','Organisation','Autonomie','Adaptabilité','Travail en équipe','Leadership','Gestion du stress','Résolution de problèmes'])] },
    { title: 'Réalisations', questions: [paragraph_('Décrivez trois réalisations significatives.'), optionalParagraph_('Quelles compétences avez-vous mobilisées dans ces réussites ?')] },
    { title: 'Développement', questions: [paragraph_('Quelles compétences souhaitez-vous développer ?'), optionalParagraph_('Quelles compétences seront importantes pour votre futur projet ?')] }
  ]
}; }

function form004_() { return {
  code: 'FORM-004', title: 'Personnalité et mode de fonctionnement', description: 'Comprendre le style de travail, de décision, de communication et de gestion du stress.',
  sections: [
    { title: 'Mode de travail', questions: [grid_('Fonctionnement professionnel', ['J’aime travailler de manière autonome','J’aime travailler en équipe','J’ai besoin d’un cadre clair','Je m’adapte facilement aux changements','Je prends facilement des initiatives','Je suis à l’aise avec les responsabilités']), optionalParagraph_('Dans quel environnement êtes-vous le plus efficace ?')] },
    { title: 'Décision et organisation', questions: [grid_('Décision et organisation', ['Je prends mes décisions rapidement','J’ai besoin d’analyser avant d’agir','Je sais prioriser mes tâches','Je respecte facilement les délais','Je garde le cap en période de pression']), optionalParagraph_('Qu’est-ce qui vous aide à prendre une bonne décision ?')] },
    { title: 'Communication et stress', questions: [grid_('Communication et tensions', ['Je communique clairement mes idées','Je sais écouter les autres','Je suis à l’aise dans les échanges difficiles','Je gère correctement mon stress','Je sais demander de l’aide lorsque c’est nécessaire']), optionalParagraph_('Quelles situations professionnelles vous mettent le plus en difficulté ?')] }
  ]
}; }

function form005_() { return {
  code: 'FORM-005', title: 'Intérêts professionnels', description: 'Identifier les activités, missions et environnements professionnels qui attirent le bénéficiaire.',
  sections: [
    { title: 'Activités préférées', questions: [checks_('Quels types d’activités vous attirent le plus ?', ['Analyser','Créer','Organiser','Accompagner','Former','Vendre','Négocier','Produire','Réparer','Concevoir','Diriger','Communiquer','Chercher','Protéger','Soigner','Transmettre','Innover']), paragraph_('Expliquez les activités que vous aimeriez retrouver dans votre futur projet professionnel.')] },
    { title: 'Domaines et environnements', questions: [optionalParagraph_('Quels secteurs d’activité vous intéressent ?'), optionalParagraph_('Quels métiers ou fonctions aimeriez-vous explorer ?'), checks_('Quels environnements de travail préférez-vous ?', ['Petite entreprise','Grande entreprise','Association','Administration','Indépendance','Terrain','Bureau','Télétravail','Hybride','International','Local','Autre'], false)] },
    { title: 'Motivation', questions: [grid_('Niveau d’intérêt', ['Explorer un nouveau métier','Changer de secteur','Monter en responsabilité','Créer une activité','Me spécialiser','Me former','Transmettre mes compétences']), optionalParagraph_('Quelle piste vous attire le plus aujourd’hui, même si elle n’est pas encore réaliste ?')] }
  ]
}; }

function form006_() { return {
  code: 'FORM-006', title: 'Analyse des pistes professionnelles', description: 'Évaluer la motivation, la faisabilité et les risques associés aux pistes envisagées.',
  sections: [
    { title: 'Piste principale', questions: [text_('Nom de la piste professionnelle étudiée'), paragraph_('Décrivez cette piste en quelques lignes.'), grid_('Évaluation de la piste', ['Motivation personnelle','Cohérence avec mes valeurs','Cohérence avec mes compétences','Faisabilité à court terme','Faisabilité financière','Opportunités sur le marché','Niveau d’information disponible','Confiance dans cette piste'])] },
    { title: 'Écarts et besoins', questions: [paragraph_('Quelles compétences possédez-vous déjà pour cette piste ?'), paragraph_('Quelles compétences devez-vous encore acquérir ?'), optionalParagraph_('Quelles formations, rencontres ou recherches sont nécessaires ?')] },
    { title: 'Risques et décision', questions: [optionalParagraph_('Quels sont les principaux risques ou freins ?'), optionalParagraph_('Quelles actions permettraient de sécuriser cette piste ?'), choice_('Souhaitez-vous conserver cette piste pour la suite du bilan ?', ['Oui','Non','À approfondir'])] }
  ]
}; }

function form007_() { return {
  code: 'FORM-007', title: 'Plan d’action professionnel', description: 'Formaliser les objectifs, actions, échéances et ressources nécessaires.',
  sections: [
    { title: 'Objectif principal', questions: [paragraph_('Quel est votre objectif professionnel principal ?'), choice_('Horizon visé', ['Moins de 3 mois','3 à 6 mois','6 à 12 mois','12 à 24 mois','Plus de 24 mois']), scale_('Niveau de clarté de votre objectif', 'Pas clair', 'Très clair', true)] },
    { title: 'Actions à réaliser', questions: [paragraph_('Listez les actions prioritaires à réaliser.'), optionalParagraph_('Quelles démarches administratives, formations ou prises de contact sont nécessaires ?'), optionalParagraph_('Quelles sont les trois premières actions concrètes à mener ?')] },
    { title: 'Ressources et suivi', questions: [optionalParagraph_('De quelles ressources avez-vous besoin ?'), optionalParagraph_('Qui peut vous aider dans la mise en œuvre ?'), optionalParagraph_('Quels indicateurs permettront de vérifier votre progression ?'), scale_('Niveau d’engagement personnel dans ce plan d’action', 'Faible', 'Fort', true)] }
  ]
}; }

function form008_() { return {
  code: 'FORM-008', title: 'Évaluation intermédiaire', description: 'Mesurer la progression à mi-parcours et ajuster l’accompagnement.',
  sections: [
    { title: 'Satisfaction à mi-parcours', questions: [grid_('Évaluation de l’accompagnement', ['Clarté des objectifs','Qualité des échanges','Utilité des exercices','Rythme du parcours','Adaptation à ma situation','Progression ressentie','Satisfaction globale à ce stade']), optionalParagraph_('Qu’est-ce qui vous aide le plus depuis le début du bilan ?')] },
    { title: 'Difficultés et ajustements', questions: [optionalParagraph_('Quelles difficultés rencontrez-vous actuellement ?'), optionalParagraph_('Quels ajustements seraient utiles pour la suite ?'), choice_('Souhaitez-vous aborder un point particulier lors de la prochaine séance ?', ['Oui','Non']), optionalParagraph_('Si oui, lequel ?')] }
  ]
}; }

function form009_() { return {
  code: 'FORM-009', title: 'Évaluation finale du bilan', description: 'Évaluer la satisfaction finale, l’atteinte des objectifs et l’utilité du bilan CAP Method®.',
  sections: [
    { title: 'Satisfaction globale', questions: [grid_('Évaluation finale', ['Satisfaction globale','Atteinte de mes objectifs','Qualité de l’accompagnement','Utilité des outils','Clarté de mon projet','Confiance dans la suite','Capacité à passer à l’action']), scale_('Recommanderiez-vous CAP Method® ?', 'Pas du tout', 'Tout à fait', true)] },
    { title: 'Résultats obtenus', questions: [paragraph_('Quels sont les principaux résultats obtenus grâce au bilan ?'), optionalParagraph_('Qu’est-ce qui a le plus changé dans votre vision professionnelle ?'), optionalParagraph_('Quels outils ou exercices vous ont été les plus utiles ?')] },
    { title: 'Amélioration continue', questions: [optionalParagraph_('Quels points pourraient être améliorés ?'), optionalParagraph_('Souhaitez-vous laisser un témoignage ou un commentaire libre ?')] }
  ]
}; }

function form010_() { return {
  code: 'FORM-010', title: 'Suivi à 6 mois', description: 'Mesurer l’impact du bilan plusieurs mois après sa clôture.',
  sections: [
    { title: 'Situation actuelle', questions: [choice_('Quelle est votre situation actuelle ?', ['Même poste','Nouveau poste','Nouvelle entreprise','Formation en cours','Création ou reprise d’entreprise','Recherche d’emploi','Projet en pause','Autre']), optionalParagraph_('Décrivez brièvement votre situation actuelle.')] },
    { title: 'Mise en œuvre du projet', questions: [grid_('Avancement depuis la fin du bilan', ['Mise en œuvre du plan d’action','Clarté du projet','Motivation','Confiance','Résultats obtenus','Satisfaction professionnelle actuelle']), optionalParagraph_('Quelles actions avez-vous réalisées depuis la fin du bilan ?'), optionalParagraph_('Quels obstacles avez-vous rencontrés ?')] },
    { title: 'Besoins complémentaires', questions: [choice_('Auriez-vous besoin d’un accompagnement complémentaire ?', ['Oui','Non','Peut-être']), optionalParagraph_('Si oui ou peut-être, sur quels sujets ?'), optionalParagraph_('Quel conseil donneriez-vous à une personne qui commence CAP Method® ?')] }
  ]
}; }

function values_() { return ['Autonomie','Créativité','Innovation','Respect','Liberté','Transmission','Équilibre vie professionnelle / vie personnelle','Reconnaissance','Esprit d’équipe','Responsabilité','Sécurité','Apprentissage','Excellence','Solidarité','Engagement','Fiabilité','Bienveillance','Performance','Curiosité','Impact positif']; }
