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
- d'activer l'IA uniquement plus tard et explicitement ;
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
```

## Statut

```text
STARTED
```

## Prochaine étape

Implémenter `CreateCapSessionUseCase`, `ICapSessionRepository`, un adapter mémoire local et l'endpoint `POST /api/cap-sessions`.
