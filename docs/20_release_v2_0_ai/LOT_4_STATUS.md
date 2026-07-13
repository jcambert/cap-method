# Statut Lot 4 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut global

```text
VALIDATED
```

## Objectif du Lot 4

Préparer l'intégration future d'un provider IA réel sans casser :

- le mode local déterministe ;
- la CI ;
- la chaîne v1.0-pro ;
- la validation consultant obligatoire.

## User stories du Lot 4

```text
US-AI-015 - Préparer l'intégration future d'un fournisseur IA
US-AI-018 - Documenter la procédure d'utilisation IA
US-AI-020 - Suivre l'avancement des user stories
```

## Réalisations validées

### Contrat provider IA

Statut :

```text
DONE
```

Fichier ajouté :

```text
questionnaire-engine/ai/AI_PROVIDER_CONTRACT.md
```

Ce document définit :

- le contrat logique d'un provider IA ;
- l'entrée minimale ;
- la sortie obligatoire ;
- le manifest obligatoire ;
- les providers envisagés ;
- la configuration cible ;
- les garde-fous obligatoires ;
- le comportement en erreur ;
- la règle CI sans clé API.

### Procédure d'utilisation IA

Statut :

```text
DONE
```

Fichier ajouté :

```text
questionnaire-engine/ai/AI_USAGE.md
```

Ce document explique :

- comment générer un brouillon IA ;
- comment valider le brouillon ;
- comment valider le manifest ;
- comment appliquer la revue consultant ;
- quels fichiers ne doivent pas être remis au bénéficiaire ;
- quels livrables peuvent être transmis après validation.

### Suivi consolidé v2.0-ai

Statut :

```text
DONE
```

Fichier ajouté :

```text
docs/20_release_v2_0_ai/V2_AI_PROGRESS.md
```

Ce document consolide :

- l'état des lots ;
- l'état des user stories ;
- les livrables techniques ;
- les livrables documentaires ;
- la chaîne CI ;
- les garanties méthodologiques.

## Statut détaillé

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-015 | DONE | Le contrat provider IA futur est documenté. |
| US-AI-018 | DONE | La procédure d'utilisation IA est documentée. |
| US-AI-020 | DONE | Le suivi consolidé `v2.0-ai` est disponible. |

## Décision technique

Le mode suivant reste obligatoire pour la CI :

```text
deterministic-local-draft
```

Aucun fournisseur externe ne doit être requis pour valider le dépôt.

## Décision

Le Lot 4 est validé.

Le backlog `v2.0-ai` est désormais fonctionnellement couvert.

## Prochaine étape recommandée

Vérifier la CI finale sur `feature/v2-ai`, puis préparer la validation de branche avant intégration vers `main`.
