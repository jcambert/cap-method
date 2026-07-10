# CAP Method

CAP est une méthode d'accompagnement personnel et professionnel visant à aider une personne à mieux se connaître, clarifier ses ressources, ses valeurs, ses besoins, ses compétences et construire des pistes d'évolution cohérentes.

Ce dépôt contient la documentation source de la méthode CAP ainsi que les briques techniques permettant d'automatiser les questionnaires, l'import des réponses, l'analyse, la préparation des livrables consultant et les exports finaux.

## Organisation

```text
cap-method/
├── README.md
├── CHANGELOG.md
├── docs/
│   ├── 00_bienvenue/
│   ├── 01_mon_histoire/
│   ├── 02_mes_valeurs/
│   ├── 03_ma_facon_de_fonctionner/
│   ├── 04_mes_besoins/
│   ├── 05_mes_motivations_profondes/
│   ├── 06_mes_competences/
│   ├── 07_mes_talents/
│   ├── 08_mon_environnement_ideal/
│   ├── 09_mes_ressources_et_contraintes/
│   ├── 10_projet_de_vie_et_scenarios/
│   ├── 11_mon_plan_action/
│   ├── 12_synthese_finale/
│   ├── 13_mode_emploi_accompagnant/
│   ├── 14_pilote_terrain/
│   ├── 15_google_forms_pack/
│   ├── 16_google_sheets_pack/
│   ├── 17_publication/
│   └── 18_release_v1_5_0/
├── questionnaire-engine/
│   ├── cmdl/
│   ├── generators/
│   ├── responses/
│   ├── analysis/
│   ├── synthesis/
│   ├── deliverables/
│   └── tools/
└── templates/
    ├── google_forms/
    ├── google_sheets/
    └── messenger/
```

## État d'avancement synthétique

| Bloc | Statut |
|---|---|
| LIVRABLE-001 à LIVRABLE-099 | ✅ Terminés |
| Release v1.5.0 – Notes, manifeste et procédure | ✅ Préparée |
| Questionnaire Engine | ✅ Chaîne complète jusqu'au ZIP final |
| Export DOCX/PDF | ✅ Généré et validé en CI |
| Pack versionné de livrables | ✅ Généré et validé en CI |

## Chaîne technique complète

Le dossier `questionnaire-engine/` contient une chaîne automatisée complète :

```text
CMDL FORM-001 à FORM-010
  ↓
Génération Google Forms / Google Sheets
  ↓
Import CSV des réponses
  ↓
ResponseSession JSON
  ↓
AnalysisSnapshot JSON
  ↓
SynthesisDraft Markdown
  ↓
FinalSynthesis Markdown
  ↓
ActionPlan Markdown
  ↓
Package source / exports / review
  ↓
DOCX
  ↓
PDF
  ↓
ZIP final
```

### Statut de la chaîne

| Étape | Statut |
|---|---|
| Définitions CMDL FORM-001 à FORM-010 | ✅ OK |
| Validation CMDL | ✅ OK |
| Génération Google Forms | ✅ OK |
| Test manuel Apps Script | ✅ OK |
| Modèle de données réponses | ✅ OK |
| Règles de normalisation | ✅ OK |
| Import CSV FORM-001 | ✅ OK |
| Import ResponseSession complète | ✅ OK |
| Génération AnalysisSnapshot | ✅ OK |
| Génération SynthesisDraft Markdown | ✅ OK |
| Génération FinalSynthesis Markdown | ✅ OK |
| Génération ActionPlan Markdown | ✅ OK |
| Commande end-to-end `generate-deliverables.mjs` | ✅ OK |
| Préparation du package `prepare-deliverable-package.mjs` | ✅ OK |
| Export DOCX `export-docx.mjs` | ✅ OK |
| Export PDF `export-pdf.mjs` | ✅ OK |
| ZIP final `package-deliverables.mjs` | ✅ OK |
| Validation CI complète | ✅ OK |

## Commande principale recommandée

Générer les livrables Markdown :

```bash
node questionnaire-engine/tools/generate-deliverables.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/deliverables/generated/sample-session
```

Préparer le package exportable :

```bash
node questionnaire-engine/tools/prepare-deliverable-package.mjs \
  questionnaire-engine/deliverables/generated/sample-session \
  questionnaire-engine/deliverables/packages \
  0.1.0
```

Générer les exports DOCX :

```bash
node questionnaire-engine/tools/export-docx.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

Générer les exports PDF :

```bash
node questionnaire-engine/tools/export-pdf.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

Générer le ZIP final :

```bash
node questionnaire-engine/tools/package-deliverables.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session \
  questionnaire-engine/deliverables/packages
```

## Package final produit

```text
CAP-DELIVERABLES-{session-id}/
├── source/
│   ├── response-session.json
│   ├── analysis-snapshot.json
│   ├── synthesis-draft.md
│   ├── final-synthesis.md
│   └── action-plan.md
├── exports/
│   ├── CAP-SYNTHESE-FINALE.docx
│   ├── CAP-SYNTHESE-FINALE.pdf
│   ├── CAP-PLAN-ACTION.docx
│   └── CAP-PLAN-ACTION.pdf
├── review/
│   ├── consultant-review.md
│   └── beneficiary-validation.md
└── manifest.json

CAP-DELIVERABLES-{session-id}.zip
```

## Documentation export

```text
questionnaire-engine/deliverables/EXPORT_PLAN.md
questionnaire-engine/deliverables/VERSIONED_PACKAGE.md
questionnaire-engine/deliverables/PACKAGE_PREPARATION.md
questionnaire-engine/deliverables/DOCX_EXPORT.md
questionnaire-engine/deliverables/PDF_EXPORT.md
questionnaire-engine/deliverables/ZIP_PACKAGE.md
```

## Règles importantes

- Les fichiers Markdown restent la source de vérité éditable.
- Les DOCX/PDF sont des artefacts d'export.
- Le ZIP est le package de distribution.
- Toute correction doit être faite dans les sources Markdown, puis les exports doivent être régénérés.
- Les livrables restent à relire et valider humainement avant remise réelle à un bénéficiaire.

## Prochaine étape

La prochaine phase consiste à améliorer la qualité des exports :

```text
exports minimalistes validés
  ↓
styles DOCX professionnels
  ↓
tables natives
  ↓
page de garde
  ↓
entêtes / pieds de page
```
