# Statut Lot 1 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut global

```text
IMPLEMENTED - PENDING CI CONFIRMATION
```

Le Lot 1 de `v2.0-ai` dispose maintenant d'un socle fonctionnel complet :

```text
AnalysisSnapshot
  ↓
generate-ai-analysis-draft.mjs
  ↓
ai-analysis-draft.md
  ↓
validate-ai-analysis-draft.mjs
  ↓
CI validation
```

## Réalisations

### Génération IA locale déterministe

```text
questionnaire-engine/tools/generate-ai-analysis-draft.mjs
```

La commande génère un brouillon IA sans fournisseur externe obligatoire.

### Validation automatique

```text
questionnaire-engine/tools/validate-ai-analysis-draft.mjs
```

La commande vérifie :

- les sections obligatoires ;
- l'ordre des sections ;
- les formulations de prudence ;
- l'absence de formulations interdites.

### Documentation

```text
questionnaire-engine/ai/AI_ANALYSIS_DRAFT.md
```

La documentation décrit :

- la commande de génération ;
- la commande de validation ;
- les sections attendues ;
- les formulations obligatoires ;
- les formulations interdites ;
- les limites du mode actuel.

### CI

```text
.github/workflows/cmdl-validation.yml
```

La CI génère et valide le brouillon IA à partir du snapshot d'analyse produit pendant le workflow.

## User stories du Lot 1

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-001 | DONE | Le brouillon `ai-analysis-draft.md` est généré depuis `AnalysisSnapshot`. |
| US-AI-002 | DONE | Les sections obligatoires sont générées et validées. |
| US-AI-010 | DONE | L'avertissement méthodologique est présent dans la sortie. |
| US-AI-013 | DONE | La commande `generate-ai-analysis-draft.mjs` existe. |
| US-AI-014 | DONE | Le mode local déterministe ne nécessite aucun fournisseur externe. |
| US-AI-016 | DONE | Le brouillon est marqué comme non final et soumis à validation consultant. |

## User stories amorcées hors Lot 1

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-008 | IN_PROGRESS | Les formulations prudentes sont intégrées et validées. |
| US-AI-009 | IN_PROGRESS | Les formulations interdites sont détectées par validation automatique. |
| US-AI-012 | IN_PROGRESS | La CI confirme que la chaîne existante continue à fonctionner avec l'étape IA optionnelle. |

## Commandes de référence

### Générer le brouillon IA

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

### Valider le brouillon IA

```bash
node questionnaire-engine/tools/validate-ai-analysis-draft.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

## Critères remplis

```text
[x] génération d'un brouillon IA
[x] absence de fournisseur externe obligatoire
[x] sortie Markdown structurée
[x] avertissement méthodologique
[x] validation consultant obligatoire
[x] validation automatique des sections
[x] détection des formulations interdites
[x] intégration CI
[x] compatibilité avec la chaîne existante
```

## Décision

Le Lot 1 est fonctionnellement implémenté.

Il reste à vérifier le résultat de la CI GitHub Actions avant de le considérer comme définitivement validé.

## Prochaine étape recommandée

Une fois la CI confirmée verte :

```text
1. marquer le Lot 1 comme VALIDATED ;
2. démarrer le Lot 2 - Garde-fous ;
3. renforcer la traçabilité avec un manifest IA dédié.
```
