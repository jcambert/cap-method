# Lot 2 Status - v3.1-saas-production-ready

## Lot

```text
Lot 2 - Authentification production minimale
```

## Branche

```text
feature/v3-1-lot2-production-auth
```

## Statut

```text
VALIDATED - CI OK
```

## Objectif

Sortir du mode exclusivement développement en ajoutant un point d'entrée d'authentification minimal utilisable pour une mise en production progressive.

Ce lot ne met pas encore en place une gestion complète des comptes utilisateurs. Il introduit un utilisateur de bootstrap configuré, un endpoint de login production et des garde-fous de configuration.

## Fonctionnalités livrées

```text
POST /api/auth/token = ajouté
ProductionJwtTokenService = ajouté
ProductionAuthenticationOptions = ajouté
PasswordHashVerifier = ajouté
AccessTokenResponse commun = ajouté côté serveur et client
UI de connexion production = ajoutée au tableau de bord
/api/dev/token = conservé uniquement en Development
JWT SigningKey par défaut interdite hors Development = oui
Documentation auth production = ajoutée
```

## Décisions

```text
Gestion complète utilisateurs = hors périmètre Lot 2
Rôles complets = hors périmètre Lot 2
Inscription autonome = hors périmètre Lot 2
Reset password = hors périmètre Lot 2
Endpoint dev token = conservé mais limité à Development
TenantId client = toujours ignoré côté serveur
```

## Configuration production attendue

```text
Authentication__Jwt__Issuer
Authentication__Jwt__Audience
Authentication__Jwt__SigningKey
Authentication__ProductionUser__Email
Authentication__ProductionUser__PasswordSha256
Authentication__ProductionUser__TenantId
Authentication__ProductionUser__UserId
```

## Fichiers modifiés

```text
src/CapMethod.Saas/CapMethod.Saas.Server/Program.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/AccessTokenResponse.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/PasswordHashVerifier.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/ProductionAuthenticationOptions.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/ProductionJwtTokenService.cs
src/CapMethod.Saas/CapMethod.Saas.Server/Security/ProductionLoginRequest.cs
src/CapMethod.Saas/CapMethod.Saas.Server/appsettings.Development.json
src/CapMethod.Saas/CapMethod.Saas.Client/Auth/AccessTokenResponse.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Auth/ProductionLoginRequest.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Auth/CapMethodApiClient.cs
src/CapMethod.Saas/CapMethod.Saas.Client/Components/AuthSummaryCard.razor
src/CapMethod.Saas/CapMethod.Saas.Client/Pages/DashboardPage.razor
docs/31_release_v3_1_production_ready/AUTHENTICATION_PRODUCTION.md
docs/31_release_v3_1_production_ready/LOT_2_STATUS.md
.github/workflows/v3-saas-validation.yml
```

## Critères de validation

```text
Build server = validé
Build client = validé
Tests existants = validés
CI GitHub Actions = validée
```

## Prochaine étape après merge

Démarrer le Lot 3 : modèle workflow CAP.
