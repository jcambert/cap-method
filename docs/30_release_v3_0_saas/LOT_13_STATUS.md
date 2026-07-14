# Statut Lot 13 - UI Blazor détail session CAP

## Branche

```text
feature/v3-lot13-cap-session-detail-ui
```

## Objectif

Ajouter le chargement et l'affichage du détail d'une session CAP existante depuis l'UI Blazor.

Le Lot 13 doit permettre :

- de charger une session CAP précise via `GET /api/cap-sessions/{capSessionId}` ;
- d'utiliser le token JWT stocké côté navigateur ;
- de transmettre le header `Authorization: Bearer` ;
- de sélectionner une session depuis la liste du tenant connecté ;
- d'afficher le détail complet retourné par le serveur ;
- de préparer une future navigation métier vers un écran dédié session.

## User stories du Lot 13

```text
US-SAAS-1301 - Ajouter l'appel API authentifié de détail session CAP
US-SAAS-1302 - Ajouter une action UI Voir détail depuis la liste des sessions
US-SAAS-1303 - Afficher le détail complet d'une session CAP
US-SAAS-1304 - Garantir l'isolation tenant côté serveur
US-SAAS-1305 - Documenter le flux UI détail session CAP
```

## Règles

```text
TenantId saisi par l'utilisateur = non
TenantId envoyé par query string = non
TenantId résolu côté serveur via JWT = oui
Token Bearer requis = oui
Session hors tenant = non exposée
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-1301 - Ajouter l'appel API authentifié de détail session CAP

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `CapMethodApiClient.GetCapSessionAsync(...)` ajouté ;
- appel `GET /api/cap-sessions/{capSessionId}` ajouté ;
- header Bearer ajouté via token stocké ;
- réponse `CapSessionResponse` lue côté Client.

### US-SAAS-1302 - Ajouter une action UI Voir détail depuis la liste des sessions

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- colonne `Action` ajoutée au tableau des sessions ;
- bouton `Voir détail` ajouté pour chaque session ;
- sélection visuelle de la session consultée.

### US-SAAS-1303 - Afficher le détail complet d'une session CAP

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- affichage `CapSessionId` ;
- affichage `TenantId` ;
- affichage `BeneficiaryId` ;
- affichage `ConsultantId` ;
- affichage `Status` ;
- affichage `IsAiEnabled` ;
- affichage `CreatedAtUtc`.

### US-SAAS-1304 - Garantir l'isolation tenant côté serveur

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- le client n'envoie pas de tenant dans la requête de détail ;
- le serveur utilise le contexte JWT ;
- l'endpoint existant reste protégé par `RequireAuthorization`.

### US-SAAS-1305 - Documenter le flux UI détail session CAP

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- statut Lot 13 créé ;
- flux UI de détail session documenté.

## Statut global

```text
IMPLEMENTED - CI TO VERIFY
```

## Prochaine étape

Attendre la CI automatique, corriger si nécessaire, valider la documentation, ouvrir la PR Lot 13 vers `main`, effectuer le squash merge, puis supprimer la branche obsolète.
