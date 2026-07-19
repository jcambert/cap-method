# Lot 9 - Plan d'action

## Branche

`feature/v3-1-action-plan-lot-9`

## Statut

```text
IMPLEMENTED - CI TO VERIFY
```

## Objectif

Transformer la synthese validee du bilan en plan d'action operationnel, priorise et suivable par le consultant.

## Livrables

- Contrats partages `ActionPlanResponse`, `ActionPlanItemResponse`, `SaveActionPlanRequest` et `SaveActionPlanItemRequest`.
- Store serveur en memoire pour les plans d'action par tenant et beneficiaire.
- Endpoints consultant securises :
  - `GET /api/beneficiaries/{beneficiaryId}/action-plan` ;
  - `PUT /api/beneficiaries/{beneficiaryId}/action-plan` ;
  - `POST /api/beneficiaries/{beneficiaryId}/action-plan/items/{itemId}/complete`.
- Validation metier :
  - titre obligatoire ;
  - categorie obligatoire ;
  - priorite obligatoire ;
  - maximum 20 actions ;
  - validation interdite sur plan vide ;
  - modification interdite apres validation via l'endpoint de sauvegarde.
- Client Blazor consultant integre dans la page des sessions CAP.
- Ajout, validation et completion d'actions depuis l'UI.
- Client HTTP `ActionPlanApiClient`.
- Tests HTTP serveur avec `WebApplicationFactory`.

## Validation attendue

- Restore solution `.slnx`.
- Build solution.
- Tests domaine, application, infrastructure, compatibilite, serveur et Aspire.
- Verification des artefacts CI et couverture.

## Limites assumees

- Persistance PostgreSQL durable repoussee au Lot 11.
- Workflow de relance automatique non implemente dans ce lot.
- UI avancee drag and drop / Kanban non incluse.

## Suite

Apres validation CI, fusionner la PR puis passer au lot suivant de la release v3.1.
