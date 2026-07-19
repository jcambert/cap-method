# CAP Method

CAP Method est une méthode d'accompagnement personnel et professionnel complétée par un moteur de questionnaires, d'analyse et de livrables, ainsi que par une application SaaS en cours de construction.

## État des versions

| Version | Statut |
|---|---|
| `v1.0-pro` | ✅ Publiée et exploitable professionnellement |
| `v2.0-ai` | ✅ Intégrée dans `main`, assistance IA optionnelle |
| `v3.0-saas` | ✅ Socle SaaS stable |
| `v3.1-saas-production-ready` | 🚧 En cours — Lots 0 à 7 validés |

## v3.1-saas-production-ready

Fonctionnalités déjà intégrées :

- navigation consultant ;
- authentification consultant hors token de développement ;
- workflow CAP ;
- espace bénéficiaire sécurisé ;
- questionnaires en ligne ;
- brouillons, soumission et progression ;
- analyse structurée déterministe ;
- isolation tenant et bénéficiaire ;
- tests et CI ;
- solution `.slnx`, packages NuGet centralisés et Aspire pour le développement.

La version n'est pas encore prête pour la production. Les réponses questionnaires sont encore stockées en mémoire et les Lots 8 à 13 restent à terminer.

Documentation de référence :

```text
docs/31_release_v3_1_production_ready/README.md
docs/31_release_v3_1_production_ready/DOCUMENTATION_INDEX.md
docs/31_release_v3_1_production_ready/BACKLOG.md
docs/31_release_v3_1_production_ready/PRODUCTION_READINESS.md
```

## Base technique SaaS

```text
.NET 10
C# 14
Blazor WebAssembly hosted
ASP.NET Core
PostgreSQL / InMemory selon environnement
Solution .slnx
Central Package Management
Aspire AppHost en développement uniquement
```

Solution :

```text
src/CapMethod.Saas/CapMethod.Saas.slnx
```

Lancement local avec Aspire :

```bash
dotnet run --project src/CapMethod.Saas/CapMethod.Saas.AppHost/CapMethod.Saas.AppHost.csproj
```

Build complet :

```bash
dotnet restore src/CapMethod.Saas/CapMethod.Saas.slnx
dotnet build src/CapMethod.Saas/CapMethod.Saas.slnx --no-restore
```

## Chaîne historique CAP

Le dossier `questionnaire-engine/` conserve la chaîne complète historique :

```text
Questionnaires
  ↓
Import des réponses
  ↓
ResponseSession
  ↓
AnalysisSnapshot
  ↓
Analyse IA optionnelle
  ↓
Validation consultant
  ↓
Synthèse finale
  ↓
Plan d'action
  ↓
DOCX / PDF / ZIP
```

Les fichiers Markdown restent la source éditable. Les exports sont des artefacts de distribution. La validation humaine reste obligatoire.

## Organisation principale

```text
docs/                           documentation méthode et releases
questionnaire-engine/           moteur historique de questionnaires et livrables
src/CapMethod.Saas/             application SaaS .NET
.github/workflows/              CI GitHub Actions
templates/                      modèles Google Forms, Sheets et messages
```

## Roadmap

```text
docs/ROADMAP.md
```

Prochaine étape produit :

```text
Lot 8 - Synthèse éditable
```