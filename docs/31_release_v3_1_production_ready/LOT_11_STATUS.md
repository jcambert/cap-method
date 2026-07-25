# Lot 11 - Configuration et persistance de production

## État

`IMPLEMENTED - CI TO VERIFY`

## Objectif

Supprimer la dépendance des fonctionnalités des Lots 6 à 10 aux dictionnaires mémoire lorsque le serveur utilise PostgreSQL.

## Périmètre livré

- stockage générique `operational_snapshots` au format JSONB ;
- clé primaire composite : tenant, bénéficiaire, type de document et clé fonctionnelle ;
- persistance des réponses aux questionnaires ;
- persistance du dernier snapshot d'analyse structurée ;
- persistance de la synthèse éditable et de sa validation ;
- persistance du plan d'action et de l'avancement des actions ;
- implémentation mémoire conservée pour les tests et le développement sans PostgreSQL ;
- implémentation EF Core/PostgreSQL via `IDbContextFactory` ;
- migration `20260725170000_AddOperationalSnapshots` ;
- tests d'isolation tenant, bénéficiaire et clé documentaire.

## Configuration

### Développement sans PostgreSQL

```json
{
  "Persistence": {
    "Provider": "InMemory"
  }
}
```

### PostgreSQL

```text
Persistence__Provider=PostgreSql
ConnectionStrings__CapMethodSaas=Host=...;Database=...;Username=...;Password=...
Authentication__Jwt__SigningKey=<secret de 32 caractères minimum>
```

Aucun secret de production ne doit être stocké dans le dépôt.

## Migration contrôlée

Depuis `src/CapMethod.Saas` :

```bash
dotnet ef database update \
  --project CapMethod.Saas.Infrastructure \
  --startup-project CapMethod.Saas.Server
```

La migration n'est pas appliquée automatiquement au démarrage du serveur. Cette décision évite qu'une instance applicative modifie implicitement le schéma en production.

## Modèle de données

La table `operational_snapshots` contient :

- `tenant_id` ;
- `beneficiary_id` ;
- `document_type` ;
- `document_key` ;
- `payload_json` en JSONB ;
- `updated_at_utc`.

Les types actuellement utilisés sont :

```text
questionnaire
structured-analysis
synthesis
action-plan
```

## Garanties

- lecture et écriture toujours filtrées par tenant et bénéficiaire ;
- deux tenants peuvent utiliser le même identifiant bénéficiaire sans partager leurs données ;
- les questionnaires sont séparés par leur identifiant fonctionnel ;
- les contrats HTTP des Lots 6 à 10 restent inchangés ;
- le mode `InMemory` reste compatible avec la suite de tests existante.

## Limites assumées

- les snapshots sont versionnés par écrasement et non par historique complet ;
- chiffrement applicatif champ par champ non inclus ;
- stratégie de sauvegarde PostgreSQL dépend de l'hébergement ;
- audit détaillé des modifications traité au Lot 13.

## Validation attendue

- restore et build de `CapMethod.Saas.slnx` ;
- tests domaine, application, infrastructure, compatibilité, serveur et Aspire ;
- migration PostgreSQL applicable ;
- CI GitHub Actions verte.

## Prochaine étape

Lot 12 - Observabilité minimale.
