# Statut Lot 3 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut global

```text
STARTED
```

## Objectif du Lot 3

Rendre le brouillon IA plus utile pour le consultant en préparation d'entretien.

Le Lot 3 ne cherche pas à produire une conclusion automatique.

Il vise à mieux structurer :

- la synthèse neutre ;
- les thèmes récurrents ;
- les hypothèses professionnelles ;
- les points à clarifier ;
- les questions d'entretien ;
- l'étape `ConsultantReview`.

## User stories du Lot 3

```text
US-AI-003 - Produire une synthèse neutre des réponses
US-AI-004 - Identifier les thèmes récurrents
US-AI-005 - Proposer des hypothèses professionnelles
US-AI-006 - Identifier les zones à clarifier
US-AI-007 - Générer des questions d'entretien
US-AI-017 - Préparer une étape ConsultantReview
```

## Démarrage réalisé

### ConsultantReview

Statut :

```text
DONE
```

Fichier ajouté :

```text
questionnaire-engine/ai/CONSULTANT_REVIEW.md
```

Ce document définit :

- le rôle de la revue humaine ;
- les entrées de la revue ;
- les sorties attendues ;
- une checklist de validation ;
- un tableau de décision consultant ;
- des questions d'entretien génériques ;
- le critère de passage vers `FinalSynthesis`.

## Statut détaillé

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-003 | IN_PROGRESS | La synthèse neutre existe mais doit être enrichie. |
| US-AI-004 | IN_PROGRESS | Les thèmes récurrents existent mais doivent être mieux préparés pour l'entretien. |
| US-AI-005 | IN_PROGRESS | Les hypothèses professionnelles existent mais doivent être mieux structurées. |
| US-AI-006 | IN_PROGRESS | Les points à clarifier existent mais doivent être reliés aux décisions consultant. |
| US-AI-007 | IN_PROGRESS | Les questions d'entretien existent mais doivent être enrichies. |
| US-AI-017 | DONE | L'étape `ConsultantReview` est documentée. |

## Chaîne cible enrichie

```text
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
ConsultantReview
  ↓
FinalSynthesis
  ↓
ActionPlan
```

## Prochaine étape recommandée

Enrichir la génération du brouillon IA pour ajouter une section de préparation consultant plus exploitable :

```text
priorités d'entretien
hypothèses à valider
hypothèses à écarter
questions ciblées
points de vigilance consultant
```
