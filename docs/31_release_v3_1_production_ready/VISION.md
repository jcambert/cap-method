# Vision produit - v3.1-saas-production-ready

## Constat de départ

`v3.0-saas` a validé le socle Blazor WebAssembly hosted / ASP.NET Core, le JWT, le contexte tenant, les bénéficiaires, les sessions CAP, la persistance InMemory/PostgreSQL et la CI.

`v3.1-saas-production-ready` doit transformer ce socle en parcours métier complet exploitable par un consultant et un bénéficiaire.

## État d'avancement

```text
Lots 0 à 7 = intégrés et validés par CI
Lots 8 à 13 = restant à livrer
Release production-ready = non atteinte
```

Le parcours est actuellement opérationnel jusqu'à l'analyse structurée :

```text
Connexion consultant
  ↓
Navigation et tableau de bord
  ↓
Création bénéficiaire et session CAP
  ↓
Workflow CAP
  ↓
Espace bénéficiaire sécurisé
  ↓
Questionnaires en ligne
  ↓
Analyse structurée déterministe
```

La suite restant à livrer :

```text
Synthèse éditable
  ↓
Plan d'action
  ↓
Exports livrables
  ↓
Durcissement production
  ↓
Release v3.1
```

## Principes produit

```text
Le consultant pilote.
Le bénéficiaire répond.
Le système structure.
L'IA reste optionnelle.
Le livrable final est validé humainement.
Aspire facilite le développement mais n'est pas une dépendance de production.
```

## Périmètre production-ready minimal

La v3.1 doit couvrir :

- authentification exploitable hors mode développement ;
- navigation consultant et espace bénéficiaire sécurisé ;
- workflow CAP ;
- questionnaires et réponses persistés durablement ;
- suivi d'avancement ;
- analyse structurée persistable et relisible ;
- synthèse éditable ;
- plan d'action ;
- exports livrables ;
- configuration PostgreSQL et migrations ;
- observabilité, audit et sécurité minimaux.

## Situation réelle au Lot 7

Déjà disponible :

- authentification consultant et bénéficiaire ;
- isolation tenant/bénéficiaire ;
- navigation et workflow ;
- questionnaires, brouillons, soumission et progression ;
- analyse structurée déterministe ;
- base technique `.slnx`, packages centralisés et Aspire dev.

Écarts bloquant la production :

- réponses questionnaires encore stockées en mémoire ;
- analyse non persistée durablement ;
- synthèse, plan d'action et exports absents ;
- migrations des nouveaux agrégats absentes ;
- observabilité et audit incomplets.

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

La v3.1 est réussie lorsque le consultant peut conduire un bilan de bout en bout, avec données persistées, synthèse relue, plan d'action, exports exploitables et exploitation production documentée, sans manipulation technique manuelle.