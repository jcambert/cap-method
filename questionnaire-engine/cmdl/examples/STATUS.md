# CMDL Questionnaire Status

## Status

All CAP Method questionnaire examples now have enriched CMDL definitions.

## Completed forms

| Form | Title | Version | Status |
| --- | --- | --- | --- |
| FORM-001 | Diagnostic initial | 0.2.0 | enriched |
| FORM-002 | Valeurs professionnelles | 0.2.0 | enriched |
| FORM-003 | Competences et realisations | 0.2.0 | enriched |
| FORM-004 | Mode de fonctionnement | 0.2.0 | enriched |
| FORM-005 | Interets professionnels | 0.2.0 | enriched |
| FORM-006 | Analyse des pistes | 0.2.0 | enriched |
| FORM-007 | Plan professionnel | 0.2.0 | enriched |
| FORM-008 | Evaluation intermediaire | 0.2.0 | enriched |
| FORM-009 | Evaluation finale | 0.2.0 | enriched |
| FORM-010 | Suivi six mois | 0.2.0 | enriched |

## Definition standard

Each enriched definition follows the same baseline:

- root metadata;
- ordered sections;
- stable question ids;
- explicit question type;
- question label;
- required flag for non-information questions;
- options for choice questions.

## Validation

The current validator checks the structural baseline required before generation.

Run:

```bash
node questionnaire-engine/tools/validate-cmdl.mjs
```

## Next milestone

The next milestone is to generate an Apps Script model from CMDL, starting with FORM-001.
