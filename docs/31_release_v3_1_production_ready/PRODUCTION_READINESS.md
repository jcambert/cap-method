# Production Readiness - v3.1-saas-production-ready

## Statut global

```text
NOT READY FOR PRODUCTION
Lots fonctionnels validés = 0 à 7
Lots restants = 8 à 13
```

Ce document distingue les fonctionnalités intégrées des critères réellement satisfaits pour une exploitation production.

## Matrice consolidée

| Domaine | Critère | État | Commentaire |
|---|---|---|---|
| Sécurité | Token dev indisponible hors Development | ✅ Satisfait | Livré au Lot 2 |
| Sécurité | Endpoints métier protégés | ✅ Satisfait | JWT requis |
| Sécurité | Tenant résolu côté serveur | ✅ Satisfait | Aucun tenant saisi par l'UI métier |
| Sécurité | Isolation bénéficiaire | ✅ Satisfait | Claim bénéficiaire dédié |
| Sécurité | Audit des actions sensibles | ❌ À faire | Lot 13 |
| Données | PostgreSQL disponible pour le socle | ✅ Partiel | Infrastructure existante |
| Données | Questionnaires persistés en PostgreSQL | ❌ Bloquant | Stockage mémoire au Lot 6 |
| Données | Analyse persistée durablement | ❌ Bloquant | Génération à la demande au Lot 7 |
| Données | Migrations EF Core des agrégats v3.1 | ❌ Bloquant | Lot 11 |
| Métier | Navigation consultant | ✅ Satisfait | Lot 1 |
| Métier | Authentification production minimale | ✅ Satisfait | Lot 2 |
| Métier | Workflow CAP | ✅ Satisfait | Lots 3 et 4 |
| Métier | Espace bénéficiaire sécurisé | ✅ Satisfait | Lot 5 |
| Métier | Questionnaires en ligne | ✅ Fonctionnel | Persistance durable manquante |
| Métier | Analyse structurée | ✅ Fonctionnel | Snapshot durable manquant |
| Métier | Synthèse éditable | ❌ À faire | Lot 8 |
| Métier | Plan d'action | ❌ À faire | Lot 9 |
| Métier | Exports livrables | ❌ À faire | Lot 10 |
| Qualité | Solution `.slnx` | ✅ Satisfait | Restore/build CI |
| Qualité | Packages centralisés | ✅ Satisfait | `Directory.Packages.props` |
| Qualité | Aspire pour le développement | ✅ Satisfait | Non requis en production |
| Qualité | Tests domaine/application/infrastructure/compatibilité | ✅ Satisfait | CI verte Lots 0 à 7 |
| Exploitation | Configuration production complète | ❌ À faire | Lot 11 |
| Exploitation | Observabilité minimale | ❌ À faire | Lot 12 |
| Exploitation | Audit et sécurité minimale | ❌ À faire | Lot 13 |

## 1. Sécurité

Obligatoire avant release :

- endpoints protégés ;
- tenant et bénéficiaire résolus côté serveur ;
- tokens de développement désactivés hors Development ;
- secrets non versionnés ;
- tests inter-tenant ;
- audit des opérations sensibles ;
- messages UI sans fuite technique.

Les quatre premiers points sont largement couverts. L'audit, les tests de sécurité consolidés et le traitement uniforme des erreurs restent à finaliser.

## 2. Données et persistance

Obligatoire avant release :

```text
PostgreSQL = référence production
InMemory = local/dev/test uniquement
Migrations EF Core = présentes et documentées
Questionnaires = persistés
Analyses = persistées ou reproductibles avec snapshot traçable
Synthèses et plans d'action = persistés et isolés par tenant
```

Le stockage mémoire des questionnaires est actuellement un blocage explicite de mise en production.

## 3. Parcours métier

Disponible jusqu'au Lot 7 : création du dossier, workflow, espace bénéficiaire, questionnaires, progression et analyse structurée.

Restant pour un bilan complet :

- synthèse éditable et validation humaine ;
- plan d'action ;
- exports ;
- clôture démontrable avec livrables prêts.

## 4. Expérience utilisateur

Déjà couvert : navigation par pages, tableau de bord minimal, détail de session, workflow et espace bénéficiaire.

À consolider : cohérence des erreurs, états de chargement, désactivation des actions et ergonomie des prochains éditeurs.

## 5. Qualité technique

Règles maintenues :

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

## 6. Critères de release

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

À l'issue du Lot 7, ces critères ne sont pas encore tous satisfaits.