# Statut Lot 8 - Authentification JWT

## Branche

```text
feature/v3-lot8-jwt-authentication
```

## Objectif

Introduire une authentification réelle via JWT Bearer côté Server afin que les endpoints métier SaaS soient protégés par ASP.NET Core Authentication / Authorization.

Le Lot 8 doit permettre :

- de configurer JWT Bearer côté Server ;
- de valider les tokens entrants ;
- d'utiliser les claims `tenant_id` et `NameIdentifier` déjà introduits au Lot 7 ;
- de protéger les endpoints métier avec `RequireAuthorization` ;
- de fournir un endpoint de génération de token uniquement en développement ;
- de conserver le mode mémoire et PostgreSQL existants ;
- de documenter l'usage local du token.

## User stories du Lot 8

```text
US-SAAS-801 - Configurer l'authentification JWT Bearer
US-SAAS-802 - Protéger les endpoints métier
US-SAAS-803 - Générer un token local de développement
US-SAAS-804 - Valider les claims tenant et utilisateur
US-SAAS-805 - Documenter l'authentification JWT
```

## Règles

```text
Endpoints métier anonymes = non
/api/info anonyme = oui
/api/dev/token = développement uniquement
TenantId client = non
TenantId claim tenant_id = oui
UserId claim NameIdentifier = oui
Fallback développement = conservé mais non utilisé par les endpoints protégés sans token
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-801 - Configurer l'authentification JWT Bearer

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- package `Microsoft.AspNetCore.Authentication.JwtBearer` ajouté ;
- configuration `Authentication:Jwt` ajoutée ;
- validation `Issuer`, `Audience`, `SigningKey` ajoutée ;
- middleware `UseAuthentication` / `UseAuthorization` ajouté.

### US-SAAS-802 - Protéger les endpoints métier

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `GET /api/me` protégé ;
- `POST /api/beneficiaries` protégé ;
- `POST /api/cap-sessions` protégé ;
- `GET /api/cap-sessions` protégé ;
- `GET /api/cap-sessions/{capSessionId}` protégé.

### US-SAAS-803 - Générer un token local de développement

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `DevelopmentJwtTokenService` ajouté ;
- endpoint `POST /api/dev/token` ajouté uniquement en environnement `Development` ;
- le token contient les claims `tenant_id` et `NameIdentifier`.

### US-SAAS-804 - Valider les claims tenant et utilisateur

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- le contexte utilisateur du Lot 7 continue de lire `tenant_id` ;
- le contexte utilisateur du Lot 7 continue de lire `NameIdentifier` ;
- les endpoints métier utilisent uniquement le contexte serveur.

### US-SAAS-805 - Documenter l'authentification JWT

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- statut Lot 8 créé ;
- configuration JWT documentée ;
- endpoint de token local documenté.

## Commandes utiles

Générer un token local en développement :

```bash
curl -X POST http://localhost:5000/api/dev/token
```

Appeler un endpoint protégé :

```bash
curl http://localhost:5000/api/me \
  -H "Authorization: Bearer {access_token}"
```

## Statut global

```text
IMPLEMENTED - CI TO VERIFY
```

## Prochaine étape

Attendre la CI automatique, corriger si nécessaire, valider la documentation, ouvrir la PR Lot 8 vers `main`, effectuer le squash merge, puis supprimer la branche obsolète.
