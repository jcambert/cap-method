# Statut Lot 11 - UI Blazor sessions CAP

## Branche

```text
feature/v3-lot11-cap-sessions-ui
```

## Objectif

Ajouter le flux métier authentifié suivant côté Blazor : création d'une session CAP depuis un bénéficiaire existant.

Le Lot 11 doit permettre :

- de créer une session CAP depuis le Client Blazor ;
- d'utiliser le token JWT stocké côté navigateur ;
- de transmettre le header `Authorization: Bearer` ;
- de sélectionner le bénéficiaire créé comme cible de la session ;
- de laisser le tenant et le consultant côté serveur ;
- de choisir si l'IA est activée pour la session ;
- d'afficher la session CAP créée.

## User stories du Lot 11

```text
US-SAAS-1101 - Ajouter l'appel API authentifié de création session CAP
US-SAAS-1102 - Ajouter une action UI de création session CAP depuis un bénéficiaire
US-SAAS-1103 - Permettre l'activation optionnelle de l'IA
US-SAAS-1104 - Afficher la session CAP créée
US-SAAS-1105 - Documenter le flux métier UI session CAP
```

## Règles

```text
Bénéficiaire requis avant création session = oui
TenantId saisi par l'utilisateur = non
ConsultantId saisi par l'utilisateur = non
TenantId résolu côté serveur via JWT = oui
ConsultantId résolu côté serveur via JWT = oui
Token Bearer requis = oui
IA optionnelle = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-1101 - Ajouter l'appel API authentifié de création session CAP

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- `CapMethodApiClient.CreateCapSessionAsync(...)` ajouté ;
- appel `POST /api/cap-sessions` ajouté ;
- header Bearer ajouté via token stocké ;
- réponse `CapSessionResponse` lue côté Client.

### US-SAAS-1102 - Ajouter une action UI de création session CAP depuis un bénéficiaire

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- section `Créer une session CAP` ajoutée après création bénéficiaire ;
- création de session basée sur `BeneficiaryId` retourné par le serveur ;
- reset de la session créée lorsqu'un nouveau bénéficiaire est créé.

### US-SAAS-1103 - Permettre l'activation optionnelle de l'IA

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- case à cocher `Activer l'IA pour cette session` ajoutée ;
- valeur transmise au endpoint de création session.

### US-SAAS-1104 - Afficher la session CAP créée

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- affichage `CapSessionId` ;
- affichage `TenantId` ;
- affichage `BeneficiaryId` ;
- affichage `ConsultantId` ;
- affichage `Status` ;
- affichage `IsAiEnabled` ;
- affichage date de création.

### US-SAAS-1105 - Documenter le flux métier UI session CAP

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- statut Lot 11 créé ;
- flux UI session CAP documenté.

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Squash merge dans `main`, puis suppression de la branche obsolète.
