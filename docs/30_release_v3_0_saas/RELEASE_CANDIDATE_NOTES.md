# Release Candidate Notes - v3.0-saas-rc1

## Version

```text
v3.0-saas-rc1
```

## Statut

```text
PUBLISHED
```

## Commit taggé

```text
fd8563861ebbd74e474ff2904dd7ae38ff4048ec
```

## Nature de la version

Cette version est une release candidate technique et fonctionnelle pour la version SaaS de CAP Method.

Elle ne remplace pas :

```text
v1.0-pro
v2.0-ai
```

Elle ajoute un socle web SaaS qui encapsule progressivement les usages CAP existants.

## Fonctionnalités incluses

```text
Application Blazor WebAssembly hosted
API ASP.NET Core
Séparation Client / Server / Shared
Domain/Application/Infrastructure
Création bénéficiaire
Création session CAP
Liste sessions CAP
Détail session CAP
JWT dev authentication
Contexte tenant serveur
Support PostgreSQL EF Core
Migrations EF Core
Mode InMemory local
UI Blazor structurée en composants
CI GitHub Actions dédiée v3 SaaS
```

## Compatibilité métier CAP

```text
CAP v1 sans IA obligatoire = conservé
CAP v2 avec IA optionnelle = conservé
IA automatique imposée = non
Diagnostic automatique final = non
Livrables métier automatiques en production = non inclus dans cette RC
```

## Sécurité et tenant

```text
Endpoints métier protégés par JWT
Tenant résolu côté serveur
Utilisateur résolu côté serveur
Fallback dev uniquement en Development
Pas de TenantId métier saisi côté UI
Pas de TenantId métier passé en query string
```

## Persistance

```text
Développement local par défaut = InMemory
Persistance cible supportée = PostgreSQL
Migrations EF disponibles = oui
Configuration de provider = oui
```

## Limitations connues

```text
Pas de gestion complète des comptes utilisateurs
Pas de back-office admin complet
Pas de workflow complet de bilan en pages séparées
Pas de déploiement Azure obligatoire
Pas de module paiement
Pas de génération finale de livrables métier dans l'UI SaaS
```

## Conditions de publication du tag RC

```text
PR Lot 15 fusionnée dans main = oui
CI main OK = oui
Validation manuelle de la checklist RC = oui
Décision explicite de tag v3.0-saas-rc1 = oui
Tag v3.0-saas-rc1 publié = oui
```

## Commande utilisée

```bash
git checkout main
git pull origin main
git tag -a v3.0-saas-rc1 fd8563861ebbd74e474ff2904dd7ae38ff4048ec -m "Release candidate v3.0-saas-rc1"
git push origin v3.0-saas-rc1
```

## Suite après RC

Après `v3.0-saas-rc1`, les prochaines itérations logiques sont :

```text
v3.0-saas-rc2 - corrections ou ajustements après validation RC1
v3.0-saas - tag stable après validation terrain
v3.1-saas - workflow métier CAP complet côté SaaS
```
