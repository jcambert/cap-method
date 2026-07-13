# Démarrage Lot 1 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut

```text
STARTED
```

Le développement de `v2.0-ai` démarre sur une branche dédiée afin de préserver la stabilité de `v1.0-pro`.

## Objectif du Lot 1

Créer un premier socle IA sans fournisseur externe obligatoire.

Le but est de produire un brouillon d'analyse IA structuré à partir d'un `AnalysisSnapshot`.

## Chaîne cible du Lot 1

```text
AnalysisSnapshot
  ↓
generate-ai-analysis-draft.mjs
  ↓
ai-analysis-draft.md
```

## User stories couvertes

```text
US-AI-001 - Générer un brouillon d'analyse IA
US-AI-002 - Structurer l'analyse IA en sections obligatoires
US-AI-010 - Ajouter un avertissement méthodologique obligatoire
US-AI-013 - Créer la commande generate-ai-analysis-draft
US-AI-014 - Ajouter une génération de test sans fournisseur externe obligatoire
US-AI-016 - Marquer l'analyse IA comme brouillon non validé
```

## Livrables attendus

```text
questionnaire-engine/ai/
questionnaire-engine/ai/generated/
questionnaire-engine/tools/generate-ai-analysis-draft.mjs
questionnaire-engine/ai/AI_ANALYSIS_DRAFT.md
questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

## Contraintes

- L'IA doit rester optionnelle.
- Aucun fournisseur externe ne doit être obligatoire pour la CI.
- La sortie doit être un brouillon consultant.
- La validation consultant doit être obligatoire.
- Les commandes existantes de `v1.0-pro` doivent rester fonctionnelles.

## Sections obligatoires du brouillon

```text
# Analyse IA assistée

## Avertissement méthodologique
## Synthèse neutre des réponses
## Thèmes récurrents
## Valeurs exprimées
## Motivations apparentes
## Compétences évoquées
## Contraintes et freins
## Hypothèses professionnelles
## Points à clarifier
## Questions d'entretien
## Risques d'interprétation
## Validation consultant obligatoire
```

## Décision

Ce lot doit être terminé avant toute intégration d'un fournisseur IA réel.
