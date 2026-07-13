# Release notes - CAP Method v2.0-ai

## Version

```text
v2.0-ai
```

## Statut

```text
READY TO PUBLISH
```

## Résumé

`v2.0-ai` ajoute une analyse IA assistée au moteur CAP Method, tout en conservant la chaîne professionnelle existante `v1.0-pro`.

Cette version introduit un brouillon IA exploitable par le consultant, mais jamais transmissible directement au bénéficiaire.

## Principe directeur

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

## Nouveautés principales

### Brouillon IA assisté

Ajout de la commande :

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

Cette commande génère :

```text
AIAnalysisDraft
AIAnalysisManifest
```

### Validation automatique du brouillon IA

Ajout de la commande :

```bash
node questionnaire-engine/tools/validate-ai-analysis-draft.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

La validation contrôle :

- les sections obligatoires ;
- l'ordre des sections ;
- les formulations prudentes ;
- l'absence de formulations interdites ;
- la préparation consultant ;
- la validation consultant obligatoire.

### Manifest IA

Ajout de la commande :

```bash
node questionnaire-engine/tools/validate-ai-analysis-manifest.mjs \
  questionnaire-engine/ai/generated/sample.ai-analysis-manifest.json
```

Le manifest trace :

- la source `AnalysisSnapshot` ;
- la session ;
- le bénéficiaire ;
- le consultant ;
- le provider ;
- le modèle ;
- le statut `draft` ;
- les garde-fous ;
- l'interdiction de livraison directe.

### Garde-fous méthodologiques

Ajout de règles explicites :

```text
brouillon uniquement
validation consultant obligatoire
pas de diagnostic
pas de recommandation finale automatique
pas de remise directe au bénéficiaire
provider externe non obligatoire
```

### ConsultantReview

Ajout de la documentation :

```text
questionnaire-engine/ai/CONSULTANT_REVIEW.md
```

Elle définit :

- la checklist de revue ;
- le tableau de décision consultant ;
- les questions d'entretien ;
- le critère de passage vers `FinalSynthesis`.

### Préparation provider IA futur

Ajout de :

```text
questionnaire-engine/ai/AI_PROVIDER_CONTRACT.md
```

Objectif : préparer un futur provider configurable sans rendre l'IA obligatoire.

Le provider actuel reste :

```text
deterministic-local-draft
```

## CI/CD

Statut confirmé :

```text
OK
```

La CI continue de fonctionner sans clé API et sans provider externe.

## Backlog

```text
US-AI-001 à US-AI-020 = DONE
```

## Lots validés

```text
Lot 1 IA = VALIDATED
Lot 2 garde-fous = VALIDATED
Lot 3 analyse consultant = VALIDATED
Lot 4 provider futur = VALIDATED
```

## Compatibilité

`v2.0-ai` conserve la compatibilité avec `v1.0-pro`.

La chaîne classique reste disponible :

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
DOCX/PDF/ZIP
```

La chaîne IA ajoute :

```text
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
AIAnalysisManifest
  ↓
ConsultantReview
```

## Limites connues

- Le provider réel n'est pas encore implémenté.
- Le mode IA actuel est local déterministe.
- Le brouillon IA reste un support consultant, pas un livrable final.
- La validation humaine reste obligatoire.

## Décision de release

```text
v2.0-ai peut être publiée après création du tag GitHub.
```
