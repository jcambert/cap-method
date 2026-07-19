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
| 8 | P1 | Synthèse éditable | ⏳ À faire | — |
| 9 | P1 | Plan d'action | ⏳ À faire | — |
| 10 | P1 | Exports livrables | ⏳ À faire | — |
| 11 | P2 | Configuration production | ⏳ À faire | — |
| 12 | P2 | Observabilité minimale | ⏳ À faire | — |
| 13 | P2 | Audit et sécurité minimale | ⏳ À faire | — |

## Lots validés

### Lots 0 à 5

Le cadrage, la navigation, l'authentification consultant, le workflow CAP, son interface et l'espace bénéficiaire sécurisé sont intégrés dans `main` avec CI verte.

### Lot 6 - Questionnaires en ligne

Livré :

- catalogue initial ;
- formulaire bénéficiaire ;
- sauvegarde de brouillon ;
- soumission ;
- validation serveur ;
- progression ;
- isolation tenant et bénéficiaire.

Limite : le stockage des réponses est actuellement en mémoire serveur. La persistance PostgreSQL et les migrations EF Core restent obligatoires avant production.

### Lot 7 - Analyse structurée SaaS

Livré :

- analyse déterministe des questionnaires soumis ;
- scores de complétude, profondeur et diversité ;
- extraction de mots-clés ;
- restitution API et UI ;
- tests d'isolation et de non-régression.

Limite : l'analyse est générée à la demande et n'est pas encore persistée comme snapshot durable.

## Prochains lots

### Lot 8 - Synthèse éditable

User story : `US-31-LIV-001`.

Objectifs :

- créer le modèle de synthèse ;
- générer un brouillon initial depuis l'analyse structurée ;
- exposer les API de lecture et d'écriture ;
- fournir une UI d'édition consultant ;
- tracer la validation humaine ;
- préparer la persistance durable et le versionnement.

Critères de sortie :

- la synthèse est modifiable par le consultant ;
- aucune synthèse n'est considérée finale sans validation humaine ;
- l'isolation tenant est garantie ;
- tests et CI sont verts.

### Lot 9 - Plan d'action

User story : `US-31-LIV-002`.

Objectifs : modéliser les actions, échéances et statuts, fournir API/UI et rattacher le plan à la session.

### Lot 10 - Exports livrables

User story : `US-31-LIV-003`.

Objectifs : produire au minimum un export exploitable, contrôler les sections obligatoires et garantir l'isolation tenant.

### Lot 11 - Configuration production

User story : `US-31-PRD-001`.

Objectifs :

- PostgreSQL comme persistance de référence ;
- migrations EF Core de tous les agrégats v3.1 ;
- secrets et profils d'environnement ;
- documentation de déploiement ;
- conserver Aspire uniquement comme outil de développement.

### Lot 12 - Observabilité minimale

User story : `US-31-PRD-002`.

Objectifs : logs API, diagnostics, erreurs UI compréhensibles et absence de données sensibles dans les journaux.

### Lot 13 - Audit et sécurité minimale

User stories : `US-31-PRD-003`, `US-31-BEN-001`, `US-31-BEN-002`.

Objectifs : audit des actions sensibles, tests inter-tenant, protection des modifications et identification RGPD des données.

## Cible de release

La release peut être taguée uniquement lorsque :

```text
Lots 0 à 13 requis pour le périmètre retenu = validés
Questionnaires et analyses = persistés durablement
Synthèse, plan d'action et exports = opérationnels
Configuration production = documentée et testée
CI main = verte
Checklist production-ready = validée
Aucun bug bloquant connu = oui
```

Les fonctionnalités des Lots 0 à 7 sont intégrées, mais la release n'est pas encore production-ready.