# Décision technique - CAP Method v3.0-saas

## Décision

```text
Blazor WebAssembly hosted
Azure
```

## Statut

```text
VALIDATED
```

## Architecture retenue

```text
CAP Method SaaS
  ├── Client  : Blazor WebAssembly + MudBlazor
  ├── Server  : ASP.NET Core API / traitements CAP
  ├── Shared  : DTO / contrats / validations partagées
  ├── Database: PostgreSQL
  └── Hosting : Azure
```

## Pourquoi Blazor WebAssembly hosted

Ce choix permet :

- une interface riche côté navigateur ;
- un backend ASP.NET Core sécurisé ;
- une séparation claire `Client / Server / Shared` ;
- l'exécution serveur des traitements lourds CAP ;
- l'exécution serveur des exports DOCX/PDF/ZIP ;
- l'encapsulation des chaînes `v1.0-pro` et `v2.0-ai` ;
- une cible Azure cohérente.

## Pourquoi Azure

Azure est retenu comme cible d'hébergement professionnelle.

La plateforme pourra porter :

```text
Azure App Service
Azure Database for PostgreSQL
Azure Blob Storage
Azure Key Vault
Azure Monitor / Application Insights
GitHub Actions
```

## Point important sur les licences

Le choix Azure ne change pas la règle sur les bibliothèques applicatives :

```text
Open source uniquement
Pas de licence applicative payante obligatoire
Pas de composant commercial obligatoire
```

Azure peut générer des coûts d'infrastructure, mais ne doit pas imposer une dépendance applicative propriétaire dans le coeur CAP.

## Compatibilité obligatoire

Le SaaS doit continuer à permettre :

```text
CAP v1 sans IA
CAP v2 avec IA optionnelle
```

### Mode v1 conservé

```text
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
```

### Mode v2 conservé

```text
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
AIAnalysisManifest
  ↓
ConsultantReview
```

## Règle d'architecture

Le projet SaaS ne doit pas appeler directement Azure depuis le coeur métier.

Les dépendances Azure doivent être isolées derrière des ports :

```text
Application CAP
  ↓
Ports applicatifs
  ↓
Adapters Azure
```

Ports attendus :

- stockage de fichiers ;
- secrets ;
- logs ;
- notifications ;
- jobs ;
- configuration ;
- export / téléchargement.

## Impact sur le backlog

Les premiers lots doivent donc couvrir :

```text
Lot 0 - Cadrage technique et compatibilité
Lot 1 - SaaS foundation minimal WASM hosted
Lot 2 - Déploiement Azure minimal
Lot 3 - Parcours bénéficiaire et questionnaires
Lot 4 - Moteur CAP intégré sans IA obligatoire
Lot 5 - IA optionnelle dans le SaaS
```

## Décision finale

```text
v3.0-saas part sur Blazor WebAssembly hosted + Azure.
La stack applicative reste open source.
CAP v1 et CAP v2 restent utilisables.
```
