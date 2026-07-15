# Release Candidate Checklist - v3.0-saas

## Statut global

```text
RELEASE CANDIDATE - TO VERIFY
```

## 1. Socle technique

```text
[OK] .NET 10 configuré
[OK] C# LangVersion 14 configuré
[OK] Blazor WebAssembly hosted présent
[OK] API ASP.NET Core présente
[OK] Shared DTO présent
[OK] Domain/Application/Infrastructure séparés
[OK] Tests Domain/Application/Infrastructure/Compatibility présents
[OK] CI GitHub Actions présente
```

## 2. Persistance

```text
[OK] Mode mémoire local conservé
[OK] PostgreSQL EF Core supporté
[OK] Repositories EF Core ajoutés
[OK] Migrations EF Core présentes
[OK] Configuration InMemory / PostgreSql présente
[OK] Design-time factory EF présente
```

## 3. Sécurité

```text
[OK] JWT Bearer configuré
[OK] Endpoint dev token présent
[OK] Endpoints métier protégés
[OK] Contexte tenant résolu côté serveur
[OK] Contexte utilisateur résolu côté serveur
[OK] Fallback dev limité à l'environnement Development
[OK] Le client ne transmet plus le TenantId métier
```

## 4. Fonctionnel SaaS minimal

```text
[OK] Créer un bénéficiaire
[OK] Créer une session CAP pour un bénéficiaire existant
[OK] Lister les sessions CAP du tenant connecté
[OK] Lire le détail d'une session CAP
[OK] Empêcher la création de session avec bénéficiaire hors tenant
[OK] Préserver le mode CAP v1 sans IA obligatoire
[OK] Préserver le mode CAP v2 avec IA optionnelle
```

## 5. UI Blazor

```text
[OK] Connexion dev JWT
[OK] Affichage token courant
[OK] Affichage contexte serveur
[OK] Création bénéficiaire
[OK] Création session CAP
[OK] Liste sessions CAP
[OK] Détail session CAP
[OK] Composants de présentation extraits
[OK] App.razor réduit à l'orchestration
```

## 6. Documentation

```text
[OK] Statuts Lots 0 à 14 présents
[OK] Statut release candidate présent
[OK] Checklist release candidate présente
[OK] Notes release candidate présentes
[OK] Conditions de tag documentées
```

## 7. Validation avant tag

```text
[TODO] CI Lot 15 OK
[TODO] PR Lot 15 fusionnée dans main
[TODO] Tag v3.0-saas-rc1 créé
[TODO] Vérifier main après tag
```

## Commandes locales de validation recommandées

```bash
dotnet restore src/CapMethod.Saas/CapMethod.Saas.Server/CapMethod.Saas.Server.csproj
dotnet restore src/CapMethod.Saas/CapMethod.Saas.Client/CapMethod.Saas.Client.csproj
dotnet build src/CapMethod.Saas/CapMethod.Saas.Server/CapMethod.Saas.Server.csproj --no-restore
dotnet build src/CapMethod.Saas/CapMethod.Saas.Client/CapMethod.Saas.Client.csproj --no-restore
dotnet test src/CapMethod.Saas/CapMethod.Saas.Domain.Tests/CapMethod.Saas.Domain.Tests.csproj --no-restore
dotnet test src/CapMethod.Saas/CapMethod.Saas.Application.Tests/CapMethod.Saas.Application.Tests.csproj --no-restore
dotnet test src/CapMethod.Saas/CapMethod.Saas.Infrastructure.Tests/CapMethod.Saas.Infrastructure.Tests.csproj --no-restore
dotnet test src/CapMethod.Saas/CapMethod.Saas.Compatibility.Tests/CapMethod.Saas.Compatibility.Tests.csproj --no-restore
```

## Décision RC

La release candidate peut être taguée lorsque :

```text
CI main OK après fusion Lot 15
Aucun blocage fonctionnel identifié
Documentation RC présente sur main
Tag v3.0-saas-rc1 décidé explicitement
```
