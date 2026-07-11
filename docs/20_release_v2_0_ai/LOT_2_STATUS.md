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

Les formulations interdites sont déclarées dans les validateurs, dans le manifest IA et dans la documentation des limites professionnelles.

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
DONE
```

Fichier concerné :

```text
questionnaire-engine/ai/PROFESSIONAL_LIMITS.md
```

La documentation précise :

- ce que l'IA peut faire ;
- ce que l'IA ne doit pas faire ;
- les formulations autorisées ;
- les formulations interdites ;
- la validation consultant obligatoire ;
- l'interdiction de remise directe au bénéficiaire ;
- la traçabilité obligatoire ;
- les précautions liées aux données sensibles ;
- la responsabilité professionnelle du consultant.

## Statut détaillé

| User story | Statut | Commentaire |
|---|---|---|
| US-AI-008 | IN_PROGRESS | Les formulations prudentes sont utilisées et contrôlées. |
| US-AI-009 | IN_PROGRESS | Les formulations interdites sont contrôlées par validation automatique et documentées. |
| US-AI-011 | DONE | Le manifest IA est généré, documenté et validé en CI. |
| US-AI-012 | IN_PROGRESS | La chaîne v1.0-pro reste exécutée en CI. |
| US-AI-019 | DONE | Les limites professionnelles de l'IA sont documentées. |

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

Finaliser le Lot 2 en passant à `DONE` les garde-fous déjà couverts par la génération, la validation automatique, le manifest et la documentation :

```text
US-AI-008
US-AI-009
US-AI-012
```
