# ADR-0001 - Questionnaire Definition Language

## Status
Accepted

## Context
CAP Method currently generates Google Forms from Apps Script. The product roadmap requires additional targets such as Blazor, PDF and AI-driven analysis.

## Decision
Introduce a neutral Questionnaire Definition Language (CMDL) as the single source of truth.

Business definitions must be independent from delivery technologies.

## Consequences
Positive:
- one definition for all outputs;
- stable identifiers;
- easier versioning;
- reusable validation.

Trade-offs:
- initial investment in a compiler and validators;
- temporary coexistence with the current Apps Script generator.

## Next milestone
Define the first CMDL schema and migrate FORM-001 as the reference implementation.
