# Lot 5 Status - v3.1-saas-production-ready

## Lot

```text
Lot 5 - Espace bénéficiaire sécurisé
```

## Branche

```text
feature/v3-1-lot5-beneficiary-space
```

## Statut

```text
IMPLEMENTED - CI TO VERIFY
```

## Objectif

Créer le socle d'un espace bénéficiaire protégé, séparé du parcours consultant, afin de préparer les questionnaires en ligne.

## Fonctionnalités livrées

```text
Page /beneficiary = ajoutée
POST /api/beneficiary/auth/token = ajouté
GET /api/beneficiary/me = ajouté
BeneficiaryPortalJwtTokenService = ajouté
BeneficiaryPortalAuthenticationOptions = ajouté
Claim beneficiary_id = ajouté
Contexte bénéficiaire serveur = ajouté
Navigation bénéficiaire = ajoutée
Documentation bénéficiaire = ajoutée
```

## Décisions

```text
Espace bénéficiaire séparé du consultant = oui
Contrat API consultant = inchangé
Questionnaires complets = hors périmètre Lot 5
Persistance réponses = hors périmètre Lot 5
Gestion complète invitations = hors périmètre Lot 5
Migration EF = non
```

## Fichiers modifiés

```text
src/CapMethod.Saas/CapMethod.Saas.Server/Program.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/BeneficiaryPortalAuthenticationOptions.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/BeneficiaryPortalLoginRequest.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/BeneficiaryAccessTokenResponse.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/BeneficiaryPortalContextResponse.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/BeneficiaryPortalJwtTokenService.cs
src/CapMethod.Saas/CapMethod.Saas.Server/appsettings.Development.json
src/CapMethod.Saas/CapMethod.Saas.Client/Auth/BeneficiaryPortalLoginRequest.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Auth/BeneficiaryAccessTokenResponse.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Auth/BeneficiaryPortalContextResponse.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Auth/CapMethodApiClient.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Pages/BeneficiaryPortalPage.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Layout/MainLayout.razor
docs/31_release_v3_1_production_ready/BENEFICIARY_PORTAL.md
docs/31_release_v3_1_production_ready/LOT_5_STATUS.md
.github/workflows/v3-saas-validation.yml
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

Démarrer le Lot 6 : questionnaires en ligne.
