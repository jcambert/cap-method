# Test hardening status - v3.1

## Branche

`test/v3-1-hardening-from-main`

## Statut

```text
VALIDATED - CI OK
```

## Objectif

Corriger l'insuffisance de couverture constatee apres le Lot 8 avant de reprendre les lots fonctionnels.

## Livrables

- Nouveau projet `CapMethod.Saas.Server.Tests`.
- Tests HTTP reels avec `WebApplicationFactory`.
- Couverture des endpoints publics et proteges.
- Couverture de l'authentification consultant minimale.
- Rejet des requetes non authentifiees.
- Rejet des jetons mal formes.
- Separation des contextes consultant et beneficiaire sur les endpoints critiques.
- Couverture du workflow HTTP de synthese consultant.
- Collecte de couverture `XPlat Code Coverage` sur les projets de tests principaux.
- Publication des resultats de tests et fichiers Cobertura en artefacts CI.
- Solution `.slnx` mise a jour avec les tests serveur.

## Validation CI

- Restore solution `.slnx` : OK.
- Build solution : OK.
- Tests domaine, application, infrastructure, compatibilite, serveur et Aspire : OK.
- Artefacts de couverture disponibles dans la CI : OK.
- Aucune regression Aspire apres la modification locale du `launchSettings.json` : OK.

## Ce que ce lot ne pretend pas encore couvrir

- Tests UI Blazor avec bUnit.
- Seuil chiffrage bloquant de couverture.
- Tests exhaustifs de tous les cas questionnaires.
- Tests de concurrence complets.
- Tests PostgreSQL durables pour les stores memoire des lots 6 a 8.

Ces points restent a traiter dans les lots de durcissement suivants ou au Lot 11 pour la persistance.

## Suite

Reprendre le Lot 9.