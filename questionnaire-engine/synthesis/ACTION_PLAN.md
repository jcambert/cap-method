# Action Plan

## Purpose

The action plan is a structured Markdown deliverable generated from:

```text
AnalysisSnapshot JSON
FinalSynthesis Markdown
```

It turns the final synthesis into concrete next steps.

It is designed for consultant review and beneficiary validation.

## Flow

```text
ResponseSession JSON
  ↓
AnalysisSnapshot JSON
  ↓
SynthesisDraft Markdown
  ↓
FinalSynthesis Markdown
  ↓
ActionPlan Markdown
```

## Command

Default command:

```bash
node questionnaire-engine/tools/generate-action-plan.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/generate-action-plan.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/synthesis/generated/sample.final-synthesis.md \
  questionnaire-engine/synthesis/generated/sample.action-plan.md
```

## Output sections

The generated Markdown contains:

1. project target;
2. general objective;
3. initial indicators;
4. short-term actions;
5. mid-term actions;
6. long-term actions;
7. skills to strengthen;
8. training or resources;
9. network and job investigations;
10. constraints and vigilance points;
11. success conditions;
12. timeline;
13. follow-up questions;
14. validation checklist;
15. source appendix.

## Intended use

The consultant and beneficiary use this document to:

- clarify the retained project;
- translate the synthesis into actions;
- plan short, mid and long-term steps;
- identify skills gaps;
- structure professional investigations;
- track progress over time.

## Human validation required

Before use, the consultant must verify:

- the target project is explicit;
- actions are realistic;
- deadlines are adapted;
- constraints are considered;
- success criteria are measurable;
- the beneficiary knows the first action to perform.

## Current limitations

- Markdown output only;
- generic action templates;
- no personalized scheduling engine yet;
- no DOCX/PDF export yet;
- no follow-up tracking data model yet.

## Next target

Create an end-to-end deliverable generation command:

```text
ResponseSession JSON
  ↓
AnalysisSnapshot JSON
  ↓
SynthesisDraft Markdown
  ↓
FinalSynthesis Markdown
  ↓
ActionPlan Markdown
```

Then prepare DOCX/PDF exports once the Markdown deliverables are stable.
