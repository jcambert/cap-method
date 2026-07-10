# Final Synthesis

## Purpose

The final synthesis is a structured Markdown deliverable generated from:

```text
AnalysisSnapshot JSON
SynthesisDraft Markdown
```

It is designed as the final synthesis structure for consultant review.

It is not automatically validated for delivery to the beneficiary.

## Flow

```text
ResponseSession JSON
  ↓
AnalysisSnapshot JSON
  ↓
SynthesisDraft Markdown
  ↓
FinalSynthesis Markdown
```

## Command

Default command:

```bash
node questionnaire-engine/tools/generate-final-synthesis.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/generate-final-synthesis.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/synthesis/generated/sample.synthesis-draft.md \
  questionnaire-engine/synthesis/generated/sample.final-synthesis.md
```

## Output sections

The generated Markdown contains:

1. rappel du contexte;
2. objectifs du bilan;
3. parcours et éléments recueillis;
4. compétences identifiées;
5. valeurs, besoins et motivations;
6. freins et contraintes;
7. pistes professionnelles étudiées;
8. projet retenu;
9. conditions de réussite;
10. conclusion consultant;
11. points à valider avant remise;
12. annexes.

## Intended use

The consultant uses this document to:

- prepare the final synthesis;
- complete missing narrative parts;
- validate facts with the beneficiary;
- structure final delivery;
- connect the synthesis with the action plan.

## Human validation required

Before delivery, the consultant must verify:

- factual accuracy;
- faithful representation of the beneficiary's situation;
- absence of over-promising;
- explicit constraints;
- coherence between retained project and action plan;
- final wording and professional tone.

## Current limitations

- Markdown output only;
- generated text remains generic in some sections;
- final narrative still requires consultant completion;
- no DOCX/PDF export yet;
- action plan generator is not included in this step.

## Next target

Create the action plan generator:

```text
FinalSynthesis Markdown
AnalysisSnapshot JSON
  ↓
ActionPlan Markdown
```
