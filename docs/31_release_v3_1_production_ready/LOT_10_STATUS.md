# Lot 10 - Exports livrables

## Statut

`IMPLEMENTED - CI TO VERIFY`

## Objectif

Produire un livrable exploitable à partir de la synthèse et du plan d'action validés, sans permettre l'export d'un dossier incomplet et sans contourner l'isolation tenant.

## Livrables

- service `DeliverableExportService` ;
- export Markdown UTF-8 ;
- endpoint consultant sécurisé `GET /api/beneficiaries/{beneficiaryId}/deliverables/bilan.md` ;
- nom de fichier horodaté ;
- synthèse et plan d'action rassemblés dans un document unique ;
- contrôle des sections obligatoires ;
- contrôle d'appartenance tenant/bénéficiaire ;
- tests HTTP d'authentification, de refus des brouillons et d'export nominal.

## Règles métier

L'export est refusé lorsque :

- la synthèse n'est pas validée ;
- le plan d'action n'est pas validé ;
- la synthèse est vide ;
- le plan d'action ne contient aucune action ;
- une section ne correspond pas au tenant et au bénéficiaire demandés.

## Format livré

Le premier format est Markdown afin de garantir un fichier ouvert, lisible, versionnable et convertible ultérieurement en PDF ou DOCX sans imposer une dépendance documentaire lourde au serveur.

## Limites assumées

```text
PDF signé = hors Lot 10
DOCX = hors Lot 10
Historique des exports = Lot 11 ou ultérieur
Persistance PostgreSQL durable = Lot 11
Personnalisation graphique = ultérieure
```

## Validation attendue

- restore de `CapMethod.Saas.slnx` ;
- build complet ;
- tests domaine, application, infrastructure, serveur et Aspire ;
- CI GitHub Actions verte ;
- téléchargement d'un fichier Markdown après validation de la synthèse et du plan d'action.

## Prochaine étape

Lot 11 - Configuration production et persistance PostgreSQL de référence.