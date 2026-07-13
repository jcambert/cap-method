# Statut Lot 0 - CAP Method v3.0-saas

## Branche

```text
feature/v3-saas
```

## Statut global

```text
IN_PROGRESS
```

## Objectif du Lot 0

Verrouiller le cadrage technique avant de coder le socle SaaS.

Le Lot 0 doit garantir :

- une stack open source utilisable professionnellement sans licence applicative payante obligatoire ;
- une architecture Blazor WebAssembly hosted ;
- une portabilité locale sans dépendance forte à Azure ;
- un usage Azure limité au développement / expérimentation ;
- la compatibilité avec `v1.0-pro` sans IA ;
- la compatibilité avec `v2.0-ai` avec IA optionnelle ;
- l'encapsulation du moteur CAP existant via adaptateurs ;
- des tests intégrés dès le squelette ;
- une CI dédiée au socle v3.

## User stories du Lot 0

```text
US-SAAS-000 - Valider la stack open source professionnelle
US-SAAS-032 - Conserver le mode CAP v1 sans IA
US-SAAS-033 - Conserver le mode CAP v2 avec IA optionnelle
US-SAAS-034 - Créer un adaptateur moteur CAP
```

## Décisions déjà validées

### Stack applicative

```text
Blazor WebAssembly hosted
ASP.NET Core
PostgreSQL
EF Core
MudBlazor
```

### Azure

```text
Azure = développement / expérimentation uniquement
```

Azure ne doit pas devenir une dépendance de production obligatoire.

### Compatibilité CAP

```text
CAP v1 sans IA = obligatoire
CAP v2 avec IA optionnelle = obligatoire
```

## Réalisations actuelles

### US-SAAS-000

Statut :

```text
DONE
```

Justification :

```text
docs/30_release_v3_0_saas/TECH_STACK.md
```

La stack est documentée, les licences acceptées sont listées et les modèles à éviter sont explicités.

### US-SAAS-032

Statut :

```text
IN_PROGRESS
```

Réalisations :

- projet `CapMethod.Saas.Domain` créé ;
- modèle `CapSession` créé ;
- modèle `DeliverablePackage` créé ;
- tests domaine ajoutés ;
- contrats `ResponseSession` et `AnalysisSnapshot` protégés par tests de compatibilité.

### US-SAAS-033

Statut :

```text
IN_PROGRESS
```

Réalisations :

- port `IAiAnalysisPort` créé ;
- adapter local IA créé ;
- tests de blocage si IA désactivée ajoutés ;
- tests de contrat `AIAnalysisDraft` et `AIAnalysisManifest` ajoutés ;
- règle de non-livraison d'un brouillon IA testée.

### US-SAAS-034

Statut :

```text
IN_PROGRESS
```

Réalisations :

- port `ICapEnginePort` créé ;
- `LocalCapEngineAdapter` créé ;
- `LocalAiAnalysisAdapter` créé ;
- tests d'adapter local ajoutés ;
- mismatch tenant contrôlé ;
- Azure reste absent du coeur métier.

## Socle applicatif créé

```text
src/CapMethod.Saas/
├── CapMethod.Saas.Client
├── CapMethod.Saas.Server
├── CapMethod.Saas.Shared
├── CapMethod.Saas.Application
├── CapMethod.Saas.Domain
├── CapMethod.Saas.Infrastructure
├── CapMethod.Saas.Domain.Tests
├── CapMethod.Saas.Infrastructure.Tests
└── CapMethod.Saas.Compatibility.Tests
```

## CI ajoutée

```text
.github/workflows/v3-saas-validation.yml
```

La CI exécute :

```text
dotnet restore
dotnet build
dotnet test
```

Sur :

```text
Server
Client
Domain.Tests
Infrastructure.Tests
Compatibility.Tests
```

## Prochaine étape recommandée

Vérifier la CI GitHub sur `feature/v3-saas`, corriger si nécessaire, puis ajouter les premiers cas d'usage Application autour de la création d'une session CAP.
