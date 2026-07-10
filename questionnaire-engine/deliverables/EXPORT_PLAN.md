# DOCX / PDF Export Plan

## Purpose

This document defines how CAP Method Markdown deliverables should be prepared for DOCX and PDF export.

The current production-ready source format remains Markdown.

DOCX/PDF export must be introduced only after the Markdown deliverables are stable and validated by the consultant.

## Current input files

The end-to-end command generates the following source files:

```text
response-session.json
analysis-snapshot.json
synthesis-draft.md
final-synthesis.md
action-plan.md
manifest.json
```

The export phase must focus on:

```text
final-synthesis.md
action-plan.md
manifest.json
```

## Target export files

The future export package should contain:

```text
CAP-SYNTHESIS-FINALE.docx
CAP-SYNTHESIS-FINALE.pdf
CAP-PLAN-ACTION.docx
CAP-PLAN-ACTION.pdf
CAP-MANIFEST.json
```

Optional internal files:

```text
CAP-ANALYSIS-SNAPSHOT.json
CAP-RESPONSE-SESSION.json
CAP-SYNTHESIS-DRAFT.md
```

## Recommended folder structure

```text
deliverables/generated/{session-id}/
├── source/
│   ├── response-session.json
│   ├── analysis-snapshot.json
│   ├── synthesis-draft.md
│   ├── final-synthesis.md
│   └── action-plan.md
├── exports/
│   ├── CAP-SYNTHESIS-FINALE.docx
│   ├── CAP-SYNTHESIS-FINALE.pdf
│   ├── CAP-PLAN-ACTION.docx
│   └── CAP-PLAN-ACTION.pdf
└── manifest.json
```

## Export rules

### Markdown remains the source of truth

Generated DOCX and PDF files are export artifacts.

They must not become the editable source.

### Consultant validation before export

Before exporting, the consultant must validate:

- final synthesis content;
- action plan content;
- project target wording;
- dates and deadlines;
- beneficiary identity data;
- sensitive information;
- tone and professional wording.

### PDF is distribution-only

PDF files are intended for final sharing and archiving.

Corrections must be made in Markdown, then DOCX/PDF must be regenerated.

## Export stages

### Stage 1 - Markdown package

Generate and review:

```bash
node questionnaire-engine/tools/generate-deliverables.mjs
```

Expected result:

```text
final-synthesis.md
action-plan.md
manifest.json
```

### Stage 2 - DOCX export

Future command:

```bash
node questionnaire-engine/tools/export-docx.mjs \
  questionnaire-engine/deliverables/generated/sample-session
```

Expected result:

```text
exports/CAP-SYNTHESIS-FINALE.docx
exports/CAP-PLAN-ACTION.docx
```

### Stage 3 - PDF export

Future command:

```bash
node questionnaire-engine/tools/export-pdf.mjs \
  questionnaire-engine/deliverables/generated/sample-session
```

Expected result:

```text
exports/CAP-SYNTHESIS-FINALE.pdf
exports/CAP-PLAN-ACTION.pdf
```

### Stage 4 - ZIP package

Future command:

```bash
node questionnaire-engine/tools/package-deliverables.mjs \
  questionnaire-engine/deliverables/generated/sample-session
```

Expected result:

```text
CAP-DELIVERABLES-{session-id}.zip
```

## Naming strategy

Exported file names should remain stable and professional.

Recommended pattern:

```text
CAP-{document-type}-{beneficiary-id-or-code}-{date}.{extension}
```

Examples:

```text
CAP-SYNTHESE-FINALE-beneficiary-test-2026-07-10.docx
CAP-PLAN-ACTION-beneficiary-test-2026-07-10.pdf
```

If privacy is required, use an anonymized code instead of the beneficiary identifier.

## Document style requirements

DOCX/PDF exports should use:

- professional title page;
- clear section hierarchy;
- page numbers;
- consistent headers and footers;
- consultant validation block;
- beneficiary validation block when needed;
- confidentiality notice;
- readable tables for actions and timelines.

## Validation checklist before export

- [ ] Markdown source has been reviewed
- [ ] Final synthesis has been completed by the consultant
- [ ] Action plan has been validated with the beneficiary
- [ ] Sensitive information has been checked
- [ ] Document names are correct
- [ ] Manifest references all generated files
- [ ] Exports open correctly
- [ ] PDF rendering preserves headings and tables

## CI strategy

The CI should eventually validate:

```text
Markdown deliverables generated
  ↓
DOCX files generated
  ↓
PDF files generated
  ↓
manifest references all files
```

Initial CI checks should only validate file presence and basic structure.

Content validation remains consultant-owned.

## Next implementation target

Create the first export preparation command:

```text
generate-deliverables.mjs output
  ↓
normalized deliverable package folder
```

This should move from raw generated files to an export-ready structure:

```text
source/
exports/
manifest.json
```
