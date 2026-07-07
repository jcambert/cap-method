# Google Forms Generation Status

## Milestone completed

The Google Forms generator now supports suite generation from a CMDL folder and is verified by GitHub Actions.

## Delivered files

- `generate-google-forms.mjs`
- `generated/FORM-001.generated.gs`
- `generated/cap_method_generated_suite.gs`
- `.github/workflows/google-forms-generation.yml`

## Sources

- `questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-002.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-003.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-004.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-005.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-006.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-007.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-008.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-009.cmdl.yaml`
- `questionnaire-engine/cmdl/examples/FORM-010.cmdl.yaml`

## Current generator capability

The generator can:

- read a single CMDL file;
- read a folder of CMDL files;
- sort CMDL files by name;
- generate one builder function per form;
- generate one suite creation function;
- map the current CMDL question types to Google Forms items.

## CI validation

GitHub Actions now checks that:

- CMDL examples are valid;
- the Google Forms generator runs;
- generated output contains builders from FORM-001 to FORM-010.

Workflow:

```text
.github/workflows/google-forms-generation.yml
```

## Commands

Generate the full suite:

```bash
node questionnaire-engine/generators/google-forms/generate-google-forms.mjs
```

Generate from a specific input folder to a specific output file:

```bash
node questionnaire-engine/generators/google-forms/generate-google-forms.mjs questionnaire-engine/cmdl/examples output.gs
```

## Important note about the committed suite snapshot

The committed `generated/cap_method_generated_suite.gs` is a suite-level snapshot showing the final wrapper and builder function structure.

The authoritative generated output must be refreshed by running `generate-google-forms.mjs` locally or in CI, because the script emits the full question content from CMDL.

## Supported mappings

- information to section header
- text to TextItem
- email to TextItem
- phone to TextItem
- longText to ParagraphTextItem
- singleChoice to MultipleChoiceItem
- multipleChoice to CheckboxItem
- rating5 to ScaleItem 1..5
- rating10 to ScaleItem 1..10

## Known limitations

- The generator is intentionally simple.
- It is built for current CMDL examples, not generic YAML.
- It does not yet create spreadsheets or Drive folders.
- It does not yet move files into a Drive folder.
- It does not yet replace the existing working Apps Script generator.
- The committed suite snapshot is not yet a production Apps Script replacement.
- Google Apps Script execution is not yet tested from this repository.

## Validation rule

The existing Apps Script generator remains the production fallback until generated output has been tested in Google Apps Script.

## Next milestone

Turn generated suite output into a production-ready Apps Script replacement:

1. add Drive folder and spreadsheet destination generation;
2. commit the fully refreshed generated suite;
3. test the generated suite in Google Apps Script;
4. compare generated forms with the current working generator.
