# Statut Lot 3 - Liste des sessions CAP d'un tenant

## Branche

```text
feature/v3-lot3-session-list
```

## Objectif

Ajouter la liste des sessions CAP d'un tenant depuis l'API, en respectant strictement l'isolation par tenant.

Le Lot 3 doit permettre :

- de récupérer toutes les sessions CAP d'un tenant ;
- d'exiger le `TenantId` pour toute liste ;
- de ne jamais retourner les sessions d'un autre tenant ;
- de retourner une liste vide si le tenant n'a aucune session ;
- de conserver le stockage via port applicatif ;
- de tester le cas d'usage sans dépendance Azure ;
- de conserver une CI build/test verte.

## User stories du Lot 3

```text
US-SAAS-301 - Lister les sessions CAP d'un tenant
US-SAAS-302 - Exposer un endpoint API de liste des sessions
US-SAAS-303 - Respecter l'isolation tenant lors du listing
US-SAAS-304 - Tester le cas d'usage de listing
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

## Implémentation validée

### US-SAAS-301 - Lister les sessions CAP d'un tenant

Statut :

```text
DONE
```

Réalisé :

- `ListCapSessionsUseCase` créé ;
- `ListCapSessionsQuery` créé ;
- `ListCapSessionResult` créé ;
- listing filtré par tenant.

### US-SAAS-302 - Exposer un endpoint API de liste des sessions

Statut :

```text
DONE
```

Réalisé :

- endpoint `GET /api/cap-sessions` ajouté ;
- `tenantId` requis en query string ;
- réponse `200 OK` avec tableau de sessions.

### US-SAAS-303 - Respecter l'isolation tenant lors du listing

Statut :

```text
DONE
```

Réalisé :

- port repository enrichi avec `ListByTenantAsync` ;
- adapter mémoire filtré par tenant ;
- aucune session cross-tenant retournée.

### US-SAAS-304 - Tester le cas d'usage de listing

Statut :

```text
DONE
```

Réalisé :

- tests liste tenant existant ;
- tests isolation tenant ;
- tests liste vide.

## Validation CI

```text
CI = OK
```

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Effectuer le squash merge de la PR Lot 3 vers `main`, puis supprimer la branche obsolète.
