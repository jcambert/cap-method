# Roadmap - CAP Method v3.0-saas

## Objectif

Décomposer la trajectoire SaaS en étapes maîtrisables.

`v3.0-saas` ne doit pas être développé comme un bloc unique.

## Séquence recommandée

```text
v1.0-pro stable
  ↓
v2.0-ai assistée
  ↓
v3.0-saas foundation
  ↓
v3.1-saas-mvp
  ↓
v3.2-saas-production
```

## Phase 0 - Cadrage produit

Objectif : définir le SaaS avant de coder.

Livrables :

- personas ;
- parcours consultant ;
- parcours bénéficiaire ;
- modèle économique cible ;
- périmètre MVP ;
- exigences de sécurité ;
- exigences RGPD ;
- schéma des données principales.

## Phase 1 - SaaS foundation

Objectif : poser le socle applicatif.

Fonctions :

- authentification ;
- tenants ;
- rôles ;
- gestion consultants ;
- gestion bénéficiaires ;
- création de sessions CAP ;
- tableau de bord minimal ;
- audit minimal.

## Phase 2 - Questionnaire online

Objectif : remplacer progressivement Google Forms.

Fonctions :

- rendu web des questionnaires CMDL ;
- invitation bénéficiaire ;
- progression par formulaire ;
- sauvegarde des réponses ;
- reprise de session ;
- validation des réponses ;
- export interne ResponseSession.

## Phase 3 - Moteur CAP intégré

Objectif : intégrer la chaîne existante dans le SaaS.

Fonctions :

- génération AnalysisSnapshot ;
- génération SynthesisDraft ;
- génération FinalSynthesis ;
- génération ActionPlan ;
- gestion des versions ;
- édition consultant.

## Phase 4 - Exports et livraison

Objectif : produire les livrables depuis l'interface.

Fonctions :

- génération DOCX ;
- génération PDF ;
- génération ZIP ;
- téléchargement sécurisé ;
- archivage ;
- historique des exports.

## Phase 5 - IA assistée optionnelle

Objectif : intégrer `v2.0-ai` dans le SaaS.

Fonctions :

- génération AIAnalysisDraft ;
- affichage des hypothèses IA ;
- validation consultant ;
- garde-fous de formulation ;
- traçabilité modèle/prompt ;
- désactivation complète possible.

## Phase 6 - Production readiness

Objectif : rendre le SaaS exploitable en production.

Fonctions :

- supervision ;
- logs ;
- sauvegardes ;
- restauration ;
- sécurité ;
- gestion erreurs ;
- documentation exploitation ;
- politique de conservation ;
- export/suppression données.

## MVP minimal recommandé

Le MVP SaaS ne doit contenir que :

```text
consultant
bénéficiaire
session CAP
questionnaires en ligne
réponses
analyse structurée
synthèse éditable
plan d'action
exports PDF/DOCX
```

Tout le reste doit être repoussé après validation terrain.

## Décision

La priorité n'est pas de tout automatiser.

La priorité est de rendre l'exploitation CAP plus simple, plus sûre et plus professionnelle pour plusieurs bénéficiaires.
