# CSV Response Importer

## Purpose

Import one Google Forms response CSV and convert it to a normalized CAP Method `ResponseForm` JSON document.

First supported target:

```text
FORM-001 response CSV
  ↓
ResponseForm JSON
```

## Command

Default command:

```bash
node questionnaire-engine/tools/import-response-csv.mjs
```

This uses:

```text
questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml
questionnaire-engine/responses/samples/FORM-001.responses.sample.csv
questionnaire-engine/responses/generated/FORM-001.response.normalized.json
```

Custom command:

```bash
node questionnaire-engine/tools/import-response-csv.mjs <cmdl-path> <csv-path> <output-json-path>
```

Example:

```bash
node questionnaire-engine/tools/import-response-csv.mjs \
  questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml \
  questionnaire-engine/responses/samples/FORM-001.responses.sample.csv \
  questionnaire-engine/responses/generated/FORM-001.response.normalized.json
```

## Output

The generated JSON contains:

- `formId`;
- `formTitle`;
- `submittedAt`;
- `answers[]`;
- `import.status`;
- `import.warnings[]`;
- `import.errors[]`.

Each answer contains:

- `questionId`;
- `questionLabel`;
- `questionType`;
- `rawValue`;
- `normalizedValue`;
- `source`.

## Current limitations

- imports the first response row only;
- maps Google Sheets columns by question label;
- targets FORM-001 first;
- does not yet create a full `ResponseSession` with all forms;
- does not yet read directly from Google Sheets API.

## Next target

Extend the importer to support:

- every response row in a CSV;
- all FORM-001 to FORM-010 response CSV files;
- aggregated `ResponseSession` JSON;
- analysis readiness checks.
