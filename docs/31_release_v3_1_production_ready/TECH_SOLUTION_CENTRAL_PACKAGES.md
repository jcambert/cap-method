# Solution .slnx et gestion centralisée des packages

## Objectif

Stabiliser la structure technique du projet SaaS avant de poursuivre les lots fonctionnels v3.1.

Ce lot technique introduit :

```text
Solution moderne .slnx
Central Package Management NuGet
CI basée sur la solution
```

## Solution

Fichier ajouté :

```text
src/CapMethod.Saas/CapMethod.Saas.slnx
```

La solution référence les projets SaaS suivants :

```text
CapMethod.Saas.Domain
CapMethod.Saas.Application
CapMethod.Saas.Infrastructure
CapMethod.Saas.Shared
CapMethod.Saas.Server
CapMethod.Saas.Client
CapMethod.Saas.Domain.Tests
CapMethod.Saas.Application.Tests
CapMethod.Saas.Infrastructure.Tests
CapMethod.Saas.Compatibility.Tests
```

## Central Package Management

Fichier ajouté :

```text
src/CapMethod.Saas/Directory.Packages.props
```

Les versions NuGet sont désormais déclarées dans ce fichier via `PackageVersion`.

Les fichiers `.csproj` gardent uniquement les références nécessaires via `PackageReference`, sans attribut `Version`.

## Packages centralisés

```text
Microsoft.AspNetCore.Authentication.JwtBearer = 10.0.0
Microsoft.AspNetCore.Components.WebAssembly = 10.0.0
Microsoft.AspNetCore.Components.WebAssembly.DevServer = 10.0.0
Microsoft.EntityFrameworkCore = 10.0.0
Microsoft.EntityFrameworkCore.Design = 10.0.0
Microsoft.EntityFrameworkCore.InMemory = 10.0.0
Microsoft.NET.Test.Sdk = 17.14.1
Npgsql.EntityFrameworkCore.PostgreSQL = 10.0.0
xunit = 2.9.3
xunit.runner.visualstudio = 3.1.3
```

## CI

La CI v3 SaaS restaure et build désormais la solution :

```bash
dotnet restore src/CapMethod.Saas/CapMethod.Saas.slnx
dotnet build src/CapMethod.Saas/CapMethod.Saas.slnx --no-restore
```

Les tests restent exécutés par projet pour conserver une lecture claire des échecs.

## Décisions

```text
Format solution = .slnx
Fichier packages centralisés = Directory.Packages.props
Versions dans csproj = non
LangVersion = 14.0 conservée
TargetFramework = net10.0 conservé
Refonte métier = non
Migration EF = non
```
