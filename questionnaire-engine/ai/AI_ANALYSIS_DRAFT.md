# AI Analysis Draft

## Purpose

`AIAnalysisDraft` is the first `v2.0-ai` deliverable.

It is a consultant-only draft generated from an `AnalysisSnapshot`.

It does not replace the consultant review and must not be delivered directly to the beneficiary.

## Command

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

## Current provider

```text
deterministic-local-draft
```

The first implementation does not require any external AI provider.

This keeps the command usable in local development and future CI without API keys.

## Required sections

The generated Markdown must contain:

```text
# Analyse IA assistée

## Avertissement méthodologique
## Traçabilité
## Synthèse neutre des réponses
## Thèmes récurrents
## Valeurs exprimées
## Motivations apparentes
## Compétences évoquées
## Contraintes et freins
## Hypothèses professionnelles
## Points à clarifier
## Questions d entretien
## Risques d interprétation
## Validation consultant obligatoire
```

## Methodological rule

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

## Current limitations

- no external provider integration yet;
- no prompt execution yet;
- no model configuration yet;
- output is deterministic and based on the structured snapshot;
- qualitative interpretation remains intentionally cautious.

## Next step

Add validation checks to ensure the generated draft contains all mandatory sections and avoids forbidden definitive formulations.
