# Statut Lot 10 - UI Blazor bénéficiaires

## Branche

```text
feature/v3-lot10-beneficiaries-ui
```

## Objectif

Ajouter le premier flux métier authentifié côté Blazor : création d'un bénéficiaire depuis l'UI avec appel protégé vers `POST /api/beneficiaries`.

Le Lot 10 doit permettre :

- de créer un bénéficiaire depuis le Client Blazor ;
- d'utiliser le token JWT stocké côté navigateur ;
- de transmettre le header `Authorization: Bearer` ;
- de ne pas saisir le tenant côté client ;
- d'afficher le bénéficiaire créé ;
- de préparer la future création de session CAP depuis l'UI.

## User stories du Lot 10

```text
US-SAAS-1001 - Ajouter l'appel API authentifié de création bénéficiaire
US-SAAS-1002 - Ajouter un formulaire Blazor de création bénéficiaire
US-SAAS-1003 - Afficher le bénéficiaire créé
US-SAAS-1004 - Garantir que le tenant reste côté serveur
US-SAAS-1005 - Documenter le flux métier UI bénéficiaire
```

## Règles

```text
TenantId saisi par l'utilisateur = non
TenantId envoyé utilement par le client = non
TenantId résolu côté serveur via JWT = oui
Token Bearer requis = oui
UI production finale = non
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-1001 - Ajouter l'appel API authentifié de création bénéficiaire

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- `CapMethodApiClient.CreateBeneficiaryAsync(...)` ajouté ;
- appel `POST /api/beneficiaries` ajouté ;
- header Bearer ajouté via token stocké ;
- réponse `BeneficiaryResponse` lue côté Client.

### US-SAAS-1002 - Ajouter un formulaire Blazor de création bénéficiaire

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- champs `FirstName`, `LastName`, `Email` ajoutés ;
- validation minimale côté UI ajoutée ;
- bouton de création ajouté.

### US-SAAS-1003 - Afficher le bénéficiaire créé

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- affichage `BeneficiaryId` ;
- affichage `TenantId` retourné par le serveur ;
- affichage identité bénéficiaire ;
- affichage date de création.

### US-SAAS-1004 - Garantir que le tenant reste côté serveur

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- le formulaire ne demande pas de tenant ;
- le client envoie `Guid.Empty` dans le DTO existant ;
- le serveur ignore le `TenantId` de la requête et utilise le contexte JWT.

### US-SAAS-1005 - Documenter le flux métier UI bénéficiaire

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- statut Lot 10 créé ;
- flux UI bénéficiaire documenté.

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Effectuer le squash merge vers `main`, puis supprimer la branche obsolète.
