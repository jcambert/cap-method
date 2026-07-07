# FORM-001 CMDL Reference Model

FORM-001 is the first enriched CMDL questionnaire definition.

## Purpose

It acts as the reference model for the migration of all other CAP Method forms.

## Current coverage

FORM-001 now covers:

- welcome message;
- personal information;
- professional situation;
- work satisfaction;
- reasons for starting;
- expectations;
- skills and achievements;
- points of attention;
- projection.

## Design decisions

- Stable question ids are used from Q001 to Q025.
- Choice questions include explicit options.
- Ratings use the rating10 type.
- Long answers use longText.
- The version was moved to 0.2.0 because the structure was expanded.

## Reuse rule

Future FORM-002 to FORM-010 enrichments should follow the same structure:

1. root metadata;
2. ordered sections;
3. stable question ids;
4. explicit question type;
5. required flag;
6. options for choice questions.
