# Form Definition Schema

This document defines the target structure for future CAP Method questionnaire definitions.

The operational generator currently lives in `cap_method_google_forms_generator.gs`. This schema describes the future externalized format that will replace hard-coded form definitions.

## Root object

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| code | string | yes | Stable form identifier, for example `FORM-001` |
| title | string | yes | Human-readable form title |
| description | string | yes | Form description shown to the user |
| moment | string | no | Moment in the CAP Method journey |
| estimatedDurationMinutes | number | no | Estimated completion time |
| sections | array | yes | Ordered list of sections |

## Section object

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| title | string | yes | Section title |
| description | string | no | Optional section help text |
| questions | array | yes | Ordered list of questions |

## Question object

| Field | Type | Required | Description |
| --- | --- | --- | --- |
| type | string | yes | Question type |
| title | string | yes | Question text |
| helpText | string | no | Optional guidance |
| required | boolean | no | Whether the question is mandatory |
| choices | array | no | Choice values for choice-based questions |
| rows | array | no | Row labels for grid questions |
| lowLabel | string | no | Lower label for 1-to-10 scale |
| highLabel | string | no | Higher label for 1-to-10 scale |

## Supported question types

- TEXT
- PARAGRAPH
- DATE
- MULTIPLE_CHOICE
- CHECKBOX
- LIST
- SCALE_10
- GRID_10

## Example

```json
{
  "code": "FORM-001",
  "title": "Diagnostic initial du beneficiaire",
  "description": "Questionnaire de preparation du premier entretien.",
  "moment": "Before session 1",
  "estimatedDurationMinutes": 30,
  "sections": [
    {
      "title": "Vos informations",
      "questions": [
        {
          "type": "TEXT",
          "title": "Nom",
          "required": true
        }
      ]
    }
  ]
}
```

## Validation rules

Future validation should verify that:

1. Form codes are unique.
2. Form codes follow the `FORM-001` pattern.
3. Section titles are not empty.
4. Question titles are not empty.
5. Choice-based questions define at least one choice.
6. Grid questions define at least one row.
7. Scale questions use explicit labels when possible.
8. The generated form keeps the same order as the definition file.
