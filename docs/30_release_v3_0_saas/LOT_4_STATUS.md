# Statut Lot 4 - Bénéficiaires CAP

## Branche

```text
feature/v3-lot4-beneficiaries
```

## Objectif

Introduire un vrai modèle bénéficiaire dans le socle SaaS v3 et empêcher la création de sessions CAP rattachées à un bénéficiaire inexistant.

Le Lot 4 doit permettre :

- de créer un bénéficiaire par tenant ;
- de stocker le bénéficiaire via un port applicatif ;
- d'exposer un endpoint API de création de bénéficiaire ;
- de rattacher une session CAP uniquement à un bénéficiaire existant ;
- de refuser la création d'une session si le bénéficiaire est absent ou appartient à un autre tenant ;
- de conserver une implémentation locale sans Azure ;
- de couvrir le domaine et les cas d'usage par tests.

## User stories du Lot 4

```text
US-SAAS-401 - Créer un bénéficiaire CAP
US-SAAS-402 - Exposer un endpoint API de création de bénéficiaire
US-SAAS-403 - Stocker les bénéficiaires via un port applicatif
US-SAAS-404 - Rattacher une session CAP à un bénéficiaire existant
US-SAAS-405 - Tester le domaine et les cas d'usage bénéficiaire
```

## Règles

```text
Azure obligatoire = non
IA obligatoire = non
Session sans bénéficiaire existant = non
Bénéficiaire cross-tenant = non
Tests obligatoires = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-401 - Créer un bénéficiaire CAP

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- modèle domaine `Beneficiary` créé ;
- normalisation des noms ;
- validation `TenantId`, `FirstName`, `LastName`.

### US-SAAS-402 - Exposer un endpoint API de création de bénéficiaire

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- endpoint `POST /api/beneficiaries` ajouté ;
- contrat `CreateBeneficiaryRequest` ajouté ;
- contrat `BeneficiaryResponse` ajouté.

### US-SAAS-403 - Stocker les bénéficiaires via un port applicatif

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- port `IBeneficiaryRepository` ajouté ;
- adapter `InMemoryBeneficiaryRepository` ajouté ;
- enregistrement DI ajouté.

### US-SAAS-404 - Rattacher une session CAP à un bénéficiaire existant

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `CreateCapSessionUseCase` vérifie désormais l'existence du bénéficiaire ;
- création de session refusée si le bénéficiaire est absent ;
- création de session refusée si le bénéficiaire n'appartient pas au tenant.

### US-SAAS-405 - Tester le domaine et les cas d'usage bénéficiaire

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- tests domaine `Beneficiary` ;
- tests `CreateBeneficiaryUseCase` ;
- tests `CreateCapSessionUseCase` adaptés au rattachement bénéficiaire.

## Statut global

```text
IMPLEMENTED - CI TO VERIFY
```

## Prochaine étape

Attendre la CI automatique, corriger si nécessaire, valider la documentation, ouvrir la PR Lot 4 vers `main`, effectuer le squash merge, puis supprimer la branche obsolète.
