# CAP Method Questionnaire Engine

This folder contains the future questionnaire engine for CAP Method.

## Goal

Provide a neutral source of truth for questionnaires, independent from Google Forms, Blazor, PDF or AI adapters.

## Main concepts

- CMDL: CAP Method Definition Language.
- Definition: one questionnaire described as YAML.
- Validator: checks that a definition is valid.
- Adapter: converts a definition into a target format.

## Current structure

```text
questionnaire-engine/
├── README.md
├── cmdl/
│   ├── specification.md
│   └── examples/
│       └── FORM-001.cmdl.yaml
└── tools/
    └── planned validators and builders
```

## Current status

Draft architecture. The existing Google Forms Apps Script generator remains the operational implementation.

## Next steps

1. Expand FORM-001 CMDL until it fully matches the current Google Forms generator.
2. Add CMDL examples for FORM-002 to FORM-010.
3. Add a validator.
4. Add a Google Forms adapter.
5. Add Blazor and PDF adapters later.
