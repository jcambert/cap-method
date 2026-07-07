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
- presence of at least one question type.

## Next checks

Planned checks:

- supported question types;
- unique section ids;
- unique question ids;
- required options for choice questions;
- required rows for matrix questions.
