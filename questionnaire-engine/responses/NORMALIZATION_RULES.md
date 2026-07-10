# Response Normalization Rules

## Purpose

Define how raw Google Forms spreadsheet values are transformed into structured CAP Method response data.

Normalization is required before analysis, synthesis or deliverable generation.

## Principle

Google Forms is a collection channel, not the source of truth.

CMDL remains the source of truth for:

- form ids;
- section ids;
- question ids;
- question types;
- labels;
- options;
- required flags.

## Mapping strategy

Google Sheets columns are mapped to CMDL questions by label during the first implementation.

Target strategy:

- use CMDL question ids in generated titles or metadata where possible;
- keep label-based mapping as fallback;
- store both raw column header and CMDL question id.

## Normalization by type

### text, email, phone, longText

Input:

```text
raw string
```

Output:

```yaml
normalizedValue: string
```

Rules:

- trim surrounding whitespace;
- preserve internal line breaks for longText;
- convert empty string to null.

### singleChoice

Input:

```text
one selected option
```

Output:

```yaml
normalizedValue: string
```

Rules:

- trim value;
- validate value exists in CMDL options;
- empty value is invalid if question is required.

### multipleChoice

Input:

```text
one or more selected options
```

Output:

```yaml
normalizedValue:
  - option 1
  - option 2
```

Rules:

- split by comma when imported from Google Sheets;
- trim each option;
- remove empty values;
- validate every value exists in CMDL options.

### rating5

Input:

```text
1 to 5
```

Output:

```yaml
normalizedValue: number
```

Rules:

- parse as integer;
- value must be between 1 and 5.

### rating10

Input:

```text
1 to 10
```

Output:

```yaml
normalizedValue: number
```

Rules:

- parse as integer;
- value must be between 1 and 10.

### date

Input:

```text
Google Forms date value
```

Output:

```yaml
normalizedValue: date
```

Rules:

- parse as ISO date when possible;
- keep raw value when parsing fails;
- report parsing warning.

### information

Input:

```text
none
```

Output:

```yaml
normalizedValue: null
```

Rules:

- information blocks are not expected in spreadsheet responses;
- ignore during answer import.

## Required answer validation

A required answer is missing when:

- raw value is null;
- raw value is empty string;
- normalized array is empty;
- numeric value cannot be parsed.

## Import warnings

Import should produce warnings for:

- unknown spreadsheet column;
- missing required answer;
- invalid option;
- invalid rating value;
- unknown form id;
- unknown question label;
- duplicate mapped question.

## Import result

The import process should return:

```yaml
sessionId: string
status: success | success_with_warnings | failed
formsImported: number
answersImported: number
warnings: ImportWarning[]
errors: ImportError[]
```

## Next implementation target

Build a first importer that reads a CSV export from one Google Sheet and produces normalized JSON.

First target:

```text
FORM-001 response CSV
  ↓
normalized ResponseForm JSON
```
