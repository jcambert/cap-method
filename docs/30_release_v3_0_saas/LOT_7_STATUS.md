# Statut Lot 7 - Authentification et contexte tenant

## Branche

```text
feature/v3-lot7-auth-tenant-context
```

## Objectif

Introduire un contexte utilisateur et tenant côté Server afin que les endpoints SaaS ne dépendent plus d'un `TenantId` fourni par le client.

Le Lot 7 doit permettre :

- de résoudre le tenant courant depuis les claims HTTP ;
- de résoudre l'utilisateur courant depuis les claims HTTP ;
- d'utiliser le tenant serveur dans les endpoints métier ;
- de conserver un fallback contrôlé pour le développement local ;
- de préparer l'intégration future d'une authentification réelle ;
- de préserver l'isolation tenant côté use cases et repositories.

## User stories du Lot 7

```text
US-SAAS-701 - Créer un contexte utilisateur SaaS
US-SAAS-702 - Résoudre le tenant depuis les claims HTTP
US-SAAS-703 - Appliquer le tenant serveur aux endpoints métier
US-SAAS-704 - Ajouter un fallback de développement contrôlé
US-SAAS-705 - Documenter les règles de sécurité tenant
```

## Règles

```text
TenantId fourni par le client = non pour les endpoints métier
TenantId issu du contexte serveur = oui
Fallback développement = uniquement en Development
Authentification réelle complète = étape ultérieure
Tests existants obligatoires = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation validée

### US-SAAS-701 - Créer un contexte utilisateur SaaS

Statut :

```text
DONE
```

Réalisé :

- `CapUserContext` ajouté ;
- `ICapUserContextAccessor` ajouté ;
- endpoint technique `GET /api/me` ajouté.

### US-SAAS-702 - Résoudre le tenant depuis les claims HTTP

Statut :

```text
DONE
```

Réalisé :

- `HttpCapUserContextAccessor` ajouté ;
- claim `tenant_id` requis pour le tenant ;
- claim `NameIdentifier` requis pour l'utilisateur.

### US-SAAS-703 - Appliquer le tenant serveur aux endpoints métier

Statut :

```text
DONE
```

Réalisé :

- `POST /api/beneficiaries` utilise le tenant serveur ;
- `POST /api/cap-sessions` utilise le tenant serveur ;
- `GET /api/cap-sessions` utilise le tenant serveur ;
- `GET /api/cap-sessions/{capSessionId}` utilise le tenant serveur ;
- le consultant d'une session est dérivé de l'utilisateur courant.

### US-SAAS-704 - Ajouter un fallback de développement contrôlé

Statut :

```text
DONE
```

Réalisé :

- `Security:EnableDevelopmentUserContext` ajouté ;
- `Security:DevelopmentTenantId` ajouté ;
- `Security:DevelopmentUserId` ajouté ;
- fallback actif uniquement en environnement `Development`.

### US-SAAS-705 - Documenter les règles de sécurité tenant

Statut :

```text
DONE
```

Réalisé :

- statut Lot 7 créé ;
- règle d'isolation tenant serveur documentée ;
- authentification réelle complète reportée à un lot ultérieur.

## Validation CI

```text
CI = OK
```

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Effectuer le squash merge de la PR Lot 7 vers `main`, puis supprimer la branche obsolète.
