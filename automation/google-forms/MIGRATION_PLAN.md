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
- FORM-001 as the first externalized source note.

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

1. Keep the validated generator unchanged until all forms are externalized.
2. Create one Markdown source file per form.
3. Review every source file against the current Apps Script implementation.
4. Convert stable Markdown definitions to JSON or YAML.
5. Build a definition-driven generator.
6. Keep the current generator as a fallback until the new generator is validated.

## Release strategy

- v1.6.0: Google Forms generator baseline.
- v1.6.1: Markdown source definitions for all forms.
- v1.7.0: structured definitions and validation.
- v1.8.0: definition-driven generator.

## Principle

Do not break the working generator while building the future architecture.
