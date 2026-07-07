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

- required root fields;
- form id format;
- semantic version format;
- presence of at least one question type;
- supported question types;
- duplicate section ids;
- duplicate question ids;
- options block for singleChoice and multipleChoice questions;
- rows block for matrix questions.

## Next checks

Planned checks:

- question id format coverage;
- section id format coverage;
- status compatibility rules;
- minimum option count for choice questions;
- minimum row count for matrix questions.
