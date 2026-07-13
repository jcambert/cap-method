# Stack technique - CAP Method v3.0-saas

## Objectif

Définir les bibliothèques utilisables pour `v3.0-saas` avant le démarrage du développement.

La stack doit respecter trois règles :

```text
1. Open source uniquement.
2. Licence compatible avec un usage professionnel sans paiement de licence obligatoire.
3. Compatibilité conservée avec v1.0-pro et v2.0-ai.
```

## Règle de compatibilité CAP

`v3.0-saas` ne remplace pas le moteur existant.

Le SaaS doit orchestrer et encapsuler les chaînes déjà validées :

```text
v1.0-pro
  ResponseSession
    ↓
  AnalysisSnapshot
    ↓
  SynthesisDraft
    ↓
  FinalSynthesis
    ↓
  ActionPlan
    ↓
  DOCX/PDF/ZIP

v2.0-ai
  AnalysisSnapshot
    ↓
  AIAnalysisDraft
    ↓
  AIAnalysisManifest
    ↓
  ConsultantReview
```

Le SaaS doit donc pouvoir :

- utiliser CAP sans IA ;
- utiliser CAP avec IA optionnelle ;
- produire les mêmes structures métier ;
- conserver les exports existants ;
- ne jamais livrer automatiquement un brouillon IA au bénéficiaire.

## Licences acceptées

Les licences suivantes sont acceptées pour le projet :

```text
MIT
Apache-2.0
BSD-2-Clause
BSD-3-Clause
PostgreSQL License
```

Ces licences sont permissives et compatibles avec un usage professionnel, sous réserve de conserver les mentions de copyright et notices applicables.

## Licences ou modèles à éviter

```text
GPL / AGPL pour les dépendances runtime principales
LGPL si le mode d'intégration impose une contrainte de redistribution non souhaitée
SSPL
Commons Clause
licence duale nécessitant un achat pour usage commercial
bibliothèque gratuite seulement pour usage personnel ou non commercial
composant UI soumis à licence commerciale
```

## Stack retenue

| Besoin | Bibliothèque / technologie | Licence | Décision |
|---|---|---|---|
| Runtime backend | .NET / ASP.NET Core | MIT | Retenu |
| Frontend web | Blazor | MIT via ASP.NET Core | Retenu |
| UI components | MudBlazor | MIT | Retenu |
| Base de données | PostgreSQL | PostgreSQL License | Retenu |
| ORM | Entity Framework Core | MIT | Retenu |
| Provider PostgreSQL | Npgsql | Licence permissive Npgsql | Retenu |
| Authentification | ASP.NET Core Identity | MIT | Retenu |
| Autorisation | ASP.NET Core Authorization Policies | MIT | Retenu |
| Validation | FluentValidation | Apache-2.0 | Retenu sous vérification finale avant ajout NuGet |
| Mapping | Mapster | MIT | Retenu |
| Logs | Serilog | Apache-2.0 | Retenu |
| Résilience | Polly | BSD-3-Clause | Retenu |
| Jobs planifiés | Quartz.NET | Apache-2.0 | Retenu sous vérification finale avant ajout NuGet |
| Observabilité | OpenTelemetry .NET | Apache-2.0 | Retenu sous vérification finale avant ajout NuGet |
| Tests unitaires | xUnit | Apache-2.0 | Retenu sous vérification finale avant ajout NuGet |
| Test containers | Testcontainers for .NET | MIT | Retenu sous vérification finale avant ajout NuGet |

## Stack non retenue à ce stade

| Besoin | Bibliothèque / technologie | Raison |
|---|---|---|
| Dashboard commercial | composants UI commerciaux | Licence payante ou contrainte commerciale possible |
| Jobs background | Hangfire | Modèle avec éditions commerciales, à éviter pour le socle libre |
| Paiement | Stripe SDK | Hors périmètre MVP v3 initial |
| Email provider propriétaire | SDK fournisseur email spécifique | À éviter au profit d'un port SMTP / HTTP abstrait |
| IA provider obligatoire | SDK propriétaire obligatoire | Non compatible avec l'exigence d'IA optionnelle |

## Architecture cible

```text
CAP SaaS Web App
  ├── Blazor UI
  ├── ASP.NET Core API / Server
  ├── Application CAP
  ├── CAP Engine Adapter
  ├── AI Adapter optionnel
  ├── Export Adapter
  ├── PostgreSQL
  └── Background Jobs
```

## Principes d'intégration

### 1. CAP Engine Adapter

Le SaaS doit appeler le moteur CAP via un adaptateur.

```text
SaaS data
  ↓
CAP Engine Adapter
  ↓
ResponseSession / AnalysisSnapshot / FinalSynthesis / ActionPlan
```

Objectif : ne pas casser les formats validés en `v1.0-pro`.

### 2. AI Adapter optionnel

L'IA doit rester désactivable.

```text
AI enabled = false
  => chaîne v1.0-pro uniquement

AI enabled = true
  => chaîne v2.0-ai avec AIAnalysisDraft + ConsultantReview
```

### 3. Export Adapter

Les exports existants restent la référence.

```text
FinalSynthesis + ActionPlan
  ↓
DOCX / PDF / ZIP
```

### 4. Multi-tenant obligatoire

Toutes les entités SaaS métier doivent porter un `TenantId`.

```text
Tenant
  ├── Consultants
  ├── Beneficiaries
  ├── CapSessions
  ├── Responses
  ├── Analyses
  ├── Deliverables
  └── AuditLogs
```

## Règles de sélection des dépendances

Avant d'ajouter une dépendance NuGet ou npm :

```text
[x] vérifier la licence dans le dépôt officiel
[x] vérifier l'absence de licence commerciale obligatoire
[x] vérifier la compatibilité avec un usage professionnel
[x] documenter la dépendance dans ce fichier
[x] éviter les dépendances non nécessaires au MVP
```

## Décision

La stack `v3.0-saas` est orientée :

```text
.NET / Blazor / PostgreSQL / EF Core / MudBlazor
```

Le développement doit démarrer par un socle SaaS minimal qui encapsule `v1.0-pro` et `v2.0-ai`, sans réécrire le moteur CAP.
