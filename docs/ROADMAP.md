# Roadmap produit - CAP Method

## Synthèse actuelle

```text
v1.0-pro
  = publiée et exploitable professionnellement

v2.0-ai
  = intégrée dans main, IA assistée optionnelle avec validation consultant

v3.0-saas
  = socle SaaS stable publié

v3.1-saas-production-ready
  = en cours, Lots 0 à 7 validés
```

## Principes

- `v1.0-pro` reste la référence stable de la méthode et des livrables historiques.
- `v2.0-ai` ajoute une assistance sans remplacer la validation humaine.
- `v3.0-saas` fournit le socle Blazor WebAssembly hosted / ASP.NET Core multi-tenant.
- `v3.1` construit le parcours métier complet et le durcissement production.
- Les évolutions SaaS ne doivent pas casser les contrats CAP v1/v2.

## État de v3.1

### Intégré dans `main`

```text
Lot 0  Cadrage production-ready
Lot 1  Navigation SaaS
Lot 2  Authentification production minimale
Lot 3  Modèle de workflow CAP
Lot 4  UI workflow CAP
Lot 5  Espace bénéficiaire sécurisé
Lot 6  Questionnaires en ligne
Lot 7  Analyse structurée SaaS
```

### Restant

```text
Lot 8   Synthèse éditable
Lot 9   Plan d'action
Lot 10  Exports livrables
Lot 11  Configuration et persistance production
Lot 12  Observabilité minimale
Lot 13  Audit et sécurité minimale
```

## Parcours produit cible

```text
Consultant
  ↓
Authentification et tableau de bord
  ↓
Bénéficiaire et session CAP
  ↓
Workflow par étapes
  ↓
Espace bénéficiaire sécurisé
  ↓
Questionnaires et réponses
  ↓
Analyse structurée
  ↓
Synthèse éditable et validation humaine
  ↓
Plan d'action
  ↓
Exports
  ↓
Clôture et archivage
```

Le parcours est actuellement livré jusqu'à l'analyse structurée.

## Blocages avant production

- questionnaires encore stockés en mémoire serveur ;
- analyse non persistée comme snapshot durable ;
- synthèse, plan d'action et exports non livrés ;
- migrations PostgreSQL des nouveaux agrégats absentes ;
- configuration, observabilité et audit à finaliser.

## Base technique SaaS

```text
.NET 10
C# 14
Blazor WebAssembly hosted
ASP.NET Core
Solution .slnx
NuGet centralisé
Aspire AppHost pour le développement
PostgreSQL comme cible de production
IA optionnelle
```

## Stratégie Git

```text
main = état validé
1 lot = 1 branche
1 branche = 1 PR
squash merge
CI verte obligatoire
mise à jour documentaire obligatoire
```

## Prochaine étape

```text
Lot 8 - Synthèse éditable
```

La documentation détaillée de v3.1 se trouve dans `docs/31_release_v3_1_production_ready/`.