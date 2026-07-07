# CAP Method - Product Direction

## Why this document exists

The project must avoid drifting into technical work that does not directly serve the final CAP Method product.

CMDL, validators and generators are means, not the goal.

## Final objective

CAP Method must become a professional bilan de competences method and operating system.

The final product must help a consultant:

1. run a complete bilan de competences journey;
2. collect structured information from the beneficiary;
3. analyze answers consistently;
4. produce high quality deliverables;
5. follow the beneficiary after the journey;
6. reuse the same method through digital tools.

## Target users

Primary user:

- consultant or coach running the CAP Method process.

Secondary user:

- beneficiary completing questionnaires and receiving deliverables.

Future user:

- organization managing several consultants, beneficiaries and sessions.

## Product layers

### 1. Method layer

Contains the professional CAP Method content:

- modules;
- questions;
- exercises;
- consultant guides;
- deliverables;
- synthesis rules.

This is the core value.

### 2. Definition layer

Contains neutral structured definitions:

- CMDL questionnaires;
- stable question ids;
- validation rules;
- metadata.

This layer exists only to make the method reusable and automatable.

### 3. Automation layer

Generates operational tools:

- Google Forms;
- PDF documents;
- Blazor screens;
- AI prompts;
- exports.

### 4. Application layer

Future CAP Method Studio:

- sessions;
- beneficiaries;
- consultants;
- answers;
- synthesis;
- action plans;
- follow-up.

## Current milestone

Build a reliable source of truth for questionnaires and prove it by generating Google Forms from CMDL.

## What we are doing now

We are not building a generic form engine.

We are building the questionnaire source of truth required to industrialize CAP Method.

## Guardrails

Every new technical task must answer at least one of these questions:

1. Does it improve the CAP Method content?
2. Does it reduce manual work for the consultant?
3. Does it help generate a real deliverable?
4. Does it make answers easier to analyze?
5. Does it move us closer to CAP Method Studio?

If the answer is no, the task should not be done now.

## Stop doing for now

Do not spend time on:

- a generic form builder unrelated to CAP Method;
- complex schema theory;
- a full SaaS architecture before response collection is validated;
- AI synthesis before answer normalization exists;
- advanced UI before the method and data model are stable.

## Next concrete step

Generate Google Apps Script from CMDL for FORM-001.

Success criteria:

- read FORM-001 CMDL;
- produce a Google Apps Script builder file;
- keep the current working generator as fallback;
- compare generated structure with the existing Apps Script form;
- then extend generation to FORM-002 to FORM-010.
