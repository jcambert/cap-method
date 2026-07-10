# Response Session Importer

## Purpose

Build one normalized `ResponseSession` JSON from generated Google Forms response CSV files.

Target flow:

```text
FORM-001 to FORM-010 response CSV files
  ↓
ResponseSession JSON
  ↓
completeness check
  ↓
analysis readiness check
```

## Generate sample CSV files

```bash
node questionnaire-engine/tools/generate-sample-response-csvs.mjs
```

Default output:

```text
questionnaire-engine/responses/generated/samples
```

## Import a session

```bash
node questionnaire-engine/tools/import-response-session.mjs
```

Defaults:

```text
CMDL folder: questionnaire-engine/cmdl/examples
CSV folder: questionnaire-engine/responses/generated/samples
Output: questionnaire-engine/responses/generated/session.response.normalized.json
```

Custom command:

```bash
node questionnaire-engine/tools/import-response-session.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/responses/generated/session.response.normalized.json
```

## Output

The output JSON contains:

- `id`;
- `beneficiaryId`;
- `consultantId`;
- `startedAt`;
- `completedAt`;
- `status`;
- `forms[]`;
- `completeness`;
- `analysisReadiness`;
- `import`.

## Completeness check

A session is complete when all expected CMDL forms have a corresponding response CSV.

Current expected forms:

- FORM-001
- FORM-002
- FORM-003
- FORM-004
- FORM-005
- FORM-006
- FORM-007
- FORM-008
- FORM-009
- FORM-010

## Analysis readiness check

A session is ready for first analysis when:

- no import error exists;
- FORM-001 is present;
- FORM-002 is present;
- FORM-003 is present;
- at least FORM-006 or FORM-007 is present.

## Current limitations

- imports the first response row per CSV only;
- maps spreadsheet columns by CMDL question label;
- uses generated test beneficiary metadata;
- does not yet merge several beneficiaries from the same spreadsheet;
- does not yet read directly from Google Sheets API.

## Next target

Use `ResponseSession` JSON as the input of the first analysis engine.
