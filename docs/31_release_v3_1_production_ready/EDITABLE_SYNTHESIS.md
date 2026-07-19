# Synthèse éditable - Lot 8

## Objectif

Permettre au consultant de produire une synthèse à partir de l'analyse structurée, de la modifier puis d'enregistrer une validation humaine explicite.

## Parcours

```text
Questionnaires soumis
  -> analyse structurée déterministe
  -> brouillon initial de synthèse
  -> relecture et édition consultant
  -> validation humaine
  -> contenu figé
```

## API consultant

```http
GET /api/beneficiaries/{beneficiaryId}/synthesis
PUT /api/beneficiaries/{beneficiaryId}/synthesis
```

Le tenant et l'identité du consultant sont résolus depuis le contexte JWT côté serveur. Aucun `TenantId` n'est accepté depuis le client.

## Règles métier

- Le brouillon initial est généré lors de la première lecture.
- Le contenu est limité à 30 000 caractères.
- Un contenu vide est refusé.
- La validation enregistre la date et l'identifiant du consultant.
- Une synthèse validée devient non modifiable.
- Les données sont isolées par tenant et bénéficiaire.
- L'IA n'est pas obligatoire.

## Interface

L'éditeur est intégré à la page consultant `/cap-sessions` après sélection d'une session.

Actions disponibles :

- enregistrer le brouillon ;
- valider humainement ;
- consulter les informations de validation.

## Limites actuelles

```text
Stockage synthèse = mémoire serveur
Historique des versions = non
Dévalidation / nouvelle version = non
Export DOCX/PDF = Lot 10
Migration EF Core = non
```

La persistance PostgreSQL durable reste obligatoire avant qualification production-ready.
