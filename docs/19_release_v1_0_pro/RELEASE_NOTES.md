# Release notes - CAP Method v1.0-pro

## Statut

```text
VALIDATED - READY FOR GITHUB RELEASE
```

La dernière CI GitHub Actions a été vérifiée comme verte.

Ces notes peuvent être utilisées pour publier la release GitHub `CAP Method v1.0-pro`.

## Résumé

`CAP Method v1.0-pro` est la première version exploitable professionnellement de CAP Method.

Cette release stabilise la méthode, les questionnaires, la chaîne d'import des réponses, la génération des livrables consultant et les exports finaux.

## Ce que cette release permet

Cette version permet de produire un pack complet de livrables à partir d'un parcours de réponses :

```text
Questionnaires CMDL
  ↓
Google Forms / Google Sheets
  ↓
CSV réponses
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

## Fonctionnalités principales

### Méthode CAP

- modules de bilan structurés ;
- synthèse finale ;
- plan d'action ;
- mode d'emploi accompagnant ;
- supports de publication ;
- logique d'exploitation professionnelle.

### Questionnaire Engine

- définition CMDL des formulaires ;
- validation des formulaires ;
- génération Google Forms / Sheets ;
- génération de CSV de test ;
- import CSV ;
- normalisation des réponses ;
- génération de session complète ;
- analyse structurée ;
- brouillon de synthèse ;
- synthèse finale ;
- plan d'action ;
- package préparé ;
- exports DOCX/PDF ;
- ZIP final.

### Exports

- DOCX minimal stylé ;
- PDF minimal ;
- ZIP final ;
- manifest de traçabilité ;
- fichiers de revue consultant et bénéficiaire.

## Commandes principales

Générer les livrables Markdown :

```bash
node questionnaire-engine/tools/generate-deliverables.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/deliverables/generated/sample-session
```

Préparer le package :

```bash
node questionnaire-engine/tools/prepare-deliverable-package.mjs \
  questionnaire-engine/deliverables/generated/sample-session \
  questionnaire-engine/deliverables/packages \
  0.1.0
```

Exporter en DOCX :

```bash
node questionnaire-engine/tools/export-docx.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

Exporter en PDF :

```bash
node questionnaire-engine/tools/export-pdf.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

Créer le ZIP final :

```bash
node questionnaire-engine/tools/package-deliverables.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session \
  questionnaire-engine/deliverables/packages
```

## Package final

```text
CAP-DELIVERABLES-{session-id}/
├── source/
├── exports/
├── review/
└── manifest.json

CAP-DELIVERABLES-{session-id}.zip
```

## Validation qualité

La release est validée par GitHub Actions avec la chaîne complète :

```text
CMDL
  ↓
CSV de test
  ↓
ResponseSession
  ↓
AnalysisSnapshot
  ↓
SynthesisDraft
  ↓
FinalSynthesis
  ↓
ActionPlan
  ↓
Package
  ↓
DOCX
  ↓
PDF
  ↓
ZIP
```

## Limites connues

- les exports DOCX/PDF sont exploitables mais encore minimalistes ;
- les tableaux Markdown ne sont pas encore convertis en tableaux Word natifs ;
- l'import Google Sheets direct n'est pas encore implémenté ;
- aucune interface web n'est incluse ;
- la validation humaine consultant reste obligatoire.

## Décision de release

Cette version est validée comme première version professionnelle de travail.

Elle doit être utilisée comme socle stable avant les améliorations futures.

Aucune nouvelle fonctionnalité ne doit être ajoutée à ce jalon après publication.
