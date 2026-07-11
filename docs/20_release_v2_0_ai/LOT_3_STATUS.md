# Statut Lot 3 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut global

```text
IN_PROGRESS
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

## Réalisations

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

### Préparation consultant dans le brouillon IA

Statut :

```text
DONE
```

Fichiers modifiés :

```text
questionnaire-engine/tools/generate-ai-analysis-draft.mjs
questionnaire-engine/tools/validate-ai-analysis-draft.mjs
```

Le brouillon IA contient maintenant une section dédiée :

```text
## Préparation consultant
```

Cette section structure :

- les priorités d'entretien ;
- les hypothèses à valider ;
- les hypothèses à écarter ou nuancer ;
- les questions ciblées ;
- les points de vigilance consultant.

## Statut détaillé

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-003 | IN_PROGRESS | La synthèse neutre existe et reste à enrichir si besoin. |
| US-AI-004 | IN_PROGRESS | Les thèmes récurrents existent et alimentent la préparation consultant. |
| US-AI-005 | IN_PROGRESS | Les hypothèses professionnelles existent et sont reprises dans les hypothèses à valider. |
| US-AI-006 | IN_PROGRESS | Les points à clarifier existent et alimentent les priorités d'entretien. |
| US-AI-007 | IN_PROGRESS | Les questions d'entretien sont enrichies par des questions ciblées. |
| US-AI-017 | DONE | L'étape `ConsultantReview` est documentée et préparée dans le brouillon. |

## Chaîne cible enrichie

```text
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
Préparation consultant
  ↓
ConsultantReview
  ↓
FinalSynthesis
  ↓
ActionPlan
```

## Prochaine étape recommandée

Renforcer la synthèse neutre et les thèmes récurrents pour terminer :

```text
US-AI-003
US-AI-004
```
