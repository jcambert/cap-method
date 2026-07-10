# CAP Method

CAP est une méthode d'accompagnement personnel et professionnel visant à aider une personne à mieux se connaître, clarifier ses ressources, ses valeurs, ses besoins, ses compétences et construire des pistes d'évolution cohérentes.

Ce dépôt contient la documentation source de la méthode CAP ainsi que les premières briques techniques permettant d'automatiser les questionnaires, l'import des réponses, l'analyse et la préparation des livrables consultant.

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
| Questionnaire Engine | ✅ Chaîne end-to-end opérationnelle jusqu'au pack livrables consultant |

## Modules opérationnels

| Module | Statut |
|---|---|
| Module 0 à Module 11 | ✅ Complets |
| Synthèse finale CAP | ✅ Complet |
| Mode d'emploi accompagnant | ✅ Complet |
| Pilote terrain CAP | ✅ Complet |
| Pack Google Forms | ✅ Complet |
| Pack Google Sheets | ✅ Complet |
| Pack publication | ✅ Complet |
| Préparation release GitHub v1.5.0 | ✅ Complet |

## Chaîne technique actuelle

Le dossier `questionnaire-engine/` contient maintenant une chaîne automatisée de bout en bout :

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
Manifest JSON
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
| Validation CI de bout en bout | ✅ OK |

## Commande principale recommandée

Générer le pack complet de livrables consultant :

```bash
node questionnaire-engine/tools/generate-deliverables.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/deliverables/generated/sample-session
```

Cette commande produit :

```text
response-session.json
analysis-snapshot.json
synthesis-draft.md
final-synthesis.md
action-plan.md
manifest.json
```

## Commandes unitaires

Valider les questionnaires CMDL :

```bash
node questionnaire-engine/tools/validate-cmdl.mjs
```

Générer les Google Forms depuis CMDL :

```bash
node questionnaire-engine/generators/google-forms/generate-google-forms.mjs questionnaire-engine/cmdl/examples
```

Générer des CSV de réponses de test :

```bash
node questionnaire-engine/tools/generate-sample-response-csvs.mjs
```

Importer une session complète :

```bash
node questionnaire-engine/tools/import-response-session.mjs
```

Générer une analyse :

```bash
node questionnaire-engine/tools/analyze-response-session.mjs
```

Générer un brouillon de synthèse consultant :

```bash
node questionnaire-engine/tools/generate-synthesis-draft.mjs
```

Générer une synthèse finale structurée :

```bash
node questionnaire-engine/tools/generate-final-synthesis.mjs
```

Générer un plan d'action :

```bash
node questionnaire-engine/tools/generate-action-plan.mjs
```

## Convention de nommage

Les livrables sont stockés en Markdown afin de faciliter le versionnement et les revues.

Les exports DOCX/PDF pourront être générés à partir de ces fichiers sources lorsque les modèles Markdown seront stabilisés.

## Philosophie de travail

Le dépôt GitHub devient la source officielle du projet. Le chat sert à produire, améliorer et valider les contenus avant commit.

La logique retenue est progressive :

```text
contenu stable
  ↓
questionnaires structurés
  ↓
réponses normalisées
  ↓
analyse assistée
  ↓
livrables consultant
  ↓
exports finaux
```

## Prochaine étape

Le prochain jalon est de préparer les livrables pour l'export :

```text
Markdown livrables
  ↓
plan d'export DOCX/PDF
  ↓
pack versionné de livrables
```
