# Architecture produit - CAP Method v3.0-saas

## Objectif

Définir l'architecture cible du SaaS CAP Method.

Cette architecture doit transformer le moteur actuel en plateforme web utilisable par des consultants et des bénéficiaires.

## Vue d'ensemble

```text
Frontend Web
  ↓
API Application
  ↓
Domaine CAP
  ↓
Moteur Questionnaire / Analyse / Synthèse / Export
  ↓
Stockage sécurisé
```

## Couches principales

### Frontend

Interface web pour :

- consultants ;
- bénéficiaires ;
- administrateurs.

Fonctions :

- tableau de bord ;
- gestion des bénéficiaires ;
- suivi des parcours ;
- questionnaires en ligne ;
- éditeur de synthèse ;
- visualisation des analyses ;
- génération et téléchargement des exports.

### API Application

Couche d'orchestration :

- authentification ;
- autorisation ;
- gestion des tenants ;
- commandes métier ;
- requêtes de lecture ;
- déclenchement des analyses ;
- génération des exports ;
- notifications.

### Domaine CAP

Noyau métier :

- `CapSession` ;
- `Beneficiary` ;
- `Consultant` ;
- `Questionnaire` ;
- `ResponseSession` ;
- `AnalysisSnapshot` ;
- `AIAnalysisDraft` ;
- `FinalSynthesis` ;
- `ActionPlan` ;
- `DeliverablePackage`.

### Moteur existant

Le moteur `questionnaire-engine` reste la base fonctionnelle :

- CMDL ;
- réponses ;
- analyse ;
- synthèse ;
- plan d'action ;
- exports.

À terme, il pourra être extrait en librairie ou service interne.

### Stockage

Stockage cible :

- base relationnelle ;
- stockage de fichiers ;
- stockage des exports ;
- journal d'audit ;
- logs applicatifs.

## Modèle multi-tenant

Chaque donnée métier doit être rattachée à un tenant.

```text
Tenant
  ├── Consultants
  ├── Beneficiaries
  ├── CapSessions
  ├── Deliverables
  └── AuditLogs
```

Règle :

```text
Aucun consultant ne doit pouvoir accéder aux données d'un autre tenant.
```

## Statuts de session CAP

```text
Draft
QuestionnairesSent
InProgress
ResponsesCompleted
AnalysisGenerated
ConsultantReview
BeneficiaryReview
Validated
Delivered
Archived
```

## Intégration IA

L'IA reste optionnelle.

```text
AnalysisSnapshot
  ↓
AIAnalysisDraft optional
  ↓
ConsultantReview mandatory
```

Le SaaS doit pouvoir fonctionner sans fournisseur IA.

## Exports

Le SaaS doit produire :

```text
CAP-SYNTHESE-FINALE.docx
CAP-SYNTHESE-FINALE.pdf
CAP-PLAN-ACTION.docx
CAP-PLAN-ACTION.pdf
CAP-DELIVERABLES-{session-id}.zip
```

## Notifications

Notifications cibles :

- invitation bénéficiaire ;
- questionnaire en attente ;
- réponses complétées ;
- analyse disponible ;
- synthèse à relire ;
- livrables prêts ;
- rappel consultant.

## Audit

Chaque action sensible doit être auditée :

- création bénéficiaire ;
- envoi questionnaire ;
- soumission réponse ;
- génération analyse ;
- génération IA ;
- modification synthèse ;
- génération export ;
- téléchargement livrable ;
- archivage ;
- suppression.

## Déploiement cible

Environnements :

```text
local
staging
production
```

La production devra inclure :

- sauvegardes ;
- monitoring ;
- logs ;
- gestion des erreurs ;
- politique de rétention ;
- restauration testée.
