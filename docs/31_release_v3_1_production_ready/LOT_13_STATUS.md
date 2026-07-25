# Lot 13 — Audit et sécurité minimale

## Statut

`IMPLEMENTED - CI TO VERIFY`

## Objectif

Ajouter une traçabilité exploitable des modifications fonctionnelles et appliquer des protections HTTP défensives sans journaliser le contenu métier ni les secrets.

## Livré

- journal d'audit pour les requêtes API mutatives authentifiées (`POST`, `PUT`, `PATCH`, `DELETE`) ;
- isolation des événements par tenant ;
- identification de l'utilisateur à partir du JWT ;
- conservation de la méthode, de la ressource, du statut HTTP, de la corrélation et de l'horodatage ;
- exclusion des endpoints d'authentification ;
- absence de corps HTTP, réponse, email, réponse de questionnaire, synthèse ou plan d'action dans l'audit ;
- endpoint authentifié `GET /api/audit/events` ;
- limite de lecture contrôlée entre 1 et 500 événements ;
- rétention mémoire bornée à 500 événements par tenant pour cette première version ;
- en-têtes défensifs : `X-Content-Type-Options`, `X-Frame-Options`, `Referrer-Policy`, `Permissions-Policy` et `Content-Security-Policy` ;
- tests d'authentification, d'isolation, de journalisation et d'en-têtes de sécurité.

## Modèle d'audit

```text
EventId
TenantId
UserId
Action
Resource
StatusCode
CorrelationId
OccurredAtUtc
```

Le modèle ne contient volontairement aucune donnée métier libre afin de réduire les risques RGPD et de fuite de secrets.

## Limites assumées

Le stockage d'audit est en mémoire et borné dans le Lot 13. Pour une conservation réglementaire ou contractuelle, une évolution devra fournir un stockage append-only durable, une politique de rétention configurée et un mécanisme d'export sécurisé.

La politique CORS existante reste inchangée pour préserver les scénarios locaux et les tests. Sa restriction par liste d'origines fait partie de la configuration d'environnement à finaliser lors de la clôture de release.

## Validation attendue

```bash
dotnet restore src/CapMethod.Saas/CapMethod.Saas.slnx
dotnet build src/CapMethod.Saas/CapMethod.Saas.slnx --no-restore
dotnet test src/CapMethod.Saas/CapMethod.Saas.slnx --no-build
```

La CI GitHub Actions doit être verte avant fusion.