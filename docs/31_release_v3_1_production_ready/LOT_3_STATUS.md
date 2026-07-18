# Lot 3 Status - v3.1-saas-production-ready

## Lot

```text
Lot 3 - Modèle workflow CAP
```

## Branche

```text
feature/v3-1-lot3-cap-workflow-model
```

## Statut

```text
VALIDATED - CI OK
```

## Objectif

Introduire un modèle métier de workflow CAP permettant de piloter progressivement une session de bilan de compétences.

Ce lot reste centré sur le domaine. Il ne modifie pas encore l'UI métier complète ni les contrats API.

## Fonctionnalités livrées

```text
CapWorkflowStep = ajouté
CapWorkflowStepState = ajouté
CapWorkflowProgress = ajouté
CapWorkflowPlan = ajouté
CapSession.GetWorkflowProgress() = ajouté
Méthodes de progression session = ajoutées
Tests domaine workflow = ajoutés
Documentation workflow = ajoutée
```

## Étapes métier couvertes

```text
Intake
Questionnaires
Responses
StructuredAnalysis
AiDraft optionnel
ConsultantReview
Synthesis
Delivery
Archive
```

## Décisions

```text
Enum = non
switch/case = non
Workflow dérivé de CapSession.Status = oui
Workflow persisté séparément = non
Migration EF = non
Contrat API = inchangé
UI complète workflow = hors périmètre Lot 3
```

## Fichiers modifiés

```text
src/CapMethod.Saas/CapMethod.Saas.Domain/Sessions/CapSession.cs
src/CapMethod.Saas/CapMethod.Saas.Domain/Workflow/CapWorkflowStep.cs
src/CapMethod.Saas/CapMethod.Saas.Domain/Workflow/CapWorkflowStepState.cs
src/CapMethod.Saas/CapMethod.Saas.Domain/Workflow/CapWorkflowProgress.cs
src/CapMethod.Saas/CapMethod.Saas.Domain/Workflow/CapWorkflowPlan.cs
src/CapMethod.Saas/CapMethod.Saas.Domain.Tests/Sessions/CapSessionTests.cs
docs/31_release_v3_1_production_ready/CAP_WORKFLOW_MODEL.md
docs/31_release_v3_1_production_ready/LOT_3_STATUS.md
.github/workflows/v3-saas-validation.yml
```

## Critères de validation

```text
Build server = validé
Build client = validé
Tests domaine = validé
Tests application = validé
Tests infrastructure = validé
Tests compatibilité = validé
CI GitHub Actions = validé
```

## Prochaine étape après merge

Démarrer le Lot 4 : UI workflow CAP.
