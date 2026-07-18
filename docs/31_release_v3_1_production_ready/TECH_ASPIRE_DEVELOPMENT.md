# Aspire en développement - v3.1

## Objectif

Ajouter un AppHost Aspire pour simplifier le lancement local du SaaS CAP Method pendant le développement.

Aspire est utilisé comme confort de développement uniquement. Il ne devient pas une dépendance de déploiement production.

## Périmètre livré

```text
AppHost Aspire = ajouté
Usage développement = oui
Obligation production = non
Persistence par défaut = InMemory
Server + Client orchestrés = oui
Packages centralisés SaaS = oui
AppHost Aspire = SDK NuGet Aspire.AppHost.Sdk
```

## Projet ajouté

```text
src/CapMethod.Saas/CapMethod.Saas.AppHost/CapMethod.Saas.AppHost.csproj
```

## Modèle Aspire 13+

L'AppHost utilise le modèle SDK NuGet Aspire 13+ :

```xml
<Project Sdk="Aspire.AppHost.Sdk/13.4.3">
```

Il n'utilise pas l'ancien workload Aspire ni `IsAspireHost`.

## Lancement local

Depuis la racine du dépôt :

```bash
dotnet run --project src/CapMethod.Saas/CapMethod.Saas.AppHost/CapMethod.Saas.AppHost.csproj
```

Ou depuis le dossier SaaS :

```bash
cd src/CapMethod.Saas
dotnet run --project CapMethod.Saas.AppHost/CapMethod.Saas.AppHost.csproj
```

## Services orchestrés

```text
capmethod-saas-server = API ASP.NET Core
capmethod-saas-client = Blazor WebAssembly client
```

Le client attend le serveur via `WaitFor(server)`.

## Décisions

```text
Aspire limité au développement = oui
AppHost inclus dans la solution .slnx = oui
Ancien workload Aspire = non
Modification métier = non
Migration EF = non
Déploiement production Aspire = non
```

## Prochaine étape

Reprendre le Lot 6 : questionnaires en ligne sur cette base de développement locale améliorée.