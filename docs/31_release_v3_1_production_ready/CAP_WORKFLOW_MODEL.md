# Modèle workflow CAP - v3.1-saas-production-ready

## Objectif

Le Lot 3 introduit un modèle métier de workflow CAP exploitable par le SaaS.

Le but est de sortir d'une session CAP générique et de préparer un parcours pilotable étape par étape.

## Principe

```text
CapSession
  -> Status métier
  -> CapWorkflowPlan
  -> CapWorkflowProgress
  -> étapes affichables dans l'UI
```

La session reste l'agrégat principal.

Le workflow n'est pas encore persisté comme une entité séparée. Il est dérivé de l'état de la session afin de ne pas introduire prématurément de dette de stockage ou de migration.

## Étapes standard

```text
Intake             - Cadrage du bilan
Questionnaires     - Questionnaires bénéficiaire
Responses          - Réponses complétées
StructuredAnalysis - Analyse structurée
ConsultantReview   - Revue consultant
Synthesis          - Synthèse validée
Delivery           - Livraison des livrables
Archive            - Archivage
```

## Étape optionnelle IA

```text
AiDraft - Brouillon IA optionnel
```

Cette étape n'apparaît dans le plan que si l'IA est activée pour la session.

## Garde-fou IA

```text
IA activée = l'étape AiDraft existe dans le workflow
IA désactivée = l'étape AiDraft n'existe pas dans le workflow
```

Le modèle conserve le principe validé depuis v2 :

```text
L'IA assiste.
Le consultant valide.
L'IA ne produit pas de diagnostic final automatique.
```

## États de session couverts

```text
Draft
QuestionnairesSent
InProgress
ResponsesCompleted
AnalysisGenerated
AIAnalysisDraftGenerated
ConsultantReview
Validated
Delivered
Archived
```

Chaque état est mappé vers une étape de workflow.

## Progression

`CapWorkflowProgress` expose :

```text
CurrentStep
Steps
CompletedRequiredStepCount
RequiredStepCount
CompletionRate
```

Le calcul de progression ne dépend pas de l'UI.

## Décisions techniques

```text
Enum = non
switch/case = non
Workflow persisté = non pour Lot 3
Contrat API = inchangé pour Lot 3
Migration EF = non requise pour Lot 3
```

## Prochain lot

Le Lot 4 pourra exploiter ce modèle pour afficher un vrai parcours métier CAP dans l'interface SaaS.
