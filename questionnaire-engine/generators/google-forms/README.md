# CMDL to Google Forms Generator

## Goal

Generate Google Apps Script form builders from CMDL definitions.

## Why

The existing Apps Script generator works and remains the fallback.

The new generator must prove that CAP Method questionnaires can be maintained from one source of truth.

## Scope for first milestone

Input:

- `questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml`

Output:

- a generated Apps Script builder model for FORM-001;
- then a complete Apps Script form builder.

## Constraints

- Do not replace the working Apps Script generator yet.
- Start with FORM-001 only.
- Keep generated output readable.
- Keep CMDL as the source of truth.
- Keep Google Forms as the first target adapter.

## Mapping

| CMDL type | Apps Script helper |
| --- | --- |
| information | section header |
| text | TEXT |
| longText | PARAGRAPH |
| email | TEXT |
| phone | TEXT |
| date | DATE |
| singleChoice | MULTIPLE_CHOICE |
| multipleChoice | CHECKBOX |
| rating5 | SCALE_5 |
| rating10 | SCALE_10 |
| matrix | GRID |

## First implementation

The first implementation can be a simple Node script that reads CMDL text and emits a generated Apps Script file.

Later implementation can move to .NET once the model is stable.
