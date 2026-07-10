# Beneficiary Test Campaign

## Objective

Validate the complete generated questionnaire journey from the point of view of a beneficiary.

This campaign checks that answers collected through generated Google Forms can be used later for analysis, synthesis and deliverables.

## Scope

Forms to test:

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

## Test dataset

Use one fictitious beneficiary profile.

Recommended profile:

- name: Beneficiaire Test
- email: beneficiary.test@example.com
- current role: developpeur logiciel
- objective: clarifier une evolution professionnelle
- context: souhaite mieux valoriser ses competences et construire un plan d action concret

## Execution steps

1. Generate the Google Forms suite from CMDL.
2. Run `createCapMethodGeneratedFormsSuite()` in Google Apps Script.
3. Open each generated form.
4. Submit one complete test response per form.
5. Open each response spreadsheet.
6. Check that each submitted answer is stored correctly.
7. Export one response spreadsheet as CSV if deeper analysis is needed.

## Checks per form

For each form, verify:

- the form opens correctly;
- all expected sections are present;
- required fields block incomplete submission;
- choice options are displayed;
- rating questions use the expected scale;
- the response is written to the linked spreadsheet;
- timestamp and answer columns are created by Google Forms.

## Pass criteria

The campaign passes when:

- all 10 forms can be submitted;
- all 10 sheets receive responses;
- no blocking Google Apps Script runtime error occurs;
- response columns are understandable enough to be mapped back to CMDL questions;
- no missing critical question is identified.

## Fail criteria

The campaign fails if:

- one generated form cannot be opened;
- one form cannot be submitted;
- one response sheet is missing;
- one form is not linked to its sheet;
- a required question behaves incorrectly;
- answer columns are unusable for later analysis.

## Output of this campaign

The expected output is a test note containing:

- execution date;
- generated folder name;
- list of tested forms;
- issues found;
- decision: pass, pass with minor corrections, or fail.

## Current status

Ready to execute.
