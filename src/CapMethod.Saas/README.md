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

Ce dossier contient le futur socle applicatif SaaS de CAP Method.

Le SaaS doit encapsuler :

```text
v1.0-pro = CAP sans IA
v2.0-ai  = CAP avec IA optionnelle
```

## Structure cible

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
```
