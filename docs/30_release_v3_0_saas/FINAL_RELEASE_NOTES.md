# Release Notes - v3.0-saas

## Version

```text
v3.0-saas
```

## Statut

```text
DRAFT - FINAL STABILIZATION
```

## Resume

`v3.0-saas` stabilise la premiere version SaaS de CAP Method apres la release candidate `v3.0-saas-rc1`.

Cette version fournit un socle web minimal base sur Blazor WebAssembly hosted et ASP.NET Core.

## Inclus

```text
Blazor WebAssembly hosted
API ASP.NET Core
Client / Server / Shared
Domain / Application / Infrastructure
Creation beneficiaire
Creation session CAP
Liste sessions CAP
Detail session CAP
JWT dev authentication
Contexte tenant serveur
EF Core PostgreSQL
Migrations EF Core initiales
Mode InMemory local
UI Blazor en composants
CI GitHub Actions v3 SaaS
Documentation RC et stabilisation finale
```

## Garanties

```text
CAP v1 sans IA obligatoire = conserve
CAP v2 avec IA optionnelle = conserve
IA automatique imposee = non
Diagnostic automatique final = non
Validation humaine des livrables = requise
TenantId non saisi cote UI
Tenant resolu cote serveur
Endpoints metier proteges par JWT
```

## Hors perimetre

```text
Paiement SaaS
Back-office admin complet
Gestion complete des comptes utilisateurs
Workflow complet de bilan par pages metier
Generation finale de livrables depuis l'UI SaaS
Deploiement Azure obligatoire
```

## Suite prevue

```text
v3.1-saas - workflow metier CAP complet cote SaaS
v3.2-saas - comptes utilisateurs et roles enrichis
v3.3-saas - generation progressive des livrables depuis l'UI
```
