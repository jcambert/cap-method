# Index documentaire - v3.1-saas-production-ready

## Documents transverses

| Document | Rôle | État |
|---|---|---|
| `README.md` | Vue d'ensemble v3.1 | À jour jusqu'au Lot 8 |
| `VISION.md` | Vision produit et cible | À jour jusqu'au Lot 7, vision inchangée au Lot 8 |
| `USER_STORIES.md` | User stories et statut d'implémentation | À jour jusqu'au Lot 8 |
| `BACKLOG.md` | Lots livrés et travail restant | À jour jusqu'au Lot 8 |
| `PRODUCTION_READINESS.md` | Matrice des critères de production | À jour jusqu'au Lot 8 |

## Documentation technique transverse

| Document | Sujet |
|---|---|
| `TECH_SOLUTION_CENTRAL_PACKAGES.md` | Solution `.slnx` et Central Package Management |
| `TECH_ASPIRE_DEVELOPMENT.md` | Aspire AppHost pour le développement local |

## Lots validés dans main

| Lot | Statut | Documentation principale |
|---|---|---|
| 0 | ✅ CI OK | `LOT_0_STATUS.md` |
| 1 | ✅ CI OK | `LOT_1_STATUS.md` |
| 2 | ✅ CI OK | `LOT_2_STATUS.md` |
| 3 | ✅ CI OK | `LOT_3_STATUS.md` |
| 4 | ✅ CI OK | `LOT_4_STATUS.md` |
| 5 | ✅ CI OK | `LOT_5_STATUS.md` |
| 6 | ✅ CI OK | `LOT_6_STATUS.md`, `ONLINE_QUESTIONNAIRES.md` |
| 7 | ✅ CI OK | `LOT_7_STATUS.md`, `STRUCTURED_ANALYSIS.md` |

## Lot courant

| Lot | Statut | Documentation principale |
|---|---|---|
| 8 | 🟡 Implémenté, CI à vérifier | `LOT_8_STATUS.md`, `EDITABLE_SYNTHESIS.md` |

## Lots restants

```text
Lot 9   Plan d'action
Lot 10  Exports livrables
Lot 11  Configuration production et persistance
Lot 12  Observabilité minimale
Lot 13  Audit et sécurité minimale
```

## Règle de maintenance documentaire

Pour chaque nouveau lot :

1. créer ou mettre à jour la documentation fonctionnelle/technique du lot ;
2. mettre à jour `LOT_X_STATUS.md` après validation CI ;
3. mettre à jour `BACKLOG.md` ;
4. mettre à jour les statuts concernés dans `USER_STORIES.md` ;
5. mettre à jour `PRODUCTION_READINESS.md` lorsque le lot modifie un critère de release ;
6. mettre à jour `README.md`, `VISION.md` ou `docs/ROADMAP.md` lorsque le positionnement global évolue ;
7. ajouter le document dans cet index.

## Source de vérité

- le code et les tests dans `src/CapMethod.Saas/` prouvent l'implémentation ;
- les fichiers `LOT_X_STATUS.md` prouvent la validation du lot ;
- `PRODUCTION_READINESS.md` détermine si la release peut réellement être mise en production ;
- un lot fonctionnel validé ne signifie pas automatiquement que le critère de production associé est entièrement satisfait.
