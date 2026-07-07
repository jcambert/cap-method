# CAP Method Definition Language - CMDL

## Status

Version: 0.1.0
Status: Draft

## Purpose

CMDL is the neutral language used to describe CAP Method questionnaires independently from Google Forms, Blazor, PDF or AI adapters.

## File format

The first implementation will use YAML because it is readable, versionable and easy to review.

## Root fields

| Field | Required | Description |
| --- | --- | --- |
| id | yes | Stable questionnaire id, for example FORM-001 |
| title | yes | Public title |
| version | yes | Semantic version |
| language | yes | Language code |
| module | no | CAP Method module reference |
| status | yes | draft, reviewed, validated, published or archived |
| estimatedDurationMinutes | no | Expected completion time |
| sections | yes | Ordered questionnaire sections |

## Section fields

| Field | Required | Description |
| --- | --- | --- |
| id | yes | Stable section id |
| title | yes | Section title |
| description | no | Section helper text |
| order | yes | Display order |
| questions | yes | Ordered questions |

## Question fields

| Field | Required | Description |
| --- | --- | --- |
| id | yes | Stable question id |
| type | yes | Question type |
| label | yes | Display label |
| required | no | Whether an answer is required |
| helpText | no | Optional guidance |
| options | no | Values for choice questions |
| rows | no | Rows for matrix questions |
| validation | no | Validation rules |
| scoring | no | Scoring rules |

## Question types

Initial types:

- text
- longText
- email
- phone
- date
- singleChoice
- multipleChoice
- rating5
- rating10
- matrix
- information
- consent
- fileReference

## Validation rules

Initial validation rules:

- required
- minLength
- maxLength
- min
- max
- pattern
- visibleWhen
- enabledWhen

## Versioning rules

Patch version:
- wording-only change;
- help text change;
- typo correction.

Minor version:
- new optional field;
- new optional question;
- new metadata.

Major version:
- removed question;
- changed question id;
- changed answer type;
- changed scoring meaning.

## Identifier rules

- Form ids use FORM-001 style.
- Section ids use uppercase semantic names.
- Question ids use stable prefixed ids such as Q001.
- Question ids must not be reused for a different meaning.

## Adapter mapping

CMDL definitions are compiled by adapters:

- Google Forms adapter maps CMDL to Apps Script calls.
- Blazor adapter maps CMDL to UI components.
- PDF adapter maps CMDL to printable documents.
- AI adapter maps CMDL to analysis prompts.

## First target

FORM-001 will be the reference implementation for the first CMDL migration.
