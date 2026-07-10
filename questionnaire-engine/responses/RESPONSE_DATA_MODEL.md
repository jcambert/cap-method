# Response Data Model

## Purpose

Define the target structure used to normalize Google Forms answers into CAP Method data.

The goal is to make answers usable for:

- consultant review;
- analysis;
- synthesis;
- deliverable generation;
- future CAP Method Studio sessions.

## Source

Initial source:

- Google Forms response spreadsheets generated from CMDL questionnaires.

Future sources:

- CAP Method Studio forms;
- imported CSV files;
- API submissions.

## Core concepts

### ResponseSession

Represents one beneficiary journey.

```yaml
id: string
beneficiaryId: string
consultantId: string
startedAt: datetime
completedAt: datetime?
status: draft | active | completed | archived
source: google_forms | studio | import
forms: ResponseForm[]
```

### ResponseForm

Represents one submitted form for a session.

```yaml
id: string
sessionId: string
formId: string
formTitle: string
submittedAt: datetime
sourceSheetId: string?
sourceRowNumber: number?
answers: ResponseAnswer[]
```

### ResponseAnswer

Represents one normalized answer.

```yaml
id: string
sessionId: string
formId: string
questionId: string
questionLabel: string
questionType: string
rawValue: string
normalizedValue: string | number | boolean | string[] | null
answeredAt: datetime
source: google_forms
```

## Identity rules

Stable identifiers must come from CMDL:

- `formId` comes from CMDL root `id`;
- `questionId` comes from CMDL question `id`;
- `questionLabel` comes from CMDL question `label`.

Google Forms column headers should not be treated as stable identifiers.

They are useful for mapping but not for long-term identity.

## Normalization rules

| CMDL type | Raw Google value | Normalized value |
| --- | --- | --- |
| text | string | string |
| email | string | string |
| phone | string | string |
| longText | string | string |
| singleChoice | string | string |
| multipleChoice | comma-separated string or list | string[] |
| rating5 | number/string | number |
| rating10 | number/string | number |
| date | date/string | date |
| information | none | null |

## Required metadata

Every normalized answer must preserve:

- source form id;
- source question id;
- source question label;
- source row number if imported from a spreadsheet;
- source timestamp;
- raw value;
- normalized value.

## Minimal import flow

```text
Google Sheet row
  ↓
Column mapping
  ↓
CMDL question lookup
  ↓
ResponseAnswer
  ↓
ResponseForm
  ↓
ResponseSession
```

## Validation rules

A normalized response is valid when:

- the form id exists in CMDL;
- every mapped question id exists in CMDL;
- required questions have a non-empty value;
- rating values are within expected range;
- choice values exist in CMDL options;
- multiple choice values are represented as arrays.

## Analysis readiness

A response session is ready for analysis when:

- FORM-001 is present;
- FORM-002 is present;
- FORM-003 is present;
- at least one project or action-plan form is present;
- no required answer is missing.

## Future storage

Future CAP Method Studio may persist this model in a database.

Candidate aggregates:

- Beneficiary
- Consultant
- ResponseSession
- ResponseForm
- ResponseAnswer
- AnalysisSnapshot
- Deliverable
