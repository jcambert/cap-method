# Google Forms Generation Status

## Milestone completed

The Google Forms generator now supports suite generation from a CMDL folder, is verified by GitHub Actions, and has passed a first manual Google Apps Script test.

## Delivered files

- `generate-google-forms.mjs`
- `generated/FORM-001.generated.gs`
- `generated/cap_method_generated_suite.gs`
- `.github/workflows/google-forms-generation.yml`
- `GOOGLE_APPS_SCRIPT_TEST_PLAN.md`

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

The existing manual Apps Script generator remains available as fallback until final comparison is complete.

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
- It does not yet replace the existing working Apps Script generator.
- Final side-by-side comparison with the original generator is still pending.

## Validation rule

The generated Apps Script suite can move to candidate status.

The existing Apps Script generator remains the production fallback until side-by-side comparison is complete.

## Next milestone

Compare generated forms with the current working Apps Script generator and produce a migration decision:

1. compare form titles;
2. compare section titles;
3. compare question wording;
4. compare choice options;
5. compare required flags;
6. compare response destination behavior;
7. decide whether generated suite can replace the manual generator.
