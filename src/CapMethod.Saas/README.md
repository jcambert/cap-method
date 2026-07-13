# CAP Method SaaS

## Version cible

```text
v3.0-saas
```

## Architecture

```text
Blazor WebAssembly hosted
```

## Objectif

Ce dossier contient le socle applicatif SaaS de CAP Method.

Le SaaS doit encapsuler :

```text
v1.0-pro = CAP sans IA
v2.0-ai  = CAP avec IA optionnelle
```

## Structure

```text
CapMethod.Saas.Client
CapMethod.Saas.Server
CapMethod.Saas.Shared
CapMethod.Saas.Application
CapMethod.Saas.Domain
CapMethod.Saas.Infrastructure
CapMethod.Saas.*.Tests
```

## Règles

```text
Pas de dépendance Azure obligatoire.
Pas de dépendance commerciale obligatoire.
Pas de remise automatique d'un brouillon IA.
Tests dès le démarrage.
Squash merge des branches feature.
Suppression des branches obsolètes après merge.
```

## Lot 0 - Socle technique

Statut :

```text
MERGED INTO MAIN
```

Contenu :

- architecture Blazor WebAssembly hosted ;
- contrat `CapEngineAdapter` ;
- stratégie de tests ;
- projets `Client`, `Server`, `Shared`, `Application`, `Domain`, `Infrastructure` ;
- premiers tests Domain, Infrastructure et Compatibility ;
- CI `v3-saas-validation`.

## Lot 1 - Fondation applicative minimale

Branche :

```text
feature/v3-lot1-foundation
```

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Premier cas d'usage :

```text
Créer une session CAP
```

Endpoint :

```http
POST /api/cap-sessions
```

Règles garanties à ce stade :

```text
CAP sans IA par défaut
IA activée uniquement explicitement
Stockage via ICapSessionRepository
Adapter mémoire local
Tests Application
Azure non obligatoire
```

Documentation associée :

```text
docs/30_release_v3_0_saas/LOT_1_STATUS.md
docs/30_release_v3_0_saas/LOT_1_IMPLEMENTATION.md
```
