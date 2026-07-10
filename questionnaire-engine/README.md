# CAP Method Questionnaire Engine

This folder contains the questionnaire, response import, analysis, deliverable generation and export automation for CAP Method.

## Goal

Provide a neutral source of truth and processing chain for CAP Method questionnaires, independent from Google Forms, Google Sheets, Blazor, PDF, DOCX or AI adapters.

The current chain is complete up to a final ZIP package:

```text
CMDL questionnaire definitions
  ↓
Google Forms / Google Sheets generation
  ↓
CSV response import
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
Prepared package
  ↓
DOCX exports
  ↓
PDF exports
  ↓
Final ZIP package
```

## Main concepts

- **CMDL**: CAP Method Definition Language.
- **Form definition**: one questionnaire described as YAML.
- **Google Forms generator**: produces Google Apps Script from CMDL definitions.
- **ResponseForm**: normalized response for one form.
- **ResponseSession**: normalized response set for a full beneficiary journey.
- **AnalysisSnapshot**: first structured analysis output for consultant review.
- **SynthesisDraft**: Markdown working document generated for the consultant.
- **FinalSynthesis**: structured final synthesis Markdown document.
- **ActionPlan**: professional action plan Markdown document.
- **Prepared package**: export-ready folder separating source, exports and review files.
- **DOCX/PDF exports**: distribution artifacts generated from Markdown sources.
- **ZIP package**: final distributable package.

## Current structure

```text
questionnaire-engine/
├── README.md
├── cmdl/
│   ├── specification.md
│   └── examples/
├── generators/
│   └── google-forms/
├── responses/
├── analysis/
├── synthesis/
├── deliverables/
└── tools/
    ├── validate-cmdl.mjs
    ├── import-response-csv.mjs
    ├── generate-sample-response-csvs.mjs
    ├── import-response-session.mjs
    ├── analyze-response-session.mjs
    ├── generate-synthesis-draft.mjs
    ├── generate-final-synthesis.mjs
    ├── generate-action-plan.mjs
    ├── generate-deliverables.mjs
    ├── prepare-deliverable-package.mjs
    ├── export-docx.mjs
    ├── export-pdf.mjs
    └── package-deliverables.mjs
```

## Current status

| Area | Status |
|---|---|
| CMDL definitions FORM-001 to FORM-010 | ✅ Operational |
| CMDL validation | ✅ Operational |
| Google Forms generation | ✅ Operational and manually tested |
| Response data model | ✅ Defined |
| Normalization rules | ✅ Defined |
| FORM-001 CSV import | ✅ Operational |
| Full ResponseSession import | ✅ Operational |
| AnalysisSnapshot generation | ✅ Operational |
| SynthesisDraft Markdown generation | ✅ Operational |
| FinalSynthesis Markdown generation | ✅ Operational |
| ActionPlan Markdown generation | ✅ Operational |
| End-to-end deliverable generation | ✅ Operational |
| Package preparation | ✅ Operational |
| DOCX export | ✅ Operational |
| PDF export | ✅ Operational |
| ZIP package | ✅ Operational |
| CI full-chain validation | ✅ Operational |

## Full command chain

Generate the Markdown deliverables:

```bash
node questionnaire-engine/tools/generate-deliverables.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/deliverables/generated/sample-session
```

Prepare the export package:

```bash
node questionnaire-engine/tools/prepare-deliverable-package.mjs \
  questionnaire-engine/deliverables/generated/sample-session \
  questionnaire-engine/deliverables/packages \
  0.1.0
```

Generate DOCX exports:

```bash
node questionnaire-engine/tools/export-docx.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

Generate PDF exports:

```bash
node questionnaire-engine/tools/export-pdf.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

Generate final ZIP:

```bash
node questionnaire-engine/tools/package-deliverables.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session \
  questionnaire-engine/deliverables/packages
```

## Final package structure

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

## CI validation

The GitHub Actions workflow validates the full chain:

```text
CMDL validation
  ↓
Sample response CSV generation
  ↓
FORM-001 response import
  ↓
Full ResponseSession import
  ↓
AnalysisSnapshot generation
  ↓
SynthesisDraft Markdown generation
  ↓
FinalSynthesis Markdown generation
  ↓
ActionPlan Markdown generation
  ↓
End-to-end deliverable generation
  ↓
Package preparation
  ↓
DOCX export
  ↓
PDF export
  ↓
ZIP package
```

Workflow:

```text
.github/workflows/cmdl-validation.yml
```

## Important rules

- CMDL remains the source of truth for questionnaire structure.
- Google Forms and Google Sheets are collection channels, not the source of truth.
- Google Sheets column labels are used only as an import mapping mechanism.
- `questionId` and `formId` must come from CMDL.
- Generated Markdown files are working deliverables, not automatically approved final documents.
- DOCX/PDF files are export artifacts, not editable sources.
- PDF files are distribution artifacts.
- ZIP files are distribution packages.
- Human consultant validation remains mandatory before real delivery.

## Next steps

1. Improve DOCX styling.
2. Convert Markdown tables into native DOCX tables.
3. Add title pages.
4. Add headers, footers and page numbers.
5. Add a higher-level single command for the full export chain.
