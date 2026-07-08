# Google Apps Script Test Plan

## Objective

Validate that the generated Google Forms suite can replace the manual Apps Script generator later.

## Current status

The generated suite is CI-checked but not yet manually executed in Google Apps Script.

The existing manual generator remains the production fallback.

## File to test

Generate the suite locally:

```bash
node questionnaire-engine/generators/google-forms/generate-google-forms.mjs
```

Then copy this file into Google Apps Script:

```text
questionnaire-engine/generators/google-forms/generated/cap_method_generated_suite.gs
```

## Function to run

```javascript
createCapMethodGeneratedFormsSuite()
```

## Expected result

Google Drive should contain one folder:

```text
CAP Method - Generated Google Forms Suite
```

Inside this folder, there should be:

- 10 Google Forms;
- 10 Google Sheets response files.

Each form should be connected to its own spreadsheet destination.

## Forms expected

- FORM-001 - Diagnostic initial
- FORM-002 - Valeurs professionnelles
- FORM-003 - Competences et realisations
- FORM-004 - Mode de fonctionnement
- FORM-005 - Interets professionnels
- FORM-006 - Analyse des pistes
- FORM-007 - Plan professionnel
- FORM-008 - Evaluation intermediaire
- FORM-009 - Evaluation finale
- FORM-010 - Suivi six mois

## Manual checks

For each form:

1. open the form edit URL;
2. check that sections are present;
3. check that questions are present;
4. check required questions;
5. submit a test response;
6. verify that the response appears in the linked spreadsheet.

## Comparison with fallback generator

Compare generated forms with the existing manual Apps Script generator on:

- form title;
- section titles;
- question wording;
- choice options;
- required flags;
- response destination.

## Pass criteria

The generated suite can be considered validated when:

- all 10 forms are created;
- all 10 response sheets are created;
- all forms are moved to the generated folder;
- all sheets are moved to the generated folder;
- each form writes responses to its own sheet;
- no blocking Apps Script runtime error occurs.

## Fail criteria

The generated suite must not replace the manual generator if:

- any form is missing;
- any response sheet is missing;
- a form is not linked to a sheet;
- sections or questions are missing;
- Google Apps Script throws a runtime error.
