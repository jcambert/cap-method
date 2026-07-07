# CMDL Tools

## Validator

Run from repository root:

```bash
node questionnaire-engine/tools/validate-cmdl.mjs
```

Run against another folder:

```bash
node questionnaire-engine/tools/validate-cmdl.mjs path/to/cmdl/files
```

## Current checks

The validator currently checks:

- root fields;
- form id format;
- semantic version format;
- question type presence;
- supported question types;
- duplicate section ids;
- duplicate question ids;
- question labels;
- required flags for non-information questions;
- options block for choice questions;
- rows block for matrix questions.

## Next checks

Planned checks:

- stricter id coverage;
- status compatibility rules;
- minimum option count for choice questions;
- minimum row count for matrix questions.
