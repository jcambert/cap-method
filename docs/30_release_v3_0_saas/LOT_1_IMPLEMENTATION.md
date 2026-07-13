# Implémentation Lot 1 - Fondation applicative minimale

## Objectif

Documenter l'implémentation réalisée pour le démarrage du Lot 1 de `v3.0-saas`.

Le Lot 1 introduit le premier cas d'usage SaaS exécutable :

```text
Créer une session CAP
```

## Branche

```text
feature/v3-lot1-foundation
```

## Cas d'usage livré

```text
CreateCapSessionUseCase
```

Responsabilités :

- créer une session CAP rattachée à un tenant ;
- rattacher la session à un bénéficiaire ;
- rattacher la session à un consultant ;
- démarrer la session au statut `Draft` ;
- conserver l'IA désactivée par défaut ;
- activer l'IA uniquement si demandé explicitement ;
- persister la session via un port applicatif.

## Flux applicatif

```text
POST /api/cap-sessions
  ↓
CreateCapSessionRequest
  ↓
CreateCapSessionCommand
  ↓
CreateCapSessionUseCase
  ↓
CapSession.Create(...)
  ↓
ICapSessionRepository.SaveAsync(...)
  ↓
CapSessionResponse
```

## Fichiers ajoutés

### Application

```text
src/CapMethod.Saas/CapMethod.Saas.Application/Sessions/CreateCapSessionUseCase.cs
src/CapMethod.Saas/CapMethod.Saas.Application/Sessions/CreateCapSessionCommand.cs
src/CapMethod.Saas/CapMethod.Saas.Application/Sessions/CreateCapSessionResult.cs
src/CapMethod.Saas/CapMethod.Saas.Application/Sessions/ICapSessionRepository.cs
```

### Infrastructure

```text
src/CapMethod.Saas/CapMethod.Saas.Infrastructure/Sessions/InMemoryCapSessionRepository.cs
```

### Shared

```text
src/CapMethod.Saas/CapMethod.Saas.Shared/Sessions/CreateCapSessionRequest.cs
src/CapMethod.Saas/CapMethod.Saas.Shared/Sessions/CapSessionResponse.cs
```

### Server

```text
src/CapMethod.Saas/CapMethod.Saas.Server/Program.cs
```

### Tests

```text
src/CapMethod.Saas/CapMethod.Saas.Application.Tests/CapMethod.Saas.Application.Tests.csproj
src/CapMethod.Saas/CapMethod.Saas.Application.Tests/GlobalUsings.cs
src/CapMethod.Saas/CapMethod.Saas.Application.Tests/Sessions/CreateCapSessionUseCaseTests.cs
```

## Endpoint API

```http
POST /api/cap-sessions
```

### Requête

```json
{
  "tenantId": "00000000-0000-0000-0000-000000000000",
  "beneficiaryId": "00000000-0000-0000-0000-000000000000",
  "consultantId": "00000000-0000-0000-0000-000000000000",
  "enableAi": false
}
```

### Réponse

```json
{
  "capSessionId": "00000000-0000-0000-0000-000000000000",
  "tenantId": "00000000-0000-0000-0000-000000000000",
  "beneficiaryId": "00000000-0000-0000-0000-000000000000",
  "consultantId": "00000000-0000-0000-0000-000000000000",
  "status": "Draft",
  "isAiEnabled": false,
  "createdAtUtc": "2026-07-13T00:00:00Z"
}
```

## Tests ajoutés

```text
Create_should_create_cap_session_without_ai_by_default
Create_should_store_session
Create_should_enable_ai_only_when_requested
```

## Règles garanties

```text
Azure obligatoire = non
IA obligatoire = non
IA activée par défaut = non
Repository applicatif = oui
Tests d'application = oui
```

## Limites actuelles

- stockage mémoire uniquement ;
- pas encore de persistance PostgreSQL ;
- pas encore d'authentification ;
- pas encore de contrôle tenant par utilisateur connecté ;
- pas encore de tests API `WebApplicationFactory`.

## Prochaine étape fonctionnelle

Après validation CI :

```text
Lot 1 étape suivante = remplacer progressivement le stockage mémoire par un adapter PostgreSQL testé.
```
