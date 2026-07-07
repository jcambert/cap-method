# CMDL Validator Rules

## Purpose

This document lists the first validation rules required before building an executable validator.

## Definition rules

- The root object must define `id`, `title`, `version`, `language`, `status`, and `sections`.
- The questionnaire id must follow `FORM-001` format.
- The version must follow semantic versioning.
- The status must be one of: draft, reviewed, validated, published, archived.

## Section rules

- Section ids must be unique inside one questionnaire.
- Section order values must be unique.
- Each section must have at least one question, except information-only sections if explicitly allowed.

## Question rules

- Question ids must be unique inside one questionnaire.
- Each question must define a supported type.
- Choice questions must define options.
- Matrix questions must define rows.
- Rating questions must define their scale.

## Compatibility rules

A new version must be considered breaking when:

- a question id disappears;
- a question type changes;
- a scoring meaning changes;
- a required answer becomes optional or optional becomes required in a published form.

## Future implementation

The first validator may be implemented as:

- a Node.js script;
- a .NET console tool;
- or a GitHub Action.

The preferred long-term implementation is a .NET tool under `src/CapMethod.Questionnaires`.
