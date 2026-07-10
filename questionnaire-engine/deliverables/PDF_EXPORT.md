# PDF Export Command

## Purpose

`export-pdf.mjs` creates PDF distribution artifacts from a prepared deliverable package.

It uses the Markdown source files from the package:

```text
source/final-synthesis.md
source/action-plan.md
```

and writes PDF files into:

```text
exports/
```

## Command

Default command:

```bash
node questionnaire-engine/tools/export-pdf.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/export-pdf.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

## Output

```text
exports/CAP-SYNTHESE-FINALE.pdf
exports/CAP-PLAN-ACTION.pdf
```

The package manifest is updated with:

```yaml
files:
  exports:
    finalSynthesisPdf: path
    actionPlanPdf: path
checks:
  pdfGenerated: true
```

## Implementation note

The command creates a minimal PDF without external dependencies.

The current export is intentionally simple:

- text-based rendering;
- headings are preserved as text;
- lists and checklists are preserved as readable lines;
- Markdown tables are preserved as text rows;
- accents are normalized for PDF compatibility.

## Current limitations

- no advanced typography yet;
- no native table rendering yet;
- no page numbering yet;
- no headers/footers yet;
- no graphical title page yet.

## Important rule

PDF is a distribution artifact.

Corrections must be made in Markdown sources, then DOCX/PDF must be regenerated.

## Next target

Create the final ZIP package command:

```text
prepared package
  ↓
DOCX export
  ↓
PDF export
  ↓
ZIP package
```
