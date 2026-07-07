# CAP Method - Questionnaire Engine Specification

## Status

Version: 0.1.0
Status: Draft
Scope: questionnaire definitions, validation, publishing, response collection, and future analysis.

## Purpose

The questionnaire engine is the future core used to describe, validate, publish, collect, and analyze CAP Method questionnaires.

It must not depend on Google Forms, Blazor, PDF, or any single delivery channel.

## Design principles

1. Keep one source of truth for every questionnaire.
2. Keep stable identifiers for forms, sections, and questions.
3. Separate business definitions from delivery adapters.
4. Preserve the existing Google Forms generator until the new engine is validated.
5. Make future exports possible: Google Forms, Blazor, PDF, HTML, Microsoft Forms, and AI analysis.

## Core concepts

### Questionnaire

A questionnaire is a versioned business object.

Minimum fields:

- id
- title
- version
- language
- module
- estimated duration
- sections
- status

### Section

A section groups questions and instructions.

Minimum fields:

- id
- title
- description
- order
- questions

### Question

A question collects one answer or one structured response.

Minimum fields:

- id
- label
- type
- required
- help text
- validation rules
- scoring rules

### Response

A response is linked to a session, a question, and a source.

Minimum fields:

- session id
- questionnaire id
- question id
- value
- source
- answered at
- definition version

### Session

A session represents one beneficiary journey.

Minimum fields:

- session id
- beneficiary id
- consultant id
- started at
- status
- completed questionnaires
- notes
- generated documents

## Supported question types

Initial list:

- text
- long text
- email
- phone
- date
- single choice
- multiple choice
- rating 5
- rating 10
- matrix
- information block
- consent
- file reference

## Definition lifecycle

A questionnaire goes through the following stages:

1. Draft
2. Reviewed
3. Validated
4. Published
5. Archived

Only validated or published definitions should be used to create production forms.

## Processing lifecycle

1. Load definition
2. Validate definition
3. Compile definition
4. Publish through an adapter
5. Collect responses
6. Normalize responses
7. Analyze responses
8. Generate reports

## Adapter model

Adapters transform the neutral definition into a target format.

Initial adapters:

- Google Forms adapter
- Markdown documentation adapter
- PDF adapter
- Blazor adapter
- AI prompt adapter

The engine owns the business model. Adapters own technical mapping only.

## Validation rules

The validator should check:

- unique questionnaire ids
- unique section ids within one questionnaire
- unique question ids within one questionnaire
- supported question types
- required choices for choice questions
- required rows for matrix questions
- valid scoring rules
- version compatibility

## Versioning

Definitions use semantic versioning.

Recommended rules:

- patch: wording change without data impact
- minor: new optional question or metadata
- major: removed question, renamed id, changed answer type, or changed scoring meaning

Question identifiers must remain stable whenever possible.

## Roadmap

### Phase 1

Markdown definitions for all existing forms.

### Phase 2

Structured YAML or JSON definitions.

### Phase 3

Definition validator.

### Phase 4

Definition-driven Google Forms builder.

### Phase 5

Response normalization and analysis model.

### Phase 6

CAP Method Studio integration.

## Non-goals for the first implementation

- Replace the current Apps Script generator immediately.
- Build the full SaaS platform.
- Automate AI synthesis before response normalization exists.
- Store sensitive beneficiary data in the repository.

## Immediate next step

Convert the current FORM-001 to FORM-010 Markdown notes into richer source definitions, then introduce structured YAML definitions once the content is stable.
