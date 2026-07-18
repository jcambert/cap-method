# Questionnaires en ligne - Lot 6

## Objectif

Permettre au bénéficiaire authentifié de consulter, compléter, enregistrer et soumettre des questionnaires depuis son espace sécurisé.

## Périmètre livré

```text
Catalogue questionnaires = oui
Questionnaire détaillé = oui
Sauvegarde brouillon = oui
Soumission = oui
Validation champs obligatoires = oui
Contexte tenant/bénéficiaire résolu depuis JWT = oui
Analyse structurée = hors périmètre Lot 6
Persistance PostgreSQL = hors périmètre Lot 6
```

## Endpoints

```text
GET /api/beneficiary/questionnaires
GET /api/beneficiary/questionnaires/{questionnaireId}
GET /api/beneficiary/questionnaires/{questionnaireId}/progress
PUT /api/beneficiary/questionnaires/{questionnaireId}/answers
```

Tous les endpoints sont protégés et utilisent les claims `tenant_id` et `beneficiary_id` du token bénéficiaire.

## Questionnaires initiaux

```text
career-exploration
work-values
```

## Règles métier minimales

- une réponse inconnue est refusée ;
- la longueur maximale est contrôlée côté serveur ;
- une soumission exige toutes les réponses obligatoires ;
- un brouillon peut rester incomplet ;
- les données sont isolées par tenant, bénéficiaire et questionnaire.

## Limite volontaire

Le stockage du Lot 6 est en mémoire dans le processus serveur. Il valide le parcours fonctionnel et les contrats API. La persistance PostgreSQL durable sera intégrée dans un lot de durcissement dédié avant la mise en production.

## Prochaine étape

```text
Lot 7 - Analyse structurée SaaS
```
