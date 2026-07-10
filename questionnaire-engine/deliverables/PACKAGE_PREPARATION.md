# Package Preparation Command

## Purpose

`prepare-deliverable-package.mjs` transforms raw generated deliverables into an export-ready package structure.

It does not generate DOCX or PDF files yet.

It prepares the folder layout required for future export and review.

## Input

Input folder produced by:

```bash
node questionnaire-engine/tools/generate-deliverables.mjs
```

Expected files:

```text
response-session.json
analysis-snapshot.json
synthesis-draft.md
final-synthesis.md
action-plan.md
manifest.json
```

## Command

Default command:

```bash
node questionnaire-engine/tools/prepare-deliverable-package.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/prepare-deliverable-package.mjs \
  questionnaire-engine/deliverables/generated/sample-session \
  questionnaire-engine/deliverables/packages \
  0.1.0
```

## Output

```text
CAP-DELIVERABLES-{session-id}/
├── source/
│   ├── response-session.json
│   ├── analysis-snapshot.json
│   ├── synthesis-draft.md
│   ├── final-synthesis.md
│   └── action-plan.md
├── exports/
├── review/
│   ├── consultant-review.md
│   └── beneficiary-validation.md
└── manifest.json
```

## Generated review files

### consultant-review.md

Used by the consultant to record:

- review date;
- synthesis review status;
- action plan review status;
- remaining corrections;
- decision.

### beneficiary-validation.md

Used to record:

- beneficiary validation date;
- fidelity of the synthesis;
- validation of the action plan;
- comments;
- decision.

## Manifest

The package manifest includes:

- package id;
- session id;
- beneficiary id;
- consultant id;
- package version;
- review status;
- source file references;
- future export file references;
- review file references;
- validation checks.

## Current status

This command prepares the package for review and future export.

DOCX/PDF generation will be added later.

## Next target

Add CI validation for the prepared package structure, then implement the DOCX export command.
