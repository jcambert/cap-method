# Statut Lot 3 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut global

```text
VALIDATED
```

## Objectif du Lot 3

Rendre le brouillon IA plus utile pour le consultant en préparation d'entretien.

Le Lot 3 ne cherche pas à produire une conclusion automatique.

Il structure :

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

## Réalisations validées

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

### Synthèse neutre renforcée

Statut :

```text
DONE
```

La section `Synthèse neutre des réponses` est désormais structurée en :

```text
Lecture factuelle
Éléments observables
Points à ne pas surinterpréter
```

Objectif : séparer les signaux observables des interprétations, sans produire de conclusion automatique.

### Thèmes récurrents renforcés

Statut :

```text
DONE
```

Chaque thème récurrent est désormais structuré avec :

```text
Signal observé
Lecture prudente
Exemples sources
Utilisation en entretien
```

Objectif : rendre chaque thème exploitable en entretien sans le transformer en certitude.

### Hypothèses professionnelles renforcées

Statut :

```text
DONE
```

Chaque hypothèse professionnelle est désormais structurée avec :

```text
Signal utilisé
Lecture prudente
Condition de validation
Question de validation
```

Objectif : rendre les hypothèses discutables, vérifiables et non définitives.

### Points à clarifier renforcés

Statut :

```text
DONE
```

Les points à clarifier sont désormais structurés en :

```text
Clarifications prioritaires
Informations à compléter
Décision consultant suggérée
```

Objectif : aider le consultant à décider quoi vérifier avant la synthèse finale.

### Questions d'entretien renforcées

Statut :

```text
DONE
```

Les questions sont désormais groupées en :

```text
Questions issues du snapshot
Questions sur les thèmes récurrents
Questions sur les hypothèses professionnelles
Questions sur les points de vigilance
```

Objectif : préparer un entretien ouvert, non orienté et directement relié aux signaux disponibles.

## Statut détaillé

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-003 | DONE | La synthèse neutre distingue lecture factuelle, éléments observables et points à ne pas surinterpréter. |
| US-AI-004 | DONE | Les thèmes récurrents sont structurés avec signal, lecture prudente, exemples et usage en entretien. |
| US-AI-005 | DONE | Les hypothèses professionnelles sont structurées avec signal, lecture prudente, condition et question de validation. |
| US-AI-006 | DONE | Les points à clarifier distinguent priorités, informations manquantes et décision consultant suggérée. |
| US-AI-007 | DONE | Les questions d'entretien sont structurées par source, thèmes, hypothèses et vigilances. |
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

## Décision

Le Lot 3 est validé.

Le brouillon IA est désormais exploitable comme support de préparation d'entretien consultant, sans produire de conclusion automatique.

## Prochaine étape recommandée

Démarrer le Lot 4 - Préparation provider IA réel :

```text
US-AI-015
US-AI-018
US-AI-020
```

Objectif : préparer l'intégration future d'un provider IA configurable tout en conservant le mode local déterministe pour la CI.
