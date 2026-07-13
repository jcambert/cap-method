# AI Analysis Manifest

## Purpose

`AIAnalysisManifest` traces the generation of an AI-assisted analysis draft.

It is created next to the generated Markdown draft.

The manifest exists to make the AI step auditable and explicit.

## Generated files

When running:

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

The command generates:

```text
questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
questionnaire-engine/ai/generated/sample.ai-analysis-manifest.json
```

A custom manifest path can be passed as a fourth argument:

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  input.analysis-snapshot.json \
  output.ai-analysis-draft.md \
  output.ai-analysis-manifest.json
```

## Validation command

```bash
node questionnaire-engine/tools/validate-ai-analysis-manifest.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-manifest.json
```

The validation command checks:

- mandatory manifest fields;
- source traceability;
- output paths;
- provider and model values;
- draft status;
- consultant validation requirement;
- guardrails status;
- delivery blocking flags;
- compatibility with an optional AI step;
- guardrail lists.

## Manifest contract

The manifest contains:

```json
{
  "id": "sample-session.ai-analysis-manifest",
  "draftId": "sample-session.ai-analysis-draft",
  "sessionId": "sample-session",
  "beneficiaryId": "sample-beneficiary",
  "consultantId": "sample-consultant",
  "generatedAt": "datetime",
  "source": {
    "type": "analysis-snapshot",
    "id": "sample-session.analysis-snapshot",
    "path": "input.analysis-snapshot.json",
    "status": "ready_for_consultant_review"
  },
  "output": {
    "draftPath": "output.ai-analysis-draft.md",
    "manifestPath": "output.ai-analysis-manifest.json"
  },
  "provider": "deterministic-local-draft",
  "model": "none-local-deterministic",
  "status": "draft",
  "requiresConsultantValidation": true,
  "guardrailsApplied": true,
  "checks": {
    "draftGenerated": true,
    "manifestGenerated": true,
    "externalProviderRequired": false,
    "consultantOnlyDraft": true,
    "readyForBeneficiaryDelivery": false
  },
  "compatibility": {
    "v1ChainRequired": false,
    "aiOptional": true,
    "canRunWithoutApiKey": true
  }
}
```

## Required guarantees

The manifest must state that:

- the output is a draft;
- the output requires consultant validation;
- guardrails were applied;
- no external provider is required for the current implementation;
- the draft is not ready for beneficiary delivery;
- the AI step remains optional.

## CI validation

The GitHub Actions workflow validates both files:

```text
/tmp/sample.ai-analysis-draft.md
/tmp/sample.ai-analysis-manifest.json
```

The manifest validation is performed by:

```text
questionnaire-engine/tools/validate-ai-analysis-manifest.mjs
```

## Methodological rule

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

## Status

```text
IMPLEMENTED - CI VALIDATED
```
