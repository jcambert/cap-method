# Statut Lot 12 - UI Blazor liste des sessions CAP

## Branche

```text
feature/v3-lot12-cap-sessions-list-ui
```

## Objectif

Ajouter l'affichage des sessions CAP existantes du tenant connecté depuis l'UI Blazor.

Le Lot 12 doit permettre :

- de charger les sessions CAP via `GET /api/cap-sessions` ;
- d'utiliser le token JWT stocké côté navigateur ;
- de transmettre le header `Authorization: Bearer` ;
- d'afficher les sessions du tenant connecté ;
- de rafraîchir manuellement la liste ;
- de rafraîchir automatiquement après création d'une session ;
- de préparer une future navigation vers le détail d'une session.

## User stories du Lot 12

```text
US-SAAS-1201 - Ajouter l'appel API authentifié de liste des sessions CAP
US-SAAS-1202 - Ajouter une UI de liste des sessions CAP
US-SAAS-1203 - Rafraîchir la liste après création d'une session
US-SAAS-1204 - Garantir l'isolation tenant côté serveur
US-SAAS-1205 - Documenter le flux UI liste des sessions CAP
```

## Règles

```text
TenantId saisi par l'utilisateur = non
TenantId envoyé par query string = non
TenantId résolu côté serveur via JWT = oui
Token Bearer requis = oui
Liste vide acceptée = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-1201 - Ajouter l'appel API authentifié de liste des sessions CAP

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- `CapMethodApiClient.ListCapSessionsAsync(...)` ajouté ;
- appel `GET /api/cap-sessions` ajouté ;
- header Bearer ajouté via token stocké ;
- réponse `CapSessionSummaryResponse[]` lue côté Client.

### US-SAAS-1202 - Ajouter une UI de liste des sessions CAP

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- section `Sessions CAP du tenant` ajoutée ;
- affichage en tableau ;
- affichage session, bénéficiaire, statut, IA, date de création.

### US-SAAS-1203 - Rafraîchir la liste après création d'une session

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- bouton `Rafraîchir les sessions` ajouté ;
- rechargement automatique après création de session ;
- rechargement initial après connexion dev.

### US-SAAS-1204 - Garantir l'isolation tenant côté serveur

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- le client n'envoie pas de tenant dans la requête de liste ;
- le serveur utilise le contexte JWT ;
- l'endpoint existant reste protégé par `RequireAuthorization`.

### US-SAAS-1205 - Documenter le flux UI liste des sessions CAP

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- statut Lot 12 créé ;
- flux UI de liste des sessions documenté.

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Squash merge dans `main`, puis suppression de la branche obsolète.
