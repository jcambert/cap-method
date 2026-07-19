# Backlog - v3.1-saas-production-ready

## Règle de livraison

```text
1 lot = 1 branche
1 branche = 1 PR
1 PR = squash merge
CI verte obligatoire
Documentation de lot et documentation transverse mises à jour
```

## Vue consolidée

| Lot | Priorité | Objet | Statut | Référence |
|---|---|---|---|---|
| 0 | P0 | Cadrage production-ready | ✅ Validé | `LOT_0_STATUS.md` |
| 1 | P0 | Navigation SaaS par pages | ✅ Validé | `LOT_1_STATUS.md` |
| 2 | P0 | Authentification production minimale | ✅ Validé | `LOT_2_STATUS.md` |
| 3 | P0 | Modèle de workflow CAP | ✅ Validé | `LOT_3_STATUS.md` |
| 4 | P0 | UI workflow CAP | ✅ Validé | `LOT_4_STATUS.md` |
| 5 | P0 | Espace bénéficiaire sécurisé | ✅ Validé | `LOT_5_STATUS.md` |
| 6 | P0 | Questionnaires en ligne | ✅ Validé | `LOT_6_STATUS.md` |
| 7 | P1 | Analyse structurée SaaS | ✅ Validé | `LOT_7_STATUS.md` |
| 8 | P1 | Synthèse éditable | 🟡 Implémenté, CI à vérifier | `LOT_8_STATUS.md` |
| 9 | P1 | Plan d'action | ⏳ À faire | — |
| 10 | P1 | Exports livrables | ⏳ À faire | — |
| 11 | P2 | Configuration production | ⏳ À faire | — |
| 12 | P2 | Observabilité minimale | ⏳ À faire | — |
| 13 | P2 | Audit et sécurité minimale | ⏳ À faire | — |

## Lots livrés

### Lots 0 à 5

Cadrage, navigation, authentification consultant, workflow CAP, interface et espace bénéficiaire sécurisé sont intégrés dans `main` avec CI verte.

### Lot 6 - Questionnaires en ligne

Catalogue, formulaire bénéficiaire, brouillon, soumission, validation et progression sont disponibles. Le stockage reste en mémoire serveur.

### Lot 7 - Analyse structurée SaaS

Analyse déterministe, indicateurs, mots-clés et restitution sont disponibles. Le snapshot n'est pas persisté durablement.

### Lot 8 - Synthèse éditable

Livré sur la branche du lot :

- brouillon initial généré depuis l'analyse ;
- lecture et sauvegarde par API consultant ;
- éditeur Blazor ;
- validation humaine horodatée ;
- identification du consultant validateur ;
- verrouillage après validation ;
- isolation tenant/bénéficiaire ;
- tests dédiés.

Limite : stockage mémoire, sans historique ni migration EF Core.

## Prochains lots

### Lot 9 - Plan d'action

User story : `US-31-LIV-002`.

Objectifs : modéliser les actions, échéances et statuts, fournir API/UI et rattacher le plan à la session.

### Lot 10 - Exports livrables

User story : `US-31-LIV-003`.

Objectifs : produire un export exploitable, contrôler les sections obligatoires et garantir l'isolation tenant.

### Lot 11 - Configuration production

User story : `US-31-PRD-001`.

Objectifs : PostgreSQL de référence, migrations EF Core de tous les agrégats, secrets, profils d'environnement et documentation de déploiement. Aspire reste réservé au développement.

### Lot 12 - Observabilité minimale

User story : `US-31-PRD-002`.

Objectifs : logs API, diagnostics, erreurs UI compréhensibles et absence de données sensibles dans les journaux.

### Lot 13 - Audit et sécurité minimale

User stories : `US-31-PRD-003`, `US-31-BEN-001`, `US-31-BEN-002`.

Objectifs : audit des actions sensibles, tests inter-tenant, protection des modifications et identification RGPD des données.

## Cible de release

```text
Lots requis = validés
Questionnaires, analyses, synthèses et plans = persistés durablement
Exports = opérationnels
Configuration production = documentée et testée
CI main = verte
Checklist production-ready = validée
Aucun bug bloquant connu = oui
```

Le Lot 8 complète la chaîne jusqu'à la synthèse éditable, mais la release n'est pas encore production-ready.
