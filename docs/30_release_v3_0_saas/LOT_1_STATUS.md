# Statut Lot 1 - Fondation applicative minimale

## Branche

```text
feature/v3-lot1-foundation
```

## Objectif

Créer une première fondation SaaS exploitable techniquement, sans encore couvrir tout le parcours CAP.

Le Lot 1 doit permettre :

- de créer une session CAP depuis l'API ;
- de conserver le mode CAP sans IA par défaut ;
- d'activer l'IA uniquement explicitement ;
- de stocker une session via un port applicatif ;
- de tester le cas d'usage sans dépendance Azure ;
- de conserver une CI build/test verte.

## User stories du Lot 1

```text
US-SAAS-101 - Créer une session CAP sans IA
US-SAAS-102 - Exposer un endpoint API de création de session
US-SAAS-103 - Stocker une session via un repository applicatif
US-SAAS-104 - Tester le cas d'usage de création de session
```

## Règles

```text
Azure obligatoire = non
IA obligatoire = non
Brouillon IA livrable = non
Tests obligatoires = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-101 - Créer une session CAP sans IA

Statut :

```text
IN_PROGRESS
```

Réalisé :

- `CreateCapSessionUseCase` créé ;
- `CreateCapSessionCommand` créé ;
- `CreateCapSessionResult` créé ;
- création sans IA par défaut ;
- activation IA uniquement si `EnableAi = true`.

### US-SAAS-102 - Exposer un endpoint API de création de session

Statut :

```text
IN_PROGRESS
```

Réalisé :

- endpoint `POST /api/cap-sessions` ajouté ;
- contrat `CreateCapSessionRequest` ajouté ;
- contrat `CapSessionResponse` ajouté.

### US-SAAS-103 - Stocker une session via un repository applicatif

Statut :

```text
IN_PROGRESS
```

Réalisé :

- port `ICapSessionRepository` créé ;
- adapter `InMemoryCapSessionRepository` créé pour le socle local ;
- stockage sans dépendance Azure.

### US-SAAS-104 - Tester le cas d'usage de création de session

Statut :

```text
IN_PROGRESS
```

Réalisé :

- projet `CapMethod.Saas.Application.Tests` ajouté ;
- tests du cas d'usage ajoutés ;
- CI mise à jour pour inclure `Application.Tests`.

## Statut global

```text
IMPLEMENTED - CI TO VERIFY
```

## Prochaine étape

Vérifier la CI de `feature/v3-lot1-foundation`, corriger si nécessaire, puis ouvrir la PR Lot 1 vers `main`.
