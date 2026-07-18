# Lot 4 Status - v3.1-saas-production-ready

## Lot

```text
Lot 4 - UI workflow CAP
```

## Branche

```text
feature/v3-1-lot4-workflow-ui
```

## Statut

```text
IMPLEMENTED - CI TO VERIFY
```

## Objectif

Exposer la progression métier CAP dans l'interface SaaS afin que le consultant visualise clairement où se situe une session dans le parcours.

Ce lot s'appuie sur le modèle de workflow du Lot 3 et reste sans modification de contrat API.

## Fonctionnalités livrées

```text
Résumé workflow dans la liste sessions = ajouté
Carte de progression workflow = ajoutée
Étape courante visible = oui
Pourcentage d'avancement visible = oui
États des étapes visibles = oui
Support IA optionnelle dans la projection UI = oui
```

## Fichiers modifiés

```text
src/CapMethod.Saas/CapMethod.Saas.Shared/Workflow/CapWorkflowStepView.cs
src/CapMethod.Saas/CapMethod.Saas.Shared/Workflow/CapWorkflowStepStateView.cs
src/CapMethod.Saas/CapMethod.Saas.Shared/Workflow/CapWorkflowProgressView.cs
src/CapMethod.Saas/CapMethod.Saas.Shared/Workflow/CapWorkflowProgressProjector.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Components/WorkflowProgressCard.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Components/CapSessionDetailCard.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Components/CapSessionListCard.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Layout/MainLayout.razor
docs/31_release_v3_1_production_ready/CAP_WORKFLOW_UI.md
docs/31_release_v3_1_production_ready/LOT_4_STATUS.md
.github/workflows/v3-saas-validation.yml
```

## Décisions

```text
Enum = non
switch/case = non
Contrat API = inchangé
Migration EF = non
Workflow persisté séparément = non
Questionnaires complets = hors périmètre Lot 4
Actions d'avancement = hors périmètre Lot 4
```

## Critères de validation

```text
Build server = à vérifier
Build client = à vérifier
Tests domaine = à vérifier
Tests application = à vérifier
Tests infrastructure = à vérifier
Tests compatibilité = à vérifier
CI GitHub Actions = à vérifier
```

## Prochaine étape après merge

Démarrer le Lot 5 : espace bénéficiaire sécurisé.
