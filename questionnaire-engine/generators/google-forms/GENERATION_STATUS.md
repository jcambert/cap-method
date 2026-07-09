# Google Forms Generation Status

## Milestone completed

The Google Forms generator now supports suite generation from a CMDL folder, is verified by GitHub Actions, has passed a first manual Google Apps Script test, and is accepted as candidate primary generator.

## Delivered files

- `generate-google-forms.mjs`
- `generated/FORM-001.generated.gs`
- `generated/cap_method_generated_suite.gs`
- `.github/workflows/google-forms-generation.yml`
- `GOOGLE_APPS_SCRIPT_TEST_PLAN.md`
- `COMPARISON_REPORT.md`

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
- create Google Forms;
- create response spreadsheets;
- connect each form to its response spreadsheet;
- move generated files into a Drive folder;
- map the current CMDL question types to Google Forms items.

## CI validation

GitHub Actions checks that:

- CMDL examples are valid;
- the Google Forms generator runs;
- generated output contains builders from FORM-001 to FORM-010;
- generated output contains Drive and spreadsheet integration calls.

Workflow:

```text
.github/workflows/google-forms-generation.yml
```

## Manual Google Apps Script validation

Status: passed.

Result reported after manual test:

- generated Apps Script executed successfully;
- no correction was identified at this stage.

## Comparison status

Comparison report:

```text
questionnaire-engine/generators/google-forms/COMPARISON_REPORT.md
```

Decision:

- CMDL generator is accepted as candidate primary generator;
- manual Apps Script generator remains fallback until the next validation cycle.

## Commands

Generate the full suite:

```bash
node questionnaire-engine/generators/google-forms/generate-google-forms.mjs
```

Generate from a specific input folder to a specific output file:

```bash
node questionnaire-engine/generators/google-forms/generate-google-forms.mjs questionnaire-engine/cmdl/examples output.gs
```

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
- The manual generator remains available as emergency fallback.
- A complete beneficiary test campaign is still required before removing or archiving the manual generator.

## Validation rule

New questionnaire changes should now be made in CMDL first.

Generated Google Forms output should be produced from CMDL.

The existing manual Apps Script generator remains fallback during the next validation cycle.

## Next milestone

Run one complete beneficiary-style test campaign with generated forms:

1. submit test responses for all 10 forms;
2. inspect generated Google Sheets;
3. confirm answer collection and structure;
4. identify response normalization needs;
5. prepare the next data model for analysis and synthesis.
