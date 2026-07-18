# Lot 1 Status - v3.1-saas-production-ready

## Lot

```text
Lot 1 - Navigation SaaS par pages
```

## Branche

```text
feature/v3-1-lot1-saas-navigation
```

## Statut

```text
VALIDATED - CI OK
```

## Objectif

Remplacer l'écran unique hérité de `v3.0-saas` par une navigation SaaS par pages.

Ce lot prépare la production readiness en séparant les premiers parcours consultant : tableau de bord, bénéficiaires et sessions CAP.

## Fonctionnalités livrées

```text
Routage Blazor = ajouté
Layout SaaS principal = ajouté
Navigation latérale = ajoutée
Page tableau de bord = ajoutée
Page bénéficiaires = ajoutée
Page sessions CAP = ajoutée
Page 404 = ajoutée
Styles communs déplacés dans le layout = oui
```

## Pages créées

```text
/              -> tableau de bord
/beneficiaries -> création bénéficiaire
/cap-sessions  -> création, liste et détail session CAP
```

## Fichiers modifiés

```text
src/CapMethod.Saas/CapMethod.Saas.Client/App.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Layout/MainLayout.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Pages/DashboardPage.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Pages/BeneficiariesPage.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Pages/CapSessionsPage.razor
.github/workflows/v3-saas-validation.yml
docs/31_release_v3_1_production_ready/LOT_1_STATUS.md
```

## Décisions

```text
Authentification production = hors périmètre Lot 1
Gestion complète utilisateurs = hors périmètre Lot 1
Workflow CAP complet = hors périmètre Lot 1
Contrats API = inchangés
DTO = inchangés
Base de données = inchangée
```

## Critères de validation

```text
Application routée = validé
Navigation latérale disponible = validé
Parcours existants conservés = validé
Build client = validé
Tests existants = validé
CI GitHub Actions = validé
```

## Prochaine étape après merge

Démarrer le Lot 2 : authentification production minimale.
