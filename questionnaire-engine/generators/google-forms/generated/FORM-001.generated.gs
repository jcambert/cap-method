// GENERATED FILE - DO NOT EDIT MANUALLY
// Source: FORM-001.cmdl.yaml
// Version: 0.2.0

function build_FORM_001_(form) {
  form.setTitle('FORM-001 - Diagnostic initial');
  form.setDescription('Generated from CAP Method CMDL.');

  form.addSectionHeaderItem().setTitle('Bienvenue');
  form.addSectionHeaderItem().setTitle('Ce questionnaire prepare le premier entretien CAP Method.');

  form.addSectionHeaderItem().setTitle('Vos informations');
  form.addTextItem().setTitle('Nom').setRequired(true);
  form.addTextItem().setTitle('Prenom').setRequired(true);
  form.addTextItem().setTitle('Adresse e-mail').setRequired(true);
  form.addTextItem().setTitle('Telephone').setRequired(false);

  form.addSectionHeaderItem().setTitle('Situation professionnelle');
  form.addMultipleChoiceItem().setTitle('Situation actuelle').setChoiceValues(['Salarie', 'Independant', 'Demandeur emploi', 'Reconversion', 'Autre']).setRequired(true);
  form.addTextItem().setTitle('Poste actuel ou derniere fonction').setRequired(false);
  form.addTextItem().setTitle('Secteur ou domaine actuel').setRequired(false);
  form.addParagraphTextItem().setTitle('Decrivez brievement votre contexte professionnel actuel').setRequired(false);

  form.addSectionHeaderItem().setTitle('Satisfaction professionnelle');
  form.addScaleItem().setTitle('Satisfaction globale au travail').setBounds(1, 10).setRequired(true);
  form.addScaleItem().setTitle('Confiance dans l avenir professionnel').setBounds(1, 10).setRequired(true);
  form.addParagraphTextItem().setTitle('Qu est ce qui fonctionne bien aujourd hui ?').setRequired(false);
  form.addParagraphTextItem().setTitle('Qu est ce qui ne vous convient plus ?').setRequired(false);

  form.addSectionHeaderItem().setTitle('Raisons de la demarche');
  form.addCheckboxItem().setTitle('Pourquoi souhaitez vous demarrer ce bilan ?').setChoiceValues(['Clarifier un projet', 'Changer de metier', 'Evoluer dans l entreprise', 'Retrouver du sens', 'Valoriser mes competences', 'Preparer une transition', 'Autre']).setRequired(true);
  form.addParagraphTextItem().setTitle('Expliquez votre motivation principale').setRequired(true);

  form.addSectionHeaderItem().setTitle('Attentes');
  form.addParagraphTextItem().setTitle('Qu attendez vous principalement de ce bilan de competences ?').setRequired(true);
  form.addCheckboxItem().setTitle('Quels resultats souhaitez vous obtenir ?').setChoiceValues(['Projet professionnel clair', 'Plan d action concret', 'Meilleure connaissance de soi', 'Reperes sur le marche', 'Confiance renforcee', 'Decision argumentee']).setRequired(false);

  form.addSectionHeaderItem().setTitle('Competences et realisations');
  form.addParagraphTextItem().setTitle('Listez vos principales competences').setRequired(false);
  form.addParagraphTextItem().setTitle('Decrivez une realisation professionnelle dont vous etes fier').setRequired(false);
  form.addParagraphTextItem().setTitle('Quelles competences souhaitez vous developper ?').setRequired(false);

  form.addSectionHeaderItem().setTitle('Points de vigilance');
  form.addCheckboxItem().setTitle('Quels elements peuvent compliquer votre projet ?').setChoiceValues(['Temps disponible', 'Budget', 'Mobilite', 'Confiance', 'Formation', 'Contraintes familiales', 'Connaissance du marche', 'Autre']).setRequired(false);
  form.addParagraphTextItem().setTitle('Precisez les points importants a prendre en compte').setRequired(false);

  form.addSectionHeaderItem().setTitle('Projection');
  form.addScaleItem().setTitle('Clarte actuelle de votre projet professionnel').setBounds(1, 10).setRequired(true);
  form.addScaleItem().setTitle('Niveau d energie pour avancer').setBounds(1, 10).setRequired(true);
  form.addParagraphTextItem().setTitle('Si le bilan reussit, que devra-t-il avoir change pour vous ?').setRequired(true);

}
