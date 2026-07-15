# Statut Lot 14 - Durcissement UI et préparation RC

## Branche

```text
feature/v3-lot14-ui-hardening-rc
```

## Objectif

Durcir l'UI Blazor avant release candidate en séparant les composants de présentation et en réduisant le rôle de `App.razor`.

Le Lot 14 doit permettre :

- de conserver le flux authentifié existant ;
- de séparer les cartes de présentation réutilisables ;
- de réduire la taille fonctionnelle de `App.razor` ;
- de préparer une future navigation par pages ;
- de limiter les risques avant release candidate.

## User stories du Lot 14

```text
US-SAAS-1401 - Extraire le résumé authentification/contexte utilisateur
US-SAAS-1402 - Extraire la liste des sessions CAP
US-SAAS-1403 - Extraire le détail d'une session CAP
US-SAAS-1404 - Conserver les flux métiers existants
US-SAAS-1405 - Documenter le durcissement UI pré-RC
```

## Règles

```text
Changement fonctionnel API = non
Changement de contrat DTO = non
Flux JWT existant conservé = oui
Flux bénéficiaire conservé = oui
Flux session conservé = oui
Liste session conservée = oui
Détail session conservé = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation actuelle

### US-SAAS-1401 - Extraire le résumé authentification/contexte utilisateur

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- composant `AuthSummaryCard.razor` ajouté ;
- affichage token courant conservé ;
- affichage contexte serveur conservé.

### US-SAAS-1402 - Extraire la liste des sessions CAP

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- composant `CapSessionListCard.razor` ajouté ;
- tableau des sessions conservé ;
- action `Rafraîchir` conservée ;
- action `Voir détail` conservée ;
- sélection visuelle conservée.

### US-SAAS-1403 - Extraire le détail d'une session CAP

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- composant `CapSessionDetailCard.razor` ajouté ;
- affichage complet du détail session conservé.

### US-SAAS-1404 - Conserver les flux métiers existants

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- `App.razor` conserve l'orchestration et les appels API ;
- aucun endpoint n'est modifié ;
- aucun DTO n'est modifié ;
- aucun changement serveur n'est introduit.

### US-SAAS-1405 - Documenter le durcissement UI pré-RC

Statut :

```text
VALIDATED - CI OK
```

Réalisé :

- statut Lot 14 créé ;
- règles de durcissement UI documentées.

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Lot 14 validé par CI. Effectuer le squash merge vers `main`, puis supprimer la branche obsolète.
