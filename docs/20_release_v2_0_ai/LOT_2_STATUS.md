# Statut Lot 2 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut global

```text
IN_PROGRESS
```

Le Lot 2 renforce les garde-fous méthodologiques, la traçabilité et la compatibilité de la chaîne IA.

## Objectif du Lot 2

```text
Sécuriser le brouillon IA avant toute future intégration d'un fournisseur IA réel.
```

## User stories du Lot 2

```text
US-AI-008 - Appliquer des formulations prudentes
US-AI-009 - Bloquer les formulations interdites
US-AI-011 - Générer un manifest IA
US-AI-012 - Conserver la compatibilité avec v1.0-pro
US-AI-019 - Documenter les limites professionnelles de l'IA
```

## Réalisations actuelles

### Formulations prudentes

Statut :

```text
IN_PROGRESS
```

Les formulations prudentes sont intégrées dans le brouillon IA généré.

Exemples contrôlés :

```text
Les réponses suggèrent...
Une hypothèse possible est...
Ce point mérite validation...
Le consultant pourra explorer...
```

### Formulations interdites

Statut :

```text
IN_PROGRESS
```

Les formulations interdites sont déclarées dans les validateurs et dans le manifest IA.

Exemples contrôlés :

```text
Cette personne est
Cette personne doit
Le bon métier est
Il faut absolument
Son problème principal est
Le diagnostic est
Le profil psychologique est
```

### Manifest IA

Statut :

```text
DONE
```

Fichiers concernés :

```text
questionnaire-engine/tools/generate-ai-analysis-draft.mjs
questionnaire-engine/tools/validate-ai-analysis-manifest.mjs
questionnaire-engine/ai/AI_ANALYSIS_MANIFEST.md
```

Le manifest trace :

- la session ;
- le bénéficiaire ;
- le consultant ;
- la source `AnalysisSnapshot` ;
- le brouillon produit ;
- le provider ;
- le modèle ;
- le statut `draft` ;
- la validation consultant obligatoire ;
- les garde-fous appliqués ;
- le blocage de livraison au bénéficiaire ;
- l'absence de fournisseur externe obligatoire.

### Compatibilité v1.0-pro

Statut :

```text
IN_PROGRESS
```

La CI continue d'exécuter la chaîne complète existante :

```text
ResponseSession
  ↓
AnalysisSnapshot
  ↓
SynthesisDraft
  ↓
FinalSynthesis
  ↓
ActionPlan
  ↓
DOCX / PDF / ZIP
```

L'étape IA est ajoutée sans rendre l'IA obligatoire pour les livrables classiques.

### Documentation des limites professionnelles

Statut :

```text
TODO
```

La prochaine étape est de documenter explicitement les limites professionnelles de l'IA pour éviter tout usage abusif.

## Statut détaillé

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-008 | IN_PROGRESS | Les formulations prudentes sont utilisées et contrôlées. |
| US-AI-009 | IN_PROGRESS | Les formulations interdites sont contrôlées par validation automatique. |
| US-AI-011 | DONE | Le manifest IA est généré, documenté et validé en CI. |
| US-AI-012 | IN_PROGRESS | La chaîne v1.0-pro reste exécutée en CI. |
| US-AI-019 | TODO | Les limites professionnelles de l'IA doivent être documentées. |

## Commandes de référence

### Générer brouillon + manifest IA

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

### Valider le manifest IA

```bash
node questionnaire-engine/tools/validate-ai-analysis-manifest.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-manifest.json
```

## Prochaine étape recommandée

Créer la documentation des limites professionnelles :

```text
questionnaire-engine/ai/PROFESSIONAL_LIMITS.md
```

Objectif : terminer `US-AI-019`.
