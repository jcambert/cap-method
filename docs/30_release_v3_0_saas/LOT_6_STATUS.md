# Statut Lot 6 - Migrations EF Core et configuration PostgreSQL Server

## Branche

```text
feature/v3-lot6-ef-migrations-config
```

## Objectif

Finaliser l'activation opérationnelle de PostgreSQL côté application Server en ajoutant la configuration applicative, la factory design-time EF Core et la migration initiale.

Le Lot 6 doit permettre :

- de conserver le mode mémoire local par défaut ;
- d'activer PostgreSQL via configuration ;
- de fournir une connection string applicative standard ;
- de fournir une factory EF Core design-time ;
- de versionner la migration initiale ;
- de permettre l'exécution future de `dotnet ef database update` ;
- de garder la CI sans dépendance PostgreSQL réelle.

## User stories du Lot 6

```text
US-SAAS-601 - Configurer le provider de persistance côté Server
US-SAAS-602 - Ajouter la configuration PostgreSQL applicative
US-SAAS-603 - Ajouter la factory EF Core design-time
US-SAAS-604 - Ajouter la migration initiale EF Core
US-SAAS-605 - Documenter l'activation PostgreSQL
```

## Règles

```text
Azure obligatoire = non
PostgreSQL obligatoire en CI = non
Mode mémoire local par défaut = oui
Migration EF Core versionnée = oui
Tests existants obligatoires = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation validée

### US-SAAS-601 - Configurer le provider de persistance côté Server

Statut :

```text
DONE
```

Réalisé :

- `Program.cs` lit `Persistence:Provider` ;
- `InMemory` reste le provider par défaut ;
- `PostgreSql` active `AddCapMethodSaasPostgreSqlInfrastructure` ;
- une erreur explicite est levée si le provider est inconnu.

### US-SAAS-602 - Ajouter la configuration PostgreSQL applicative

Statut :

```text
DONE
```

Réalisé :

- `appsettings.json` ajouté ;
- `appsettings.Development.json` ajouté ;
- connection string `ConnectionStrings:CapMethodSaas` ajoutée ;
- provider mémoire conservé en développement.

### US-SAAS-603 - Ajouter la factory EF Core design-time

Statut :

```text
DONE
```

Réalisé :

- `CapMethodSaasDbContextFactory` ajoutée ;
- variable d'environnement `CAP_METHOD_SAAS_CONNECTION_STRING` supportée ;
- connection string locale fallback documentée dans le code.

### US-SAAS-604 - Ajouter la migration initiale EF Core

Statut :

```text
DONE
```

Réalisé :

- migration `InitialSaasPersistence` ajoutée ;
- tables `beneficiaries` et `cap_sessions` créées ;
- indexes tenant ajoutés ;
- snapshot EF Core ajouté.

### US-SAAS-605 - Documenter l'activation PostgreSQL

Statut :

```text
DONE
```

Réalisé :

- statut Lot 6 créé ;
- mode d'activation prévu : `Persistence:Provider = PostgreSql` ;
- connection string prévue : `ConnectionStrings:CapMethodSaas`.

## Commandes utiles

Activer PostgreSQL localement via variable d'environnement :

```bash
export CAP_METHOD_SAAS_CONNECTION_STRING="Host=localhost;Port=5432;Database=cap_method_saas;Username=cap_method;Password=cap_method"
```

Appliquer la migration depuis la racine du dépôt :

```bash
dotnet ef database update \
  --project src/CapMethod.Saas/CapMethod.Saas.Infrastructure/CapMethod.Saas.Infrastructure.csproj \
  --startup-project src/CapMethod.Saas/CapMethod.Saas.Server/CapMethod.Saas.Server.csproj
```

## Validation CI

```text
CI = OK
```

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Effectuer le squash merge de la PR Lot 6 vers `main`, puis supprimer la branche obsolète.
