# Architecture cible - CAP Method v2.0-ai

## Objectif architectural

Ajouter un niveau d'analyse IA assistée sans casser la chaîne stable `v1.0-pro`.

L'IA doit être une étape intermédiaire entre l'analyse structurée et la synthèse finale.

## Chaîne cible

```text
CSV réponses
  ↓
ResponseSession
  ↓
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
ConsultantReview
  ↓
FinalSynthesis
  ↓
ActionPlan
  ↓
Package
  ↓
DOCX / PDF / ZIP
```

## Nouveaux objets

### AIAnalysisDraft

Document Markdown généré par IA, destiné uniquement au consultant.

Chemin cible :

```text
questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

### AIAnalysisManifest

Métadonnées de génération IA.

Contenu attendu :

```json
{
  "sessionId": "sample-session",
  "source": "analysis-snapshot",
  "generatedAt": "datetime",
  "provider": "local-or-configured-provider",
  "model": "configured-model",
  "status": "draft",
  "requiresConsultantValidation": true,
  "guardrailsApplied": true
}
```

## Commande cible

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

## Contrat de sortie

Le fichier généré doit contenir au minimum :

```text
# Analyse IA assistée

## Avertissement méthodologique
## Synthèse neutre des réponses
## Thèmes récurrents
## Valeurs exprimées
## Motivations apparentes
## Compétences évoquées
## Contraintes et freins
## Hypothèses professionnelles
## Points à clarifier
## Questions d'entretien
## Risques d'interprétation
## Validation consultant obligatoire
```

## Garde-fous

L'architecture doit empêcher :

- une sortie IA directement livrée au bénéficiaire ;
- une conclusion définitive non validée ;
- un diagnostic psychologique ;
- une recommandation métier présentée comme certaine ;
- une modification automatique de la synthèse finale sans validation.

## Intégration future

À terme, `generate-final-synthesis.mjs` pourra prendre en entrée optionnelle :

```text
ai-analysis-draft.md
```

Mais la synthèse finale devra toujours passer par une étape de validation consultant.

## Règle de compatibilité

`v2.0-ai` doit rester compatible avec la chaîne `v1.0-pro`.

La génération IA doit être optionnelle.

Si l'IA est absente, la chaîne classique doit continuer à fonctionner.
