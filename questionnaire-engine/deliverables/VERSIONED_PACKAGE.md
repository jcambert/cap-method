# Versioned Deliverable Package

## Purpose

This document defines the target structure for a versioned CAP Method deliverable package.

The package must make it clear what was generated, reviewed, exported and delivered.

## Package goals

A versioned package must provide:

- traceability;
- stable file names;
- separation between source and exports;
- room for consultant review status;
- future ZIP generation;
- future auditability.

## Target structure

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
```

## Manifest target fields

```yaml
id: string
sessionId: string
beneficiaryId: string
consultantId: string
packageVersion: string
generatedAt: datetime
reviewStatus: generated | consultant_reviewed | beneficiary_validated | delivered
source:
  cmdlFolder: string
  csvFolder: string
files:
  source:
    responseSession: string
    analysisSnapshot: string
    synthesisDraft: string
    finalSynthesis: string
    actionPlan: string
  exports:
    finalSynthesisDocx: string
    finalSynthesisPdf: string
    actionPlanDocx: string
    actionPlanPdf: string
  review:
    consultantReview: string
    beneficiaryValidation: string
checks:
  markdownGenerated: boolean
  consultantReviewed: boolean
  exportsGenerated: boolean
  pdfGenerated: boolean
  readyForDelivery: boolean
```

## Review statuses

### generated

The package was generated automatically.

No human validation has been recorded yet.

### consultant_reviewed

The consultant reviewed and completed the documents.

### beneficiary_validated

The beneficiary validated the final content.

### delivered

The final PDF/DOCX package was delivered or archived.

## Consultant review file

`review/consultant-review.md` should contain:

```markdown
# Revue consultant

- Date de revue :
- Consultant :
- Synthèse finale relue : oui / non
- Plan d'action relu : oui / non
- Corrections restantes :
- Décision : à compléter / validé / à revoir
```

## Beneficiary validation file

`review/beneficiary-validation.md` should contain:

```markdown
# Validation bénéficiaire

- Date de validation :
- Bénéficiaire :
- Synthèse reconnue comme fidèle : oui / non
- Plan d'action validé : oui / non
- Commentaires :
- Décision : validé / à corriger
```

## Versioning rule

The first generated package should start at:

```text
packageVersion: 0.1.0
```

Version increments:

- patch: wording or metadata correction;
- minor: section or template changes;
- major: workflow or data model changes.

## Next implementation target

Create a command that transforms the current generated files into this structure:

```text
generate-deliverables.mjs output
  ↓
prepare-deliverable-package.mjs
  ↓
CAP-DELIVERABLES-{session-id}/
```

The command should initially create folders, copy Markdown/JSON sources, create review placeholders and update the manifest.
