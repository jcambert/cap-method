# End-to-End Deliverable Generation

## Purpose

This command generates all consultant deliverables from one response session source.

It orchestrates the existing tools instead of replacing them.

## Flow

```text
CMDL folder
CSV response folder
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

## Command

Default command:

```bash
node questionnaire-engine/tools/generate-deliverables.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/generate-deliverables.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/deliverables/generated/sample-session
```

## Generated files

The command writes:

```text
response-session.json
analysis-snapshot.json
synthesis-draft.md
final-synthesis.md
action-plan.md
manifest.json
```

## Manifest

The manifest contains:

- session id;
- beneficiary id;
- consultant id;
- generation date;
- source folders;
- output folder;
- coverage information;
- generated file paths;
- next review steps.

## Intended use

Use this command when a full set of CSV responses is available and the consultant wants to produce the working deliverable pack in one run.

## Important rules

- The generated files remain drafts until consultant validation.
- The action plan must be validated with the beneficiary.
- The final synthesis must be reviewed before delivery.
- DOCX/PDF exports should be generated only after Markdown validation.

## Current limitations

- No real Google Sheets API import yet;
- input remains CSV folder based;
- no DOCX/PDF export yet;
- no archive ZIP generation yet;
- no beneficiary-specific naming strategy yet.

## Next target

Prepare export-ready deliverables:

```text
Markdown deliverables
  ↓
DOCX/PDF export plan
  ↓
versioned deliverable package
```
