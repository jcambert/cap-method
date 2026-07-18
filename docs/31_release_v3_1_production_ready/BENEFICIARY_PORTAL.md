# Espace bénéficiaire sécurisé - Lot 5

## Objectif

Le Lot 5 introduit un socle d'espace bénéficiaire sécurisé, distinct du parcours consultant.

Ce lot prépare les questionnaires en ligne en créant un point d'entrée dédié au bénéficiaire, avec authentification séparée et contexte serveur propre.

## Périmètre livré

```text
Route UI bénéficiaire = /beneficiary
Endpoint login bénéficiaire = POST /api/beneficiary/auth/token
Endpoint contexte bénéficiaire = GET /api/beneficiary/me
Claim bénéficiaire dédié = beneficiary_id
Configuration bootstrap bénéficiaire = Authentication:BeneficiaryPortal
```

## Endpoints

### POST /api/beneficiary/auth/token

Permet de créer un token JWT bénéficiaire à partir de :

```text
Email
Code d'accès
```

Le code d'accès est vérifié côté serveur via un hash SHA-256 configuré.

### GET /api/beneficiary/me

Retourne le contexte bénéficiaire courant à partir du token Bearer :

```text
TenantId
BeneficiaryId
Email
IsAuthenticated
```

Aucun identifiant bénéficiaire n'est fourni par le client pour résoudre ce contexte.

## Configuration attendue

```text
Authentication__BeneficiaryPortal__Email
Authentication__BeneficiaryPortal__AccessCodeSha256
Authentication__BeneficiaryPortal__TenantId
Authentication__BeneficiaryPortal__BeneficiaryId
```

## Décisions de sécurité

```text
Portail bénéficiaire séparé du login consultant = oui
Claim beneficiary_id dédié = oui
Tenant résolu côté serveur = oui
BeneficiaryId résolu côté serveur = oui
Accès questionnaires complets = hors périmètre Lot 5
Persistance réponses = hors périmètre Lot 5
Gestion complète des invitations = hors périmètre Lot 5
```

## Limites connues

Le Lot 5 reste un socle. Il ne remplace pas encore une vraie gestion complète d'invitations bénéficiaires.

Les lots suivants devront ajouter :

```text
invitations bénéficiaire
expiration des accès
rotation des codes
questionnaires en ligne
sauvegarde des réponses
suivi d'avancement bénéficiaire
journalisation des accès sensibles
```
