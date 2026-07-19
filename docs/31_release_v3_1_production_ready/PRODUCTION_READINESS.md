# Production Readiness - v3.1-saas-production-ready

## Statut global

```text
NOT READY FOR PRODUCTION
Lots validés dans main = 0 à 7
Lot 8 = implémenté, CI à vérifier
Lots restants = 9 à 13
```

Ce document distingue les fonctionnalités disponibles des critères réellement satisfaits pour une exploitation production.

## Matrice consolidée

| Domaine | Critère | État | Commentaire |
|---|---|---|---|
| Sécurité | Token dev indisponible hors Development | ✅ Satisfait | Lot 2 |
| Sécurité | Endpoints métier protégés | ✅ Satisfait | JWT requis |
| Sécurité | Tenant résolu côté serveur | ✅ Satisfait | Aucun tenant métier accepté depuis l'UI |
| Sécurité | Isolation bénéficiaire | ✅ Satisfait | Claim bénéficiaire dédié |
| Sécurité | Audit des actions sensibles | ❌ À faire | Lot 13 |
| Données | PostgreSQL disponible pour le socle | 🟡 Partiel | Infrastructure existante |
| Données | Questionnaires persistés en PostgreSQL | ❌ Bloquant | Stockage mémoire |
| Données | Analyse persistée durablement | ❌ Bloquant | Génération à la demande |
| Données | Synthèse persistée durablement | ❌ Bloquant | Stockage mémoire au Lot 8 |
| Données | Migrations EF Core des agrégats v3.1 | ❌ Bloquant | Lot 11 |
| Métier | Navigation consultant | ✅ Satisfait | Lot 1 |
| Métier | Authentification production minimale | ✅ Satisfait | Lot 2 |
| Métier | Workflow CAP | ✅ Satisfait | Lots 3 et 4 |
| Métier | Espace bénéficiaire sécurisé | ✅ Satisfait | Lot 5 |
| Métier | Questionnaires en ligne | ✅ Fonctionnel | Persistance durable manquante |
| Métier | Analyse structurée | ✅ Fonctionnel | Snapshot durable manquant |
| Métier | Synthèse éditable | 🔵 Implémenté | Validation humaine et verrouillage disponibles |
| Métier | Plan d'action | ❌ À faire | Lot 9 |
| Métier | Exports livrables | ❌ À faire | Lot 10 |
| Qualité | Solution `.slnx` | ✅ Satisfait | Restore/build CI |
| Qualité | Packages centralisés | ✅ Satisfait | `Directory.Packages.props` |
| Qualité | Aspire pour le développement | ✅ Satisfait | Non requis en production |
| Qualité | `.gitignore` adapté | ✅ Satisfait | Build, IDE, secrets et artefacts locaux exclus |
| Qualité | Tests domaine/application/infrastructure/compatibilité | 🟡 À vérifier | Tests Lot 8 ajoutés |
| Exploitation | Configuration production complète | ❌ À faire | Lot 11 |
| Exploitation | Observabilité minimale | ❌ À faire | Lot 12 |
| Exploitation | Audit et sécurité minimale | ❌ À faire | Lot 13 |

## Données et persistance

Obligatoire avant release :

```text
PostgreSQL = référence production
InMemory = local/dev/test uniquement
Migrations EF Core = présentes et documentées
Questionnaires = persistés
Analyses = persistées ou reproductibles avec snapshot traçable
Synthèses = persistées, versionnées et isolées par tenant
Plans d'action = persistés et isolés par tenant
```

Les stockages mémoire des questionnaires et de la synthèse sont des blocages explicites de mise en production.

## Parcours métier

Disponible jusqu'au Lot 8 : création du dossier, workflow, espace bénéficiaire, questionnaires, progression, analyse structurée et synthèse éditable avec validation humaine.

Restant pour un bilan complet :

- plan d'action ;
- exports ;
- clôture démontrable avec livrables prêts.

## Qualité technique

```text
.NET = 10
C# = LangVersion 14
Architecture = Domain / Application / Infrastructure / Server / Client / Shared
Solution = .slnx
Packages = centralisés
Aspire = développement uniquement
IA = optionnelle
Validation humaine = obligatoire
```

## Critères de release

Le tag `v3.1-saas-production-ready` est autorisé uniquement si :

```text
Parcours métier complet = oui
Persistance PostgreSQL de tous les agrégats = oui
Migrations testées = oui
Synthèse, plan d'action et exports = oui
Observabilité minimale = oui
Audit et sécurité minimale = oui
CI main = verte
Documentation finale = à jour
Aucun bug bloquant connu = oui
```

À l'issue de l'implémentation du Lot 8, ces critères ne sont pas encore tous satisfaits.
