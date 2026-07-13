# AI Provider Contract - CAP Method v2.0-ai

## Objectif

Ce document prépare l'intégration future d'un fournisseur IA réel sans rendre l'IA obligatoire.

Le mode actuel reste :

```text
deterministic-local-draft
```

Ce mode doit rester disponible pour :

- la CI ;
- les tests reproductibles ;
- les environnements sans clé API ;
- les usages où l'IA externe est désactivée.

## Principe d'architecture

```text
AnalysisSnapshot
  ↓
AIProvider
  ↓
AIAnalysisDraft
  ↓
AIAnalysisManifest
  ↓
ConsultantReview
```

## Contrat logique d'un provider IA

Un provider IA doit recevoir une entrée structurée :

```text
AnalysisSnapshot
PromptGuardrails
ProfessionalLimits
OutputContract
```

Il doit produire :

```text
AIAnalysisDraft
AIAnalysisManifest
```

## Entrée minimale

```json
{
  "snapshot": "AnalysisSnapshot",
  "language": "fr-FR",
  "outputFormat": "markdown",
  "requiresConsultantValidation": true,
  "guardrails": {
    "cautiousFormulationsRequired": true,
    "forbiddenFormulationsBlocked": true,
    "noDiagnosis": true,
    "noFinalRecommendation": true,
    "noDirectBeneficiaryDelivery": true
  }
}
```

## Sortie obligatoire

Le provider doit produire un brouillon Markdown respectant les sections suivantes :

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
## Préparation consultant
## Validation consultant obligatoire
```

## Manifest obligatoire

Toute génération provider doit produire un manifest contenant au minimum :

```json
{
  "provider": "provider-name",
  "model": "model-name",
  "status": "draft",
  "requiresConsultantValidation": true,
  "guardrailsApplied": true,
  "checks": {
    "draftGenerated": true,
    "manifestGenerated": true,
    "consultantOnlyDraft": true,
    "readyForBeneficiaryDelivery": false
  }
}
```

## Providers envisagés

```text
local-deterministic
openai
azure-openai
mistral
ollama
custom-http
```

Aucun provider externe n'est activé par défaut.

## Configuration cible

La configuration future pourra être portée par variables d'environnement :

```text
CAP_AI_PROVIDER=local-deterministic
CAP_AI_MODEL=none-local-deterministic
CAP_AI_API_KEY=
CAP_AI_ENDPOINT=
CAP_AI_ENABLED=false
```

Règle :

```text
CAP_AI_ENABLED=false
  => génération locale déterministe ou étape IA désactivée

CAP_AI_ENABLED=true
  => provider configuré explicitement
```

## Garde-fous obligatoires

Un provider réel doit respecter les mêmes règles que le mode local :

- produire un brouillon ;
- conserver la validation consultant obligatoire ;
- interdire les formulations définitives ;
- interdire les diagnostics ;
- interdire les recommandations professionnelles finales ;
- empêcher toute remise directe au bénéficiaire ;
- documenter provider, modèle, date et source ;
- conserver la possibilité de désactiver l'IA.

## Comportement en erreur

En cas d'erreur provider :

```text
provider indisponible
clé API absente
quota dépassé
réponse invalide
sections manquantes
garde-fous non respectés
```

Le système doit :

- échouer explicitement ;
- ne pas produire de livrable bénéficiaire ;
- conserver le diagnostic technique dans le manifest ou les logs ;
- permettre de revenir au mode local déterministe ;
- ne pas casser la chaîne v1.0-pro.

## CI

La CI doit continuer à utiliser uniquement :

```text
deterministic-local-draft
```

Aucune clé API ne doit être nécessaire pour valider le dépôt.

## Statut

```text
DESIGN READY
```
