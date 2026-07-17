# Release Candidate Status - v3.0-saas

## Version cible

```text
v3.0-saas
```

## Release candidate publiée

```text
v3.0-saas-rc1
```

## Statut

```text
PUBLISHED - RC1
```

## Commit taggé

```text
fd8563861ebbd74e474ff2904dd7ae38ff4048ec
```

## Objectif release candidate

Valider que la version SaaS de CAP Method dispose d'un socle professionnel minimal :

- application Blazor WebAssembly hosted ;
- API ASP.NET Core sécurisée par JWT ;
- contexte tenant côté serveur ;
- création de bénéficiaire ;
- création de session CAP ;
- liste des sessions du tenant ;
- détail d'une session ;
- stockage local en mémoire par défaut ;
- support PostgreSQL EF Core prêt ;
- migrations EF Core disponibles ;
- UI durcie en composants ;
- compatibilité CAP v1 sans IA obligatoire ;
- compatibilité CAP v2 avec IA optionnelle.

## Décisions confirmées

```text
Azure obligatoire en production = non
Azure utilisé pour développement / expérimentation = possible
IA obligatoire = non
CAP v1 conservé = oui
CAP v2 IA optionnelle conservé = oui
Multi-tenant côté serveur = oui
JWT obligatoire sur endpoints métier = oui
PostgreSQL supporté = oui
Mode mémoire local conservé = oui
```

## Lots v3 intégrés

```text
Lot 0  - Fondation SaaS documentaire et squelette technique = DONE
Lot 1  - Création session CAP minimale = DONE
Lot 2  - Lecture session CAP = DONE
Lot 3  - Liste sessions CAP = DONE
Lot 4  - Bénéficiaires = DONE
Lot 5  - EF Core PostgreSQL = DONE
Lot 6  - Migrations EF et configuration persistance = DONE
Lot 7  - Contexte tenant / utilisateur côté serveur = DONE
Lot 8  - Authentification JWT = DONE
Lot 9  - UI Blazor authentifiée = DONE
Lot 10 - UI création bénéficiaire = DONE
Lot 11 - UI création session CAP = DONE
Lot 12 - UI liste sessions CAP = DONE
Lot 13 - UI détail session CAP = DONE
Lot 14 - Durcissement UI pré-RC = DONE
Lot 15 - Préparation release candidate = DONE
```

## Critères de sortie RC

```text
Documentation release candidate présente = oui
Checklist release candidate présente = oui
Notes RC présentes = oui
CI v3-saas-validation OK = oui
PR Lot 15 fusionnée dans main = oui
Tag v3.0-saas-rc1 créé = oui
```

## Points explicitement hors périmètre RC

```text
Paiement SaaS = hors périmètre
Gestion complète des utilisateurs = hors périmètre
Back-office admin complet = hors périmètre
Génération automatique des livrables métier = hors périmètre v3 RC
Assistant IA automatique en production = hors périmètre v3 RC
Déploiement Azure obligatoire = hors périmètre
```

## Prochaine étape

Stabiliser les retours éventuels de `v3.0-saas-rc1`, puis décider soit d'une `v3.0-saas-rc2`, soit du tag stable `v3.0-saas`.
