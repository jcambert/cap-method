# CAP Method v3.0-saas - Jalon SaaS professionnel

## Statut

```text
FROZEN SCOPE - FUTURE PRODUCT MILESTONE
```

Le jalon `v3.0-saas` est figé comme trajectoire produit majeure de CAP Method.

Il ne modifie pas le périmètre publié de `v1.0-pro` et ne remplace pas le jalon `v2.0-ai`.

## Vision

`v3.0-saas` vise à transformer le moteur CAP en application SaaS professionnelle.

Le SaaS doit permettre à un consultant, coach ou accompagnant d'exploiter CAP avec plusieurs bénéficiaires depuis une interface web sécurisée.

## Position dans la roadmap

```text
v1.0-pro
  = moteur exploitable professionnellement en mode fichiers / scripts

v2.0-ai
  = analyse IA assistée avec validation consultant obligatoire

v3.0-saas
  = plateforme web SaaS multi-bénéficiaires construite sur le moteur CAP
```

## Objectif principal

Fournir une application web permettant de gérer :

- les consultants ;
- les bénéficiaires ;
- les parcours CAP ;
- les questionnaires ;
- les réponses ;
- les analyses ;
- les synthèses ;
- les plans d'action ;
- les exports ;
- les validations ;
- l'archivage ;
- la facturation future éventuelle.

## Principe directeur

```text
Le SaaS orchestre le parcours.
Le moteur CAP produit les livrables.
L'IA assiste si elle est activée.
Le consultant reste responsable de la validation.
```

## Périmètre inclus

### Produit SaaS

- application web consultant ;
- espace bénéficiaire ;
- gestion des sessions CAP ;
- suivi d'avancement ;
- génération et partage des questionnaires ;
- collecte des réponses ;
- génération des analyses ;
- édition des synthèses ;
- génération DOCX/PDF/ZIP ;
- historique des versions ;
- validation consultant ;
- validation bénéficiaire ;
- notifications ;
- gestion des statuts de parcours.

### Sécurité et conformité

- authentification ;
- rôles et permissions ;
- isolation des données par tenant ;
- journalisation ;
- gestion des données personnelles ;
- export et suppression des données ;
- consentement bénéficiaire ;
- politique de conservation ;
- chiffrement des données sensibles si nécessaire.

### Exploitation

- environnement de développement ;
- environnement de test ;
- environnement de production ;
- CI/CD ;
- sauvegardes ;
- supervision ;
- logs applicatifs ;
- gestion des erreurs ;
- documentation d'exploitation.

## Périmètre exclu de v3.0-saas initial

Le premier jalon SaaS exclut :

- marketplace publique ;
- paiement en ligne obligatoire ;
- application mobile native ;
- signature électronique avancée ;
- intégration complète CRM ;
- multi-langue complète ;
- IA autonome sans validation ;
- certification réglementaire ;
- automatisation juridique complète.

Ces éléments pourront être traités dans des jalons ultérieurs.

## Modules fonctionnels cibles

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

## Parcours utilisateur cible

### Consultant

1. créer un compte ;
2. créer ou rejoindre un espace professionnel ;
3. créer un bénéficiaire ;
4. lancer un parcours CAP ;
5. envoyer les questionnaires ;
6. suivre les réponses ;
7. générer l'analyse ;
8. relire et ajuster la synthèse ;
9. générer les livrables ;
10. organiser la restitution ;
11. archiver le dossier.

### Bénéficiaire

1. recevoir une invitation ;
2. accepter le cadre d'utilisation ;
3. répondre aux questionnaires ;
4. suivre l'avancement ;
5. relire certains éléments si nécessaire ;
6. recevoir les livrables validés.

## Critères d'acceptation

`v3.0-saas` sera considéré terminé lorsque :

- un consultant peut créer un bénéficiaire ;
- un parcours CAP peut être lancé ;
- les questionnaires peuvent être remplis en ligne ;
- les réponses sont stockées et rattachées à une session ;
- l'analyse peut être générée ;
- la synthèse peut être relue et éditée ;
- le plan d'action peut être généré ;
- les exports peuvent être produits ;
- les données sont isolées par tenant ;
- les droits d'accès sont maîtrisés ;
- un minimum d'audit est disponible ;
- la documentation d'exploitation existe.

## Décision

`v3.0-saas` est figé comme jalon produit stratégique.

Les travaux de conception peuvent commencer sans modifier le périmètre stable `v1.0-pro`.
