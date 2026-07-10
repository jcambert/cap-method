# Consultant Synthesis Draft

## Purpose

The consultant synthesis draft is the first deliverable-oriented output generated from an `AnalysisSnapshot`.

It is not the final bilan de competences synthesis.

It is a structured working document for the consultant.

## Flow

```text
ResponseSession JSON
  ↓
AnalysisSnapshot JSON
  ↓
SynthesisDraft Markdown
```

## Command

Default command:

```bash
node questionnaire-engine/tools/generate-synthesis-draft.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/generate-synthesis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/synthesis/generated/sample.synthesis-draft.md
```

## Output format

The output is a Markdown document containing:

- status of the document;
- traceability;
- overview;
- main indicators;
- detected highlights;
- risks and vigilance points;
- consultant preparation questions;
- consultant review section;
- next decision checklist.

## Intended use

The consultant uses this draft to:

- prepare an interview;
- identify points to validate;
- structure the narrative synthesis;
- prepare the final action plan;
- avoid losing traceability between answers, analysis and deliverables.

## Important rule

This draft must always be reviewed by a human consultant.

The tool does not make a final professional decision and does not replace consultant judgment.

## Current limitations

- Markdown only;
- generic synthesis structure;
- no DOCX/PDF generation yet;
- no consultant-specific style customization;
- no final narrative synthesis yet;
- no action plan generator yet.

## Next target

Create a real deliverable structure:

```text
SynthesisDraft Markdown
  ↓
Final synthesis template
  ↓
Action plan template
```
