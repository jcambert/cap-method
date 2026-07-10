# DOCX Export Command

## Purpose

`export-docx.mjs` creates the first DOCX export artifacts from a prepared deliverable package.

It uses the Markdown source files from the package:

```text
source/final-synthesis.md
source/action-plan.md
```

and writes DOCX files into:

```text
exports/
```

## Command

Default command:

```bash
node questionnaire-engine/tools/export-docx.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/export-docx.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session
```

## Output

```text
exports/CAP-SYNTHESE-FINALE.docx
exports/CAP-PLAN-ACTION.docx
```

The package manifest is updated with:

```yaml
files:
  exports:
    finalSynthesisDocx: path
    actionPlanDocx: path
checks:
  exportsGenerated: true
```

## Implementation note

The command creates a minimal OpenXML DOCX package without external dependencies.

The current export is intentionally simple:

- headings are converted to Word paragraphs;
- bullet lists are preserved as text bullets;
- checklist items are preserved with checkbox symbols;
- Markdown tables are preserved as text rows.

## Current limitations

- no custom Word style template yet;
- tables are not converted into native Word tables yet;
- no header/footer yet;
- no page numbering yet;
- no PDF export yet.

## Next target

Create the PDF export command:

```text
prepared package
  ↓
DOCX export
  ↓
PDF export
```

PDF should remain a distribution artifact, not an editable source.
