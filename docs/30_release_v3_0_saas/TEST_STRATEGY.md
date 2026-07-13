# Stratégie de tests - CAP Method v3.0-saas

## Objectif

Définir les tests obligatoires pour `v3.0-saas` dès le démarrage du développement.

Les tests font partie du socle, au même titre que l'architecture.

## Principe

```text
Pas de développement SaaS sans tests.
Pas de merge sans CI verte.
Pas de fonctionnalité CAP sans test de compatibilité v1/v2.
```

## Stack de tests retenue

| Besoin | Outil | Licence | Décision |
|---|---|---|---|
| Tests unitaires .NET | xUnit | Apache-2.0 | Retenu |
| Assertions | FluentAssertions ou assertions natives xUnit | Apache-2.0 / natif | À confirmer avant ajout NuGet |
| Tests intégration PostgreSQL | Testcontainers for .NET | MIT | Retenu |
| Base de test | PostgreSQL container | PostgreSQL License | Retenu |
| Tests API | WebApplicationFactory ASP.NET Core | MIT | Retenu |
| Tests Blazor components | bUnit | MIT | Retenu sous vérification finale avant ajout NuGet |
| Couverture | coverlet | MIT | Retenu sous vérification finale avant ajout NuGet |

## Niveaux de tests

### 1. Tests unitaires Domain

Objectif : vérifier les règles métier sans base de données, sans API, sans Azure.

Cibles :

```text
Tenant
Beneficiary
CapSession
QuestionnaireProgress
AnalysisRecord
AiAnalysisRecord
ConsultantReview
DeliverablePackage
AuditLog
```

Tests obligatoires :

- création valide ;
- invariants métier ;
- transitions de statut ;
- impossibilité de livrer une synthèse non validée ;
- impossibilité de livrer un brouillon IA ;
- conservation du mode sans IA.

### 2. Tests unitaires Application

Objectif : vérifier les cas d'usage et ports applicatifs.

Cibles :

```text
ICapEnginePort
IAiAnalysisPort
IFileStoragePort
ITenantContext
IAuditLogPort
INotificationPort
IBackgroundJobPort
```

Tests obligatoires :

- orchestration sans IA ;
- orchestration avec IA optionnelle ;
- rejet si tenant incohérent ;
- audit des actions sensibles ;
- erreur explicite si le moteur CAP échoue ;
- absence de dépendance directe à Azure.

### 3. Tests d'intégration Infrastructure

Objectif : vérifier l'intégration réelle avec PostgreSQL et les adapters locaux.

Cibles :

```text
EF Core
PostgreSQL
Repositories
LocalFileStorageAdapter
LocalCapEngineAdapter
LocalAiAnalysisAdapter
```

Tests obligatoires :

- migration ou création schéma ;
- insertion / lecture tenant ;
- isolation tenant ;
- stockage local d'artefacts ;
- génération d'une référence d'artefact ;
- transaction sur action sensible.

### 4. Tests API Server

Objectif : vérifier les routes sécurisées et les réponses HTTP.

Tests obligatoires :

- route non authentifiée refusée ;
- consultant authentifié autorisé sur son tenant ;
- consultant interdit sur un autre tenant ;
- création bénéficiaire ;
- création session CAP ;
- déclenchement analyse sans IA ;
- déclenchement IA optionnelle ;
- remise livrable refusée si non validé.

### 5. Tests UI Client

Objectif : vérifier les composants et pages critiques.

Cibles :

```text
Dashboard consultant
Fiche bénéficiaire
Session CAP
Questionnaire web
ConsultantReview
Livrables
```

Tests obligatoires :

- affichage des statuts ;
- bouton IA visible uniquement si autorisé ;
- avertissement brouillon IA ;
- impossibilité d'afficher un brouillon IA comme livrable bénéficiaire ;
- affichage différencié consultant / bénéficiaire.

### 6. Tests de compatibilité v1.0-pro

Objectif : garantir que le SaaS peut utiliser CAP sans IA comme en v1.

Tests obligatoires :

```text
réponses web
  ↓
ResponseSession compatible
  ↓
AnalysisSnapshot compatible
  ↓
SynthesisDraft
  ↓
FinalSynthesis
  ↓
ActionPlan
  ↓
DOCX/PDF/ZIP
```

Critères :

- mêmes structures logiques que `v1.0-pro` ;
- mêmes artefacts attendus ;
- pas de dépendance IA ;
- pas de dépendance Azure.

### 7. Tests de compatibilité v2.0-ai

Objectif : garantir que le SaaS peut utiliser l'IA optionnelle comme en v2.

Tests obligatoires :

```text
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
AIAnalysisManifest
  ↓
ConsultantReview
```

Critères :

- `AIAnalysisDraft` généré si IA activée ;
- `AIAnalysisManifest` généré ;
- garde-fous contrôlés ;
- brouillon IA non livrable ;
- validation consultant obligatoire.

### 8. Tests Azure dev optionnel

Objectif : tester les adapters Azure sans rendre Azure obligatoire.

Règle :

```text
Les tests Azure ne doivent pas bloquer la CI standard.
Ils sont séparés dans une CI optionnelle ou un profil explicite.
```

Tests possibles :

- connexion stockage Azure dev ;
- lecture secret dev ;
- écriture artefact dev ;
- supervision minimale.

## Organisation recommandée des projets de tests

```text
src/CapMethod.Saas/
├── CapMethod.Saas.Domain.Tests
├── CapMethod.Saas.Application.Tests
├── CapMethod.Saas.Infrastructure.Tests
├── CapMethod.Saas.Server.Tests
├── CapMethod.Saas.Client.Tests
└── CapMethod.Saas.Compatibility.Tests
```

## CI obligatoire

La CI standard doit exécuter :

```text
dotnet restore
dotnet build --no-restore
dotnet test --no-build
```

Elle doit couvrir :

```text
Domain.Tests
Application.Tests
Infrastructure.Tests
Server.Tests
Compatibility.Tests
```

Les tests Azure dev restent séparés :

```text
AzureDev.Tests = optional / manual / protected environment
```

## Critères de merge

Aucun merge vers `main` sans :

```text
[x] build OK
[x] tests unitaires OK
[x] tests intégration OK
[x] tests compatibilité v1 OK
[x] tests compatibilité v2 OK
[x] aucune dépendance Azure obligatoire
[x] aucune bibliothèque commerciale obligatoire
```

## Tests prioritaires du Lot 1

Pour le squelette initial, les premiers tests à créer sont :

```text
1. Domain.Tests - CapSession peut être créée sans IA
2. Domain.Tests - CapSession peut activer l'IA optionnelle
3. Domain.Tests - brouillon IA non livrable au bénéficiaire
4. Application.Tests - CapEnginePort appelé en mode v1 sans IA
5. Application.Tests - AiAnalysisPort appelé uniquement si IA activée
6. Compatibility.Tests - contrat v1 ResponseSession présent
7. Compatibility.Tests - contrat v2 AIAnalysisDraft présent
```

## Décision

Les tests sont intégrés dès le squelette v3.

Le prochain développement doit créer la solution avec les projets de tests en même temps que les projets applicatifs.
