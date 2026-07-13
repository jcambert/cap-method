# Statut Lot 2 - CAP Method v2.0-ai

## Branche

```text
feature/v2-ai
```

## Statut global

```text
VALIDATED
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

## Réalisations validées

### Formulations prudentes

Statut :

```text
DONE
```

Les formulations prudentes sont intégrées dans le brouillon IA généré, documentées et contrôlées par validation automatique.

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
DONE
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
DONE
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
| US-AI-008 | DONE | Les formulations prudentes sont utilisées, documentées et contrôlées. |
| US-AI-009 | DONE | Les formulations interdites sont contrôlées par validation automatique et documentées. |
| US-AI-011 | DONE | Le manifest IA est généré, documenté et validé en CI. |
| US-AI-012 | DONE | La chaîne v1.0-pro reste exécutée en CI et l'IA reste optionnelle. |
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

## Décision

Le Lot 2 est validé.

Les garde-fous IA sont suffisamment posés pour poursuivre vers le Lot 3.

## Prochaine étape recommandée

Démarrer le Lot 3 - Analyse utile consultant :

```text
US-AI-003
US-AI-004
US-AI-005
US-AI-006
US-AI-007
US-AI-017
```

Objectif : enrichir le contenu du brouillon IA pour le rendre plus utile en préparation d'entretien consultant.
