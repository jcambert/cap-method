# Google Forms Generation Status

## Milestone completed

The first CMDL to Google Forms generation milestone is complete.

## Delivered files

- `generate-google-forms.mjs`
- `generated/FORM-001.generated.gs`

## Source

- `questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml`

## Current output

The generated output creates a Google Apps Script builder function:

```javascript
function build_FORM_001_(form) {
  // generated Google Forms items
}
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
- It does not yet generate the full suite wrapper.
- It does not yet create spreadsheets or Drive folders.
- It does not yet replace the existing working Apps Script generator.

## Validation rule

The existing Apps Script generator remains the production fallback until generated output has been tested in Google Apps Script.

## Next milestone

Generate the full Google Forms suite from all CMDL files:

1. load FORM-001 to FORM-010;
2. generate one builder function per form;
3. generate one suite creation function;
4. compare with the existing working Apps Script generator;
5. test in Google Apps Script.
