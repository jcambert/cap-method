# Statut Lot 5 - Persistance PostgreSQL / EF Core

## Branche

```text
feature/v3-lot5-postgresql-efcore
```

## Objectif

Introduire une persistance relationnelle PostgreSQL via EF Core pour les bénéficiaires et les sessions CAP, sans supprimer le mode local mémoire.

Le Lot 5 doit permettre :

- de rendre les agrégats persistables par EF Core ;
- d'ajouter un `DbContext` SaaS ;
- de mapper les bénéficiaires ;
- de mapper les sessions CAP ;
- d'ajouter des repositories EF Core ;
- d'ajouter une extension DI PostgreSQL ;
- de conserver le mode mémoire local par défaut ;
- de tester les repositories EF Core sans dépendre d'un PostgreSQL réel en CI.

## User stories du Lot 5

```text
US-SAAS-501 - Rendre les agrégats persistables
US-SAAS-502 - Ajouter le DbContext SaaS
US-SAAS-503 - Ajouter les repositories EF Core
US-SAAS-504 - Ajouter l'enregistrement DI PostgreSQL
US-SAAS-505 - Tester les repositories EF Core
```

## Règles

```text
Azure obligatoire = non
PostgreSQL obligatoire en CI = non
Mode mémoire local conservé = oui
Tests obligatoires = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
```

## Implémentation validée

### US-SAAS-501 - Rendre les agrégats persistables

Statut :

```text
DONE
```

Réalisé :

- `Beneficiary` compatible EF Core ;
- `CapSession` compatible EF Core ;
- invariants publics conservés via factories.

### US-SAAS-502 - Ajouter le DbContext SaaS

Statut :

```text
DONE
```

Réalisé :

- `CapMethodSaasDbContext` ajouté ;
- mapping `beneficiaries` ;
- mapping `cap_sessions` ;
- conversion `CapSessionStatus` vers string.

### US-SAAS-503 - Ajouter les repositories EF Core

Statut :

```text
DONE
```

Réalisé :

- `EfBeneficiaryRepository` ajouté ;
- `EfCapSessionRepository` ajouté ;
- isolation tenant conservée.

### US-SAAS-504 - Ajouter l'enregistrement DI PostgreSQL

Statut :

```text
DONE
```

Réalisé :

- extension `AddCapMethodSaasPostgreSqlInfrastructure` ajoutée ;
- repositories EF enregistrés en scoped ;
- mode mémoire existant conservé via `AddCapMethodSaasInfrastructure`.

### US-SAAS-505 - Tester les repositories EF Core

Statut :

```text
DONE
```

Réalisé :

- tests `EfBeneficiaryRepository` ;
- tests `EfCapSessionRepository` ;
- provider EF InMemory utilisé pour éviter une dépendance PostgreSQL en CI.

## Validation CI

```text
CI = OK
```

## Warning GitHub Actions observé

```text
Node.js 20 is deprecated
```

Ce warning est non bloquant. Les actions `actions/checkout@v4` et `actions/setup-dotnet@v4` sont forcées par GitHub sur Node.js 24. Le build et les tests restent validés.

## Statut global

```text
VALIDATED - CI OK
```

## Prochaine étape

Effectuer le squash merge de la PR Lot 5 vers `main`, puis supprimer la branche obsolète.
