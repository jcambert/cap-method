# Authentification production minimale - v3.1

## Objectif

Le Lot 2 remplace l'usage exclusif du token de développement par un point d'entrée d'authentification minimal exploitable hors environnement local.

Cette étape ne constitue pas encore une gestion complète des comptes utilisateurs. Elle pose un socle contrôlé pour permettre de démarrer une mise en production métier progressive.

## Endpoints

```text
POST /api/auth/token
POST /api/dev/token       Development uniquement
GET  /api/me              protégé JWT
```

## Endpoint production

```http
POST /api/auth/token
Content-Type: application/json

{
  "email": "admin@cap-method.local",
  "password": "********"
}
```

Réponse attendue :

```json
{
  "accessToken": "...",
  "tokenType": "Bearer",
  "expiresAtUtc": "...",
  "tenantId": "...",
  "userId": "..."
}
```

## Configuration requise

Les valeurs de production doivent être fournies par configuration sécurisée ou variables d'environnement.

```text
Authentication__Jwt__Issuer
Authentication__Jwt__Audience
Authentication__Jwt__SigningKey
Authentication__ProductionUser__Email
Authentication__ProductionUser__PasswordSha256
Authentication__ProductionUser__TenantId
Authentication__ProductionUser__UserId
```

## Règles de sécurité

```text
JWT SigningKey par défaut interdite hors Development = oui
/api/dev/token exposé uniquement en Development = oui
Fallback user context autorisé uniquement en Development = oui
TenantId résolu côté serveur = oui
UserId résolu côté serveur = oui
Mot de passe stocké en SHA-256 hexadécimal = temporaire MVP
```

## Limite volontaire

Le stockage d'un utilisateur unique configuré n'est pas une solution finale de gestion des comptes.

Il sera remplacé dans un lot ultérieur par une vraie gestion utilisateur avec rôles, invitations, rotation de mot de passe et audit.

## Rôle dans la trajectoire v3.1

```text
Lot 2 = socle auth minimal production
Lot 3 = modèle workflow CAP
Lot ultérieur = gestion complète utilisateurs / rôles
```
