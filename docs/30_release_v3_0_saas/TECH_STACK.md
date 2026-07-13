# Stack technique - CAP Method v3.0-saas

## Objectif

Définir les bibliothèques utilisables pour `v3.0-saas` avant le démarrage du développement.

La stack doit respecter trois règles :

```text
1. Open source uniquement.
2. Licence compatible avec un usage professionnel sans paiement de licence obligatoire.
3. Compatibilité conservée avec v1.0-pro et v2.0-ai.
```

## Décision d'architecture applicative

```text
Blazor WebAssembly hosted
```

Structure cible :

```text
CAP Method SaaS
  ├── Client  : Blazor WebAssembly + MudBlazor
  ├── Server  : ASP.NET Core API / hosted backend
  └── Shared  : contrats DTO, validation partagée, modèles de transport
```

Le choix `WASM hosted` permet :

- une interface riche côté navigateur ;
- un backend ASP.NET Core pour la sécurité, les traitements CAP et les exports ;
- une séparation claire entre UI, API et moteur CAP ;
- un hébergement Azure simple ;
- la compatibilité avec les traitements serveur nécessaires aux exports DOCX/PDF/ZIP.

## Cible d'hébergement

```text
Azure
```

Azure est retenu comme cible d'hébergement et d'exploitation.

Important : Azure est une plateforme cloud commerciale. Le choix Azure n'ajoute pas de bibliothèque applicative payante obligatoire, mais l'infrastructure Azure peut générer des coûts d'hébergement, base de données, stockage, supervision ou trafic.

## Cible Azure recommandée

| Besoin | Service Azure cible | Décision |
|---|---|---|
| Hébergement backend/API | Azure App Service | Retenu |
| Hébergement Blazor WASM | Azure App Service ou Static Web Apps selon packaging final | Retenu |
| Base PostgreSQL | Azure Database for PostgreSQL | Retenu |
| Stockage fichiers exports | Azure Blob Storage | Retenu |
| Secrets | Azure Key Vault | Retenu |
| Logs / supervision | Application Insights / Azure Monitor | Retenu |
| CI/CD | GitHub Actions vers Azure | Retenu |

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
| Frontend web | Blazor WebAssembly hosted | MIT via ASP.NET Core | Retenu |
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
CAP SaaS WebAssembly hosted
  ├── Client Blazor WASM
  │   ├── UI MudBlazor
  │   ├── pages consultant
  │   ├── pages bénéficiaire
  │   └── appels API typés
  ├── Server ASP.NET Core
  │   ├── API sécurisée
  │   ├── authentification / autorisation
  │   ├── Application CAP
  │   ├── CAP Engine Adapter
  │   ├── AI Adapter optionnel
  │   ├── Export Adapter
  │   ├── Background Jobs
  │   └── intégration Azure
  ├── Shared
  │   ├── DTO
  │   ├── contrats de transport
  │   └── validations partagées
  └── PostgreSQL
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

### 5. Déploiement Azure découplé de la logique métier

La logique CAP ne doit pas dépendre directement d'Azure.

```text
Application CAP
  ↓
Ports abstraits
  ↓
Adapters Azure / Local / Tests
```

Les services Azure doivent être appelés via des ports applicatifs :

- stockage de fichiers ;
- secrets ;
- notifications ;
- logs ;
- jobs ;
- configuration.

## Règles de sélection des dépendances

Avant d'ajouter une dépendance NuGet ou npm :

```text
[x] vérifier la licence dans le dépôt officiel
[x] vérifier l'absence de licence commerciale obligatoire
[x] vérifier la compatibilité avec un usage professionnel
[x] documenter la dépendance dans ce fichier
[x] éviter les dépendances non nécessaires au MVP
[x] distinguer coût d'hébergement Azure et licence applicative
```

## Décision

La stack `v3.0-saas` est orientée :

```text
Blazor WebAssembly hosted / ASP.NET Core / PostgreSQL / EF Core / MudBlazor / Azure
```

Le développement doit démarrer par un socle SaaS minimal qui encapsule `v1.0-pro` et `v2.0-ai`, sans réécrire le moteur CAP.
