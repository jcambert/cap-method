# Validation - CAP Method v2.0-ai

## Statut

```text
BACKLOG VALIDATED
```

Le backlog fonctionnel `v2.0-ai` est validé.

Le document de référence est :

```text
docs/20_release_v2_0_ai/USER_STORIES.md
```

## Décision

Les user stories de `v2.0-ai` sont validées comme base de travail.

Le développement peut démarrer dans une branche dédiée, sans modifier le périmètre stable de `v1.0-pro`.

## Branche recommandée

```text
feature/v2-ai
```

## Règles de démarrage

Avant développement :

- conserver `v1.0-pro` comme version stable publiée ;
- ne pas modifier le fonctionnement existant sans compatibilité ;
- garder l'analyse IA optionnelle ;
- maintenir la validation consultant obligatoire ;
- commencer par le Lot 1 du backlog.

## Lot 1 validé pour démarrage

```text
US-AI-001
US-AI-002
US-AI-010
US-AI-013
US-AI-014
US-AI-016
```

## Objectif du premier lot

Produire un premier socle IA sans fournisseur externe obligatoire :

```text
AnalysisSnapshot
  ↓
generate-ai-analysis-draft.mjs
  ↓
ai-analysis-draft.md
```

La sortie doit contenir les sections obligatoires et les avertissements méthodologiques.

## Validation produit

Le jalon `v2.0-ai` est validé comme prochaine évolution réelle après `v1.0-pro`.
