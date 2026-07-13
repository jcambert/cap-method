# Architecture applicative - CAP Method v3.0-saas

## Objectif

Définir l'architecture de départ du SaaS CAP Method avant la création du code applicatif.

La cible validée est :

```text
Blazor WebAssembly hosted
Azure limité au développement / expérimentation
Architecture portable sans dépendance forte à Azure
```

## Règles structurantes

```text
Le SaaS orchestre le parcours.
Le moteur CAP produit les livrables.
L'IA assiste si elle est activée.
Le consultant reste responsable de la validation.
```

`v3.0-saas` ne doit pas réécrire `v1.0-pro` ou `v2.0-ai`.

Il doit les encapsuler.

## Structure cible

```text
cap-method/
└── src/
    └── CapMethod.Saas/
        ├── CapMethod.Saas.Client/
        ├── CapMethod.Saas.Server/
        ├── CapMethod.Saas.Shared/
        ├── CapMethod.Saas.Application/
        ├── CapMethod.Saas.Domain/
        ├── CapMethod.Saas.Infrastructure/
        └── CapMethod.Saas.Tests/
```

## Projets

### CapMethod.Saas.Client

Type :

```text
Blazor WebAssembly
```

Responsabilités :

- pages consultant ;
- pages bénéficiaire ;
- composants UI MudBlazor ;
- appels API typés ;
- gestion du shell applicatif ;
- affichage des statuts de session ;
- affichage des livrables autorisés.

Interdictions :

- aucun accès direct à la base de données ;
- aucun traitement CAP lourd ;
- aucune génération DOCX/PDF/ZIP ;
- aucune décision métier sensible uniquement côté client ;
- aucune dépendance directe à Azure.

### CapMethod.Saas.Server

Type :

```text
ASP.NET Core hosted backend
```

Responsabilités :

- API sécurisée ;
- authentification ;
- autorisation ;
- multi-tenant ;
- déclenchement des traitements CAP ;
- génération des exports ;
- jobs applicatifs ;
- audit ;
- orchestration des ports applicatifs.

Interdictions :

- ne pas mélanger logique métier et infrastructure ;
- ne pas appeler directement Azure depuis les cas d'usage ;
- ne pas livrer automatiquement un brouillon IA au bénéficiaire.

### CapMethod.Saas.Shared

Responsabilités :

- DTO ;
- contrats de transport ;
- modèles de requête/réponse API ;
- validations simples partageables ;
- constantes de transport non sensibles.

Règle :

```text
Shared ne contient pas de logique métier critique.
```

### CapMethod.Saas.Application

Responsabilités :

- cas d'usage ;
- orchestration métier ;
- ports applicatifs ;
- contrats du moteur CAP ;
- contrats d'export ;
- contrats IA optionnels ;
- règles de validation applicative ;
- transactions applicatives.

Règle :

```text
Application dépend du domaine, pas de l'infrastructure.
```

### CapMethod.Saas.Domain

Responsabilités :

- entités métier SaaS ;
- value objects ;
- règles de domaine ;
- statuts métier ;
- invariants ;
- événements de domaine si nécessaire.

Entités candidates :

```text
Tenant
UserAccount
ConsultantProfile
Beneficiary
CapSession
QuestionnaireProgress
ResponseDraft
AnalysisRecord
AiAnalysisRecord
ConsultantReview
DeliverablePackage
AuditLog
```

### CapMethod.Saas.Infrastructure

Responsabilités :

- EF Core ;
- PostgreSQL ;
- stockage fichiers local ;
- adapters Azure dev optionnels ;
- logs ;
- jobs ;
- implémentation des ports ;
- intégration du moteur CAP existant.

Règle :

```text
Infrastructure dépend d'Application.
Application ne dépend pas d'Infrastructure.
```

### CapMethod.Saas.Tests

Responsabilités :

- tests unitaires ;
- tests d'intégration ;
- tests multi-tenant ;
- tests de compatibilité `v1.0-pro` ;
- tests de compatibilité `v2.0-ai` ;
- tests des adaptateurs locaux.

## Flux principal sans IA

```text
Consultant crée une session CAP
  ↓
Bénéficiaire complète les questionnaires
  ↓
SaaS génère ResponseSession
  ↓
CapEngineAdapter génère AnalysisSnapshot
  ↓
CapEngineAdapter génère SynthesisDraft
  ↓
Consultant édite FinalSynthesis
  ↓
Consultant valide FinalSynthesis
  ↓
CapEngineAdapter génère ActionPlan
  ↓
ExportAdapter génère DOCX/PDF/ZIP
  ↓
Consultant remet les livrables validés
```

## Flux principal avec IA optionnelle

```text
AnalysisSnapshot disponible
  ↓
Consultant active l'IA
  ↓
AiAnalysisAdapter génère AIAnalysisDraft
  ↓
AiAnalysisAdapter génère AIAnalysisManifest
  ↓
ConsultantReview obligatoire
  ↓
Consultant retient, corrige ou rejette les hypothèses
  ↓
FinalSynthesis validée humainement
  ↓
ActionPlan et exports
```

## Règle de livraison bénéficiaire

```text
Un bénéficiaire ne peut recevoir que des livrables validés.
Un brouillon IA n'est jamais livré automatiquement.
Une synthèse non validée ne peut pas être remise comme synthèse finale.
```

## Ports applicatifs minimaux

```text
ICapEnginePort
IAiAnalysisPort
IExportPort
IFileStoragePort
IClock
ICurrentUser
ITenantContext
IAuditLogPort
INotificationPort
IBackgroundJobPort
```

## Adapters d'infrastructure minimaux

```text
LocalCapEngineAdapter
LocalAiAnalysisAdapter
LocalFileStorageAdapter
PostgreSqlRepositoryAdapter
LocalNotificationAdapter
LocalBackgroundJobAdapter
AzureDevFileStorageAdapter
AzureDevSecretAdapter
```

Azure reste optionnel et limité au développement.

## Multi-tenant

Toutes les entités persistées liées au parcours doivent porter un `TenantId`.

```text
TenantId obligatoire sur :
- Beneficiary
- CapSession
- ResponseDraft
- AnalysisRecord
- AiAnalysisRecord
- ConsultantReview
- DeliverablePackage
- AuditLog
```

Les tests doivent prouver qu'un tenant ne peut pas lire les données d'un autre.

## Sécurité

Principes :

- API sécurisée par défaut ;
- routes sensibles authentifiées ;
- autorisation par rôle et tenant ;
- audit des actions sensibles ;
- pas de secret côté Client WASM ;
- pas de contenu sensible inutile dans les notifications ;
- pas de stockage non contrôlé des brouillons IA.

## Décision

Cette architecture devient la base de développement de `feature/v3-saas`.

La prochaine étape technique est de créer le contrat du `CapEngineAdapter` puis le squelette de solution.
