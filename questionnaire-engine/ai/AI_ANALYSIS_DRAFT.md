# AI Analysis Draft

## Purpose

`AIAnalysisDraft` is the first `v2.0-ai` deliverable.

It is a consultant-only draft generated from an `AnalysisSnapshot`.

It does not replace the consultant review and must not be delivered directly to the beneficiary.

## Generate command

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

## Validation command

```bash
node questionnaire-engine/tools/validate-ai-analysis-draft.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

The validation command checks:

- required sections;
- section order;
- mandatory guardrail phrases;
- forbidden definitive formulations.

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

## Required guardrail phrases

The generated Markdown must contain:

```text
brouillon
validation consultant obligatoire
Les réponses suggèrent
Ce point mérite validation
```

## Forbidden formulations

The generated Markdown must not contain:

```text
Cette personne est
Cette personne doit
Le bon métier est
Il faut absolument
Son problème principal est
Le diagnostic est
Le profil psychologique est
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

Integrate the generation and validation commands into the CI workflow for `feature/v2-ai`.
