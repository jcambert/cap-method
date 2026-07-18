# Vision produit - v3.1-saas-production-ready

## Constat de départ

`v3.0-saas` est une version stable du socle SaaS, mais elle ne couvre pas encore les fonctionnalités indispensables à une mise en production réelle.

Elle valide :

- l'architecture Blazor WebAssembly hosted / ASP.NET Core ;
- l'API sécurisée par JWT ;
- le contexte tenant côté serveur ;
- la création de bénéficiaires ;
- la création et la consultation de sessions CAP ;
- la persistance InMemory / PostgreSQL ;
- la CI dédiée.

Elle ne suffit pas encore pour exploiter un bilan de compétences complet en production.

## Ambition v3.1

`v3.1-saas-production-ready` doit rendre le SaaS utilisable sur un parcours métier réel, même avec un périmètre encore volontairement limité.

La priorité est de produire un MVP production-ready, pas une plateforme complète.

## Parcours cible

```text
Consultant
  ↓
Connexion sécurisée
  ↓
Tableau de bord
  ↓
Création / suivi bénéficiaire
  ↓
Création session CAP
  ↓
Parcours bilan étape par étape
  ↓
Questionnaires en ligne
  ↓
Réponses bénéficiaire
  ↓
Analyse consultant
  ↓
Synthèse éditable
  ↓
Plan d'action
  ↓
Export livrables
  ↓
Clôture / archivage
```

## Principes produit

```text
Le consultant pilote.
Le bénéficiaire répond.
Le système structure.
L'IA assiste uniquement si elle est activée.
Le livrable final reste validé humainement.
```

## Périmètre production-ready minimal

La v3.1 doit couvrir au minimum :

- une authentification exploitable hors mode dev ;
- une navigation SaaS par pages ;
- un espace consultant utilisable ;
- un espace bénéficiaire sécurisé ;
- un workflow de session CAP ;
- la collecte persistée des réponses ;
- le suivi d'avancement ;
- une première chaîne de livrables ;
- des paramètres production documentés ;
- une stratégie de sécurité et d'audit minimale.

## Hors périmètre v3.1

```text
Paiement SaaS complet
Marketplace
Application mobile native
CRM complet
Multi-langue complète
Signature électronique avancée
Automatisation juridique
IA autonome de décision
```

## Critère de réussite

La v3.1 est réussie si un consultant peut réaliser un bilan de compétences de bout en bout dans l'application, avec des exports exploitables, sans dépendre de manipulations manuelles techniques.
