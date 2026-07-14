# Statut Lot 9 - UI Blazor authentifiée

## Branche

```text
feature/v3-lot9-blazor-auth-ui
```

## Objectif

Créer une première UI Blazor WebAssembly capable d'utiliser l'authentification JWT ajoutée au Lot 8.

Le Lot 9 doit permettre :

- de générer un token de développement depuis le Client ;
- de stocker le token côté navigateur ;
- d'appeler les endpoints protégés avec un header Bearer ;
- d'afficher le contexte utilisateur serveur ;
- de préparer les futurs écrans bénéficiaires et sessions ;
- de conserver une UI simple, testable et compatible CI.

## User stories du Lot 9

```text
US-SAAS-901 - Ajouter un stockage navigateur du token
US-SAAS-902 - Ajouter un client API authentifié
US-SAAS-903 - Ajouter une connexion dev côté Blazor
US-SAAS-904 - Afficher le contexte utilisateur courant
US-SAAS-905 - Documenter l'usage UI authentifié
```

## Règles

```text
Token stocké localement = oui, temporaire dev
Header Authorization Bearer = oui
/api/dev/token utilisé uniquement en dev = oui
Endpoints métier préparés pour appels authentifiés = oui
UI finale production = non
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-901 - Ajouter un stockage navigateur du token

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `BrowserTokenStore` ajouté ;
- stockage local via `localStorage` ;
- suppression du token disponible.

### US-SAAS-902 - Ajouter un client API authentifié

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `CapMethodApiClient` ajouté ;
- ajout automatique du header Bearer sur `/api/me` ;
- gestion d'erreur HTTP standard via `EnsureSuccessStatusCode`.

### US-SAAS-903 - Ajouter une connexion dev côté Blazor

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- bouton `Connexion dev` ajouté ;
- appel `POST /api/dev/token` ;
- stockage du token retourné.

### US-SAAS-904 - Afficher le contexte utilisateur courant

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- appel authentifié `GET /api/me` ;
- affichage `TenantId` ;
- affichage `UserId` ;
- affichage état authentifié / fallback.

### US-SAAS-905 - Documenter l'usage UI authentifié

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- statut Lot 9 créé ;
- usage UI de développement documenté.

## Statut global

```text
IMPLEMENTED - CI TO VERIFY
```

## Prochaine étape

Attendre la CI automatique, corriger si nécessaire, valider la documentation, ouvrir la PR Lot 9 vers `main`, effectuer le squash merge, puis supprimer la branche obsolète.
