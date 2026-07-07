# Questionnaire Automation Migration Plan

## Objective

Move CAP Method Google Forms automation from a single validated Apps Script file to a maintainable source-of-truth architecture.

The existing generator must remain usable during the transition.

## Current state

The repository contains:

- a working Apps Script generator;
- a README;
- a changelog;
- a questionnaire catalog;
- a target definition schema;
- the first externalized FORM-001 source note.

## Target architecture

```text
automation/google-forms/
├── cap_method_google_forms_generator.gs
├── forms/
│   ├── FORM-001.md
│   ├── FORM-002.md
│   ├── ...
│   └── FORM-010.md
├── schema/
│   └── FORM_DEFINITION_SCHEMA.md
└── tools/
    └── future converters and validators
```

## Migration steps

### Step 1 - Preserve the validated generator

Keep `cap_method_google_forms_generator.gs` unchanged until every form has been externalized and reviewed.

### Step 2 - Externalize form definitions

Create one source file per form:

- FORM-001.md
- FORM-002.md
- FORM-003.md
- FORM-004.md
- FORM-005.md
- FORM-006.md
- FORM-007.md
- FORM-008.md
- FORM-009.md
- FORM-010.md

Each file should describe:

- metadata;
- purpose;
- sections;
- questions;
- answer types;
- expected consultant output.

### Step 3 - Validate consistency

Before changing the generator, check that each externalized definition matches the current Apps Script implementation.

### Step 4 - Introduce machine-readable definitions

Once the Markdown definitions are stable, convert them to JSON or YAML.

### Step 5 - Build a definition-driven generator

Create a new Apps Script builder that reads structured definitions and generates Google Forms.

### Step 6 - Deprecate the monolithic generator

Only after validation, move the current generator to a legacy folder or keep it as a stable fallback.

## Release strategy

Recommended releases:

- v1.6.0: Google Forms generator baseline.
- v1.6.1: Markdown source definitions for all forms.
- v1.7.0: structured definitions and validation.
- v1.8.0: definition-driven generator.

## Principle

Do not break the working generator while building the future architecture.
