# Analyse structurée SaaS - v3.1

## Objectif

Transformer les réponses soumises dans l'espace bénéficiaire en données d'analyse structurées, traçables et reproductibles.

Cette étape ne produit pas encore la synthèse finale du bilan. Elle fournit une base factuelle destinée au consultant et au futur Lot 8.

## Entrées

Seuls les questionnaires ayant le statut `IsSubmitted = true` sont pris en compte.

L'isolation est réalisée avec les claims JWT suivants :

```text
tenant_id
beneficiary_id
```

Aucun identifiant de tenant ou de bénéficiaire n'est accepté depuis le navigateur pour générer l'analyse.

## Sortie

L'endpoint retourne :

- le nombre de questionnaires soumis ;
- le nombre de réponses exploitables ;
- le volume de texte analysé ;
- un score de complétude ;
- une liste de mots-clés dominants ;
- des indicateurs structurés avec score, maximum et preuve textuelle ;
- la date de génération.

## Endpoint

```http
GET /api/beneficiary/analysis
Authorization: Bearer <beneficiary-token>
```

## Règles déterministes

```text
Complétude = questionnaires soumis / questionnaires disponibles
Profondeur = volume de caractères exploitables
Diversité = nombre de mots-clés significatifs
Mots-clés = fréquence décroissante, puis ordre alphabétique
```

Les règles sont déterministes et ne font appel à aucun modèle d'IA.

## Sécurité et limites

- authentification bénéficiaire obligatoire ;
- isolation tenant/bénéficiaire obligatoire ;
- brouillons exclus de l'analyse ;
- aucune décision ou recommandation automatique ;
- validation humaine obligatoire avant toute synthèse professionnelle ;
- persistance durable de l'analyse hors périmètre du Lot 7 ;
- synthèse éditable hors périmètre du Lot 7.

## Tests

Les tests couvrent :

- l'analyse vide ;
- la génération des indicateurs et mots-clés ;
- l'isolation entre tenants et bénéficiaires.

## Prochaine étape

Lot 8 - Synthèse éditable.
