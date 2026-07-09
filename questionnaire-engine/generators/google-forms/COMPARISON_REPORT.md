# Google Forms Generator Comparison Report

## Purpose

This document records the comparison between:

- the existing manual Google Apps Script generator;
- the generated Google Forms suite produced from CMDL.

## Context

The manual generator was the first operational implementation and remains the fallback.

The CMDL generator was introduced to make CAP Method questionnaires maintainable from a single source of truth.

## Comparison scope

The comparison covers:

- form creation;
- section creation;
- question creation;
- choice options;
- required flags;
- response spreadsheet creation;
- response destination binding;
- Drive folder organization;
- manual execution in Google Apps Script.

## Result summary

| Criterion | Manual generator | CMDL generator | Status |
| --- | --- | --- | --- |
| Creates 10 forms | yes | yes | aligned |
| Creates response sheets | yes | yes | aligned |
| Connects forms to sheets | yes | yes | aligned |
| Uses Drive folder | yes | yes | aligned |
| Supports sections | yes | yes | aligned |
| Supports text questions | yes | yes | aligned |
| Supports paragraph questions | yes | yes | aligned |
| Supports choice questions | yes | yes | aligned |
| Supports checkbox questions | yes | yes | aligned |
| Supports rating questions | yes | yes | aligned |
| Manual Apps Script test | yes | yes | aligned |
| Source of truth | script code | CMDL files | improved in CMDL |
| CI validation | partial/manual | yes | improved in CMDL |

## Manual validation result

A manual Google Apps Script test was executed on the generated suite.

Result:

- generated Apps Script executed successfully;
- no correction was identified at this stage.

## Differences accepted

The generated suite is not a byte-for-byte copy of the manual generator.

Accepted differences:

- generated code has a different internal structure;
- CMDL is the source of truth instead of hand-written Apps Script;
- helper function names differ;
- generated folder name is specific to the generated suite;
- question content follows the enriched CMDL definitions.

## Decision

The CMDL generator is accepted as the candidate primary generator.

The manual generator remains available as fallback until the next release checkpoint.

## Migration decision

Status: candidate primary.

Meaning:

- new questionnaire changes should be made in CMDL first;
- generated Google Forms output should be produced from CMDL;
- the manual generator should not receive new functional changes unless needed as emergency fallback.

## Remaining work before full replacement

Before removing or archiving the manual generator:

1. run one complete beneficiary test campaign with generated forms;
2. export or inspect responses from generated sheets;
3. confirm answer collection works for all forms;
4. confirm no consultant workflow is missing;
5. tag a release checkpoint.

## Final recommendation

Use CMDL as the main questionnaire source of truth from now on.

Keep the manual generator in the repository as fallback during the next validation cycle.
