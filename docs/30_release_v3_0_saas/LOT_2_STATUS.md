# Statut Lot 2 - Consultation des sessions CAP

## Branche

```text
feature/v3-lot2-session-read
```

## Objectif

Ajouter la consultation d'une session CAP existante depuis l'API, en respectant l'isolation par tenant.

Le Lot 2 doit permettre :

- de récupérer une session CAP par identifiant ;
- d'exiger le `TenantId` pour toute lecture ;
- de retourner `404` si la session n'existe pas ;
- de retourner `404` si le tenant ne correspond pas ;
- de conserver le stockage via port applicatif ;
- de tester le cas d'usage sans dépendance Azure ;
- de conserver une CI build/test verte.

## User stories du Lot 2

```text
US-SAAS-201 - Lire une session CAP existante
US-SAAS-202 - Exposer un endpoint API de consultation de session
US-SAAS-203 - Respecter l'isolation tenant lors de la lecture
US-SAAS-204 - Tester le cas d'usage de consultation de session
```

## Règles

```text
Azure obligatoire = non
IA obligatoire = non
Lecture cross-tenant = non
Tests obligatoires = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-201 - Lire une session CAP existante

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `GetCapSessionUseCase` créé ;
- `GetCapSessionQuery` créé ;
- `GetCapSessionResult` créé ;
- retour `null` si session absente.

### US-SAAS-202 - Exposer un endpoint API de consultation de session

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- endpoint `GET /api/cap-sessions/{capSessionId}` ajouté ;
- `tenantId` requis en query string ;
- réponse `200 OK` si la session existe ;
- réponse `404 NotFound` si absente.

### US-SAAS-203 - Respecter l'isolation tenant lors de la lecture

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- lecture par clé `TenantId + CapSessionId` ;
- aucun accès cross-tenant ;
- `404 NotFound` si le tenant ne correspond pas.

### US-SAAS-204 - Tester le cas d'usage de consultation de session

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- tests de lecture session existante ;
- tests session absente ;
- tests isolation tenant.

## Statut global

```text
IMPLEMENTED - CI TO VERIFY
```

## Prochaine étape

Vérifier la CI de `feature/v3-lot2-session-read`, corriger si nécessaire, puis ouvrir la PR Lot 2 vers `main`.
