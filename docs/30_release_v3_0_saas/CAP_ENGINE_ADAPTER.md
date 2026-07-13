# Contrat CapEngineAdapter - CAP Method v3.0-saas

## Objectif

Définir le contrat d'intégration entre le SaaS `v3.0-saas` et le moteur CAP existant.

Le SaaS ne doit pas réécrire le moteur CAP.

Il doit l'appeler via un port applicatif stable.

## Principe

```text
SaaS
  ↓
ICapEnginePort
  ↓
CapEngineAdapter
  ↓
Chaîne CAP existante v1.0-pro / v2.0-ai
```

## Règles obligatoires

```text
CAP v1 sans IA doit rester utilisable.
CAP v2 avec IA optionnelle doit rester utilisable.
Les formats existants doivent rester compatibles.
Les exports existants restent la référence.
Le brouillon IA ne doit jamais être livré automatiquement.
```

## Port applicatif cible

```csharp
public interface ICapEnginePort
{
    Task<CapEngineResult<ResponseSessionRef>> BuildResponseSessionAsync(
        BuildResponseSessionCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<AnalysisSnapshotRef>> GenerateAnalysisSnapshotAsync(
        GenerateAnalysisSnapshotCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<SynthesisDraftRef>> GenerateSynthesisDraftAsync(
        GenerateSynthesisDraftCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<FinalSynthesisRef>> GenerateFinalSynthesisAsync(
        GenerateFinalSynthesisCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<ActionPlanRef>> GenerateActionPlanAsync(
        GenerateActionPlanCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<DeliverablePackageRef>> GenerateDeliverablePackageAsync(
        GenerateDeliverablePackageCommand command,
        CancellationToken cancellationToken);
}
```

## Contrat IA optionnelle

L'IA doit rester séparée du moteur CAP principal.

```csharp
public interface IAiAnalysisPort
{
    Task<CapEngineResult<AiAnalysisDraftRef>> GenerateAiAnalysisDraftAsync(
        GenerateAiAnalysisDraftCommand command,
        CancellationToken cancellationToken);

    Task<CapEngineResult<AiAnalysisManifestRef>> GenerateAiAnalysisManifestAsync(
        GenerateAiAnalysisManifestCommand command,
        CancellationToken cancellationToken);
}
```

## Objets de commande

### BuildResponseSessionCommand

```text
TenantId
CapSessionId
BeneficiaryId
ConsultantId
QuestionnaireResponses
SourceMode
```

Objectif : transformer les réponses web en structure compatible `ResponseSession`.

### GenerateAnalysisSnapshotCommand

```text
TenantId
CapSessionId
ResponseSessionRef
```

Objectif : produire `AnalysisSnapshot` en conservant le format `v1.0-pro`.

### GenerateSynthesisDraftCommand

```text
TenantId
CapSessionId
AnalysisSnapshotRef
```

Objectif : produire le brouillon de synthèse classique sans IA obligatoire.

### GenerateFinalSynthesisCommand

```text
TenantId
CapSessionId
SynthesisDraftRef
ConsultantEdits
ValidationState
```

Objectif : produire une synthèse finale éditable et validable par le consultant.

### GenerateActionPlanCommand

```text
TenantId
CapSessionId
FinalSynthesisRef
ConsultantInputs
```

Objectif : produire le plan d'action selon la logique existante.

### GenerateDeliverablePackageCommand

```text
TenantId
CapSessionId
FinalSynthesisRef
ActionPlanRef
ExportOptions
```

Objectif : produire le package DOCX/PDF/ZIP selon la logique `v1.0-pro`.

### GenerateAiAnalysisDraftCommand

```text
TenantId
CapSessionId
AnalysisSnapshotRef
AiMode
Guardrails
```

Objectif : produire `AIAnalysisDraft` selon la logique `v2.0-ai`.

### GenerateAiAnalysisManifestCommand

```text
TenantId
CapSessionId
AiAnalysisDraftRef
AnalysisSnapshotRef
ProviderMetadata
```

Objectif : produire ou valider `AIAnalysisManifest`.

## Références de sortie

Les sorties du moteur doivent être référencées et non forcément chargées entièrement dans le retour API.

```text
ResponseSessionRef
AnalysisSnapshotRef
SynthesisDraftRef
FinalSynthesisRef
ActionPlanRef
AiAnalysisDraftRef
AiAnalysisManifestRef
DeliverablePackageRef
```

Chaque référence contient au minimum :

```text
TenantId
CapSessionId
ArtifactId
ArtifactType
StorageKey
GeneratedAt
Status
Checksum si disponible
```

## Résultat standard

```csharp
public sealed record CapEngineResult<T>(
    bool Succeeded,
    T? Value,
    IReadOnlyList<CapEngineError> Errors,
    IReadOnlyList<CapEngineWarning> Warnings);
```

## Erreurs standard

```text
InvalidInput
MissingResponse
IncompatibleFormat
GenerationFailed
ExportFailed
AiGuardrailFailed
UnauthorizedTenantAccess
StorageFailed
UnexpectedError
```

## Modes d'adaptation possibles

### Mode 1 - Adapter Node existant

Le SaaS appelle les scripts existants du `questionnaire-engine/tools` via un adapter serveur.

Avantages :

- réutilisation immédiate ;
- faible risque fonctionnel ;
- compatibilité forte avec v1/v2.

Inconvénients :

- orchestration process à sécuriser ;
- gestion fichiers à cadrer ;
- plus difficile à tester finement côté .NET.

### Mode 2 - Portage progressif vers .NET

Le moteur CAP est progressivement réimplémenté côté .NET.

Avantages :

- intégration SaaS plus naturelle ;
- tests .NET plus directs ;
- meilleure intégration typée.

Inconvénients :

- risque de divergence avec v1/v2 ;
- effort plus important ;
- nécessite des tests de compatibilité stricts.

### Décision initiale recommandée

```text
Démarrer par le Mode 1 - Adapter Node existant.
```

Raison : préserver la compatibilité et livrer plus vite le socle SaaS.

Le portage .NET peut devenir un chantier ultérieur si nécessaire.

## Stockage des artefacts

Les artefacts CAP doivent être stockés via `IFileStoragePort`.

```text
LocalFileStorageAdapter pour développement local
AzureDevFileStorageAdapter pour expérimentation Azure
```

Le stockage doit rester portable.

## Sécurité tenant

Chaque appel doit vérifier :

```text
TenantId de la commande
TenantId de la session CAP
TenantId de l'utilisateur courant
TenantId de l'artefact source
```

Aucun artefact d'un tenant ne doit pouvoir être utilisé par un autre tenant.

## Tests obligatoires

```text
[x] génération ResponseSession compatible v1
[x] génération AnalysisSnapshot compatible v1
[x] génération sans IA
[x] génération avec IA optionnelle
[x] non-remise automatique du brouillon IA
[x] génération exports DOCX/PDF/ZIP
[x] isolation tenant
[x] erreur explicite si format incompatible
```

## Décision

`ICapEnginePort` et `IAiAnalysisPort` deviennent les contrats d'intégration du moteur CAP dans `v3.0-saas`.

Le premier développement doit créer les ports, les commandes, les références de sortie et un adapter local basé sur les scripts existants.
