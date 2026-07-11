# Roadmap produit - CAP Method

## Objectif

Ce document fige la trajectoire produit de CAP Method après la publication de `v1.0-pro`.

Il sépare clairement :

- la version stable exploitable ;
- la future couche IA assistée ;
- la future plateforme SaaS ;
- la stratégie de branches Git.

## Synthèse

```text
v1.0-pro
  = version publiée, stable, exploitable professionnellement

v2.0-ai
  = analyse IA assistée, cadrée et validée par le consultant

v3.0-saas
  = plateforme SaaS professionnelle basée sur le moteur CAP
```

## Règle principale

```text
v1.0-pro reste stable.
Les évolutions IA et SaaS ne doivent pas casser la chaîne publiée.
```

Le moteur existant reste le socle fonctionnel.

Les nouvelles versions doivent s'appuyer dessus progressivement.

---

# 1. Version stable - v1.0-pro

## Statut

```text
PUBLISHED
```

## Rôle

`v1.0-pro` est la première version professionnelle exploitable.

Elle permet :

- de générer les questionnaires ;
- de collecter les réponses ;
- d'importer les CSV ;
- de produire une analyse structurée ;
- de générer une synthèse finale ;
- de générer un plan d'action ;
- de produire DOCX, PDF et ZIP ;
- de livrer un dossier exploitable.

## Règle

Cette version ne doit plus recevoir de nouvelles fonctionnalités.

Les corrections critiques peuvent être documentées, mais les évolutions doivent partir dans les branches futures.

## Branche / tag

```text
tag: v1.0-pro
branch stable éventuelle: release/v1.0-pro
```

---

# 2. Jalon IA - v2.0-ai

## Statut

```text
FROZEN SCOPE - FUTURE MILESTONE
```

## Vision

Ajouter une analyse IA assistée, sans remplacer le consultant.

Principe :

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

## Chaîne cible

```text
ResponseSession
  ↓
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
ConsultantReview
  ↓
FinalSynthesis
  ↓
ActionPlan
```

## Livrable cible

```text
ai-analysis-draft.md
```

## Contenu attendu

- synthèse neutre des réponses ;
- thèmes récurrents ;
- valeurs exprimées ;
- motivations apparentes ;
- compétences évoquées ;
- contraintes et freins ;
- hypothèses professionnelles ;
- points à clarifier ;
- questions d'entretien ;
- risques d'interprétation ;
- validation consultant obligatoire.

## Garde-fous

L'IA doit utiliser des formulations prudentes :

```text
Les réponses suggèrent...
Une hypothèse possible est...
Ce point mérite validation...
Le consultant pourra explorer...
```

Elle ne doit pas produire de conclusions définitives :

```text
Cette personne est...
Le bon métier est...
Il faut absolument...
Le diagnostic est...
```

## Branche recommandée

```text
feature/v2-ai
```

ou, pour un travail plus long :

```text
product/v2-ai-analysis
```

## Premiers livrables techniques

- `questionnaire-engine/ai/` ;
- `generate-ai-analysis-draft.mjs` ;
- documentation des prompts ;
- garde-fous ;
- CI de validation des sections obligatoires ;
- sortie IA de test sans fournisseur externe obligatoire.

---

# 3. Jalon SaaS - v3.0-saas

## Statut

```text
FROZEN SCOPE - FUTURE PRODUCT MILESTONE
```

## Vision

Transformer CAP Method en plateforme SaaS professionnelle.

Le SaaS doit permettre à un consultant de gérer plusieurs bénéficiaires depuis une interface web sécurisée.

## Principe

```text
Le SaaS orchestre le parcours.
Le moteur CAP produit les livrables.
L'IA assiste si elle est activée.
Le consultant reste responsable de la validation.
```

## Chaîne produit cible

```text
Consultant
  ↓
Création bénéficiaire
  ↓
Session CAP
  ↓
Questionnaires en ligne
  ↓
Réponses stockées
  ↓
Analyse structurée
  ↓
Analyse IA optionnelle
  ↓
Synthèse éditable
  ↓
Plan d'action
  ↓
Exports DOCX/PDF/ZIP
  ↓
Archivage
```

## Modules cibles

```text
Identity
Tenant
Consultant
Beneficiary
CapSession
Questionnaire
ResponseCollection
Analysis
AIAnalysis
Synthesis
ActionPlan
Export
Review
Notification
Audit
Administration
```

## MVP SaaS minimal

Le MVP doit rester limité à :

- authentification ;
- gestion consultant ;
- gestion bénéficiaire ;
- création session CAP ;
- questionnaires en ligne ;
- collecte réponses ;
- génération analyse ;
- synthèse éditable ;
- plan d'action ;
- exports PDF/DOCX ;
- isolation des données.

## Exclusions du premier SaaS

- marketplace ;
- paiement en ligne obligatoire ;
- application mobile native ;
- CRM complet ;
- signature électronique avancée ;
- IA autonome ;
- automatisation juridique ;
- multi-langue complète.

## Branche recommandée

```text
feature/v3-saas
```

ou, pour un socle long :

```text
product/v3-saas-foundation
```

---

# 4. Stratégie de branches

## Main

```text
main
```

Rôle :

- documentation stable ;
- état publié ;
- roadmap officielle ;
- corrections mineures documentaires.

## Release stable

```text
release/v1.0-pro
```

Optionnelle.

Rôle :

- conserver une branche stable alignée sur la release publiée ;
- appliquer uniquement des correctifs critiques ;
- ne pas recevoir les évolutions IA/SaaS.

## IA

```text
feature/v2-ai
```

Rôle :

- développer la couche IA assistée ;
- préserver la compatibilité avec la chaîne `v1.0-pro` ;
- garder l'IA optionnelle.

## SaaS

```text
feature/v3-saas
```

Rôle :

- concevoir puis développer la plateforme SaaS ;
- extraire ou réutiliser le moteur CAP ;
- poser l'architecture produit.

## Règle de fusion

Aucune branche future ne doit être fusionnée dans `main` sans :

- documentation ;
- CI verte ;
- garde-fous ;
- preuve de compatibilité avec `v1.0-pro` ;
- décision explicite de jalon.

---

# 5. Ordre recommandé

## Court terme

```text
1. Exploiter v1.0-pro avec un vrai bénéficiaire
2. Collecter les retours terrain
3. Corriger la documentation d'exploitation si nécessaire
```

## Moyen terme

```text
4. Démarrer feature/v2-ai
5. Produire ai-analysis-draft.md
6. Tester l'intérêt réel de l'IA dans la préparation consultant
```

## Long terme

```text
7. Cadrer le MVP SaaS
8. Démarrer product/v3-saas-foundation
9. Construire le parcours consultant / bénéficiaire
10. Intégrer progressivement le moteur CAP
```

---

# 6. Décision produit

CAP Method doit évoluer en trois niveaux :

```text
Méthode professionnelle
  ↓
Moteur d'analyse assistée
  ↓
Plateforme SaaS
```

La priorité immédiate reste l'exploitation terrain de `v1.0-pro`.

L'IA et le SaaS sont figés comme trajectoire, mais ne doivent pas retarder les premiers usages réels.
