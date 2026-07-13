# Procédure d'utilisation IA - CAP Method v2.0-ai

## Objectif

Cette procédure explique comment utiliser l'étape IA de `v2.0-ai` sans confondre le brouillon IA avec un livrable final.

## Chaîne d'utilisation

```text
ResponseSession
  ↓
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
AIAnalysisManifest
  ↓
ConsultantReview
  ↓
FinalSynthesis
  ↓
ActionPlan
```

## Pré-requis

Avant de générer un brouillon IA, il faut disposer d'un `AnalysisSnapshot`.

Exemple :

```bash
node questionnaire-engine/tools/analyze-response-session.mjs \
  questionnaire-engine/responses/generated/session.response.normalized.json \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json
```

## Générer un brouillon IA

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

Cette commande génère :

```text
questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
questionnaire-engine/ai/generated/sample.ai-analysis-manifest.json
```

## Valider le brouillon IA

```bash
node questionnaire-engine/tools/validate-ai-analysis-draft.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

La validation contrôle :

- les sections obligatoires ;
- l'ordre des sections ;
- les formulations de prudence ;
- l'absence de formulations interdites ;
- la section de préparation consultant.

## Valider le manifest IA

```bash
node questionnaire-engine/tools/validate-ai-analysis-manifest.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-manifest.json
```

La validation contrôle :

- le statut `draft` ;
- la validation consultant obligatoire ;
- les garde-fous appliqués ;
- l'absence de fournisseur externe obligatoire ;
- le fait que le brouillon n'est pas prêt pour livraison au bénéficiaire.

## Revue consultant obligatoire

Le consultant doit ensuite appliquer :

```text
questionnaire-engine/ai/CONSULTANT_REVIEW.md
```

La revue doit permettre de :

- confirmer les thèmes ;
- nuancer les hypothèses ;
- corriger les interprétations ;
- préparer les questions d'entretien ;
- décider ce qui peut passer dans la synthèse finale.

## Interdiction de remise directe

Le fichier suivant ne doit pas être remis directement au bénéficiaire :

```text
sample.ai-analysis-draft.md
```

Il s'agit d'un support de travail consultant.

## Livrables transmissibles

Seuls les livrables relus et validés peuvent être transmis :

```text
FinalSynthesis
ActionPlan
Exports DOCX/PDF
```

## Mode provider actuel

Le provider actuel est :

```text
deterministic-local-draft
```

Il ne nécessite :

- aucune clé API ;
- aucun fournisseur externe ;
- aucune connexion réseau ;
- aucune configuration secrète.

## Futur provider réel

L'intégration future d'un provider réel doit respecter :

```text
questionnaire-engine/ai/AI_PROVIDER_CONTRACT.md
```

## Règle finale

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```
