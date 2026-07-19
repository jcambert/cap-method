# User Stories - v3.1-saas-production-ready

## Légende

```text
✅ VALIDÉ = fonctionnalité intégrée dans main avec CI verte
🟡 PARTIEL = fonctionnalité disponible mais critère production incomplet
🔵 IMPLÉMENTÉ = livré sur la branche courante, CI à vérifier
⏳ À FAIRE = non livrée
```

## Vue consolidée

| User story | Objet | État | Lot |
|---|---|---|---|
| US-31-AUTH-001 | Connexion consultant | ✅ Validé | 2 |
| US-31-AUTH-002 | Déconnexion | ✅ Validé | 2 |
| US-31-AUTH-003 | Accès bénéficiaire sécurisé | ✅ Validé | 5 |
| US-31-NAV-001 | Layout applicatif | ✅ Validé | 1 |
| US-31-NAV-002 | Tableau de bord consultant | ✅ Validé | 1 et 4 |
| US-31-BEN-001 | Fiche bénéficiaire complète | 🟡 Partiel | Base existante, durcissement Lot 13 |
| US-31-BEN-002 | Mise à jour bénéficiaire | ⏳ À faire | 13 |
| US-31-WF-001 | Étapes d'une session CAP | ✅ Validé | 3 et 4 |
| US-31-WF-002 | Démarrage de session | ✅ Validé | 3 et 4 |
| US-31-WF-003 | Clôture de session | 🟡 Partiel | Plan et exports absents |
| US-31-QST-001 | Questionnaires en ligne | 🟡 Partiel | Fonctionnel, persistance durable absente |
| US-31-QST-002 | Suivi des réponses | ✅ Validé fonctionnellement | 6 |
| US-31-ANA-001 | Préparation analyse | ✅ Validé fonctionnellement | 7 |
| US-31-LIV-001 | Synthèse éditable | 🔵 Implémenté | 8 |
| US-31-LIV-002 | Plan d'action | ⏳ À faire | 9 |
| US-31-LIV-003 | Export livrables | ⏳ À faire | 10 |
| US-31-PRD-001 | Configuration production | ⏳ À faire | 11 |
| US-31-PRD-002 | Observabilité minimale | ⏳ À faire | 12 |
| US-31-PRD-003 | Sécurité minimale consolidée | 🟡 Partiel | Finalisation Lot 13 |

## Personas

- **Consultant** : pilote le bilan, édite et valide les livrables.
- **Bénéficiaire** : répond aux questionnaires et suit son parcours.
- **Administrateur** : configure et exploite l'instance SaaS.
- **Système** : structure les données sans remplacer la validation humaine.

## Fonctionnalités validées

Les Lots 1 à 7 couvrent l'authentification, la navigation, le workflow, l'espace bénéficiaire, les questionnaires et l'analyse structurée. Les limites de persistance durable restent suivies dans `PRODUCTION_READINESS.md`.

## Epic Analyse et livrables

### US-31-ANA-001 - Préparation analyse — ✅ VALIDÉ FONCTIONNELLEMENT

Les questionnaires soumis alimentent une analyse déterministe avec complétude, profondeur, diversité et mots-clés. Les brouillons sont exclus. Le snapshot durable reste à réaliser.

### US-31-LIV-001 - Synthèse éditable — 🔵 IMPLÉMENTÉ

Le consultant peut :

- charger un brouillon initial produit depuis l'analyse ;
- modifier le contenu ;
- enregistrer un brouillon ;
- valider humainement la synthèse ;
- consulter la date et l'identité du validateur.

Après validation, le contenu est figé. L'isolation tenant/bénéficiaire est appliquée côté serveur.

Limites production : stockage mémoire, absence d'historique et de migration EF Core.

### US-31-LIV-002 - Plan d'action — ⏳ À FAIRE

Le plan doit être lié à la session et contenir actions, échéances et statuts.

### US-31-LIV-003 - Export livrables — ⏳ À FAIRE

Le SaaS doit fournir un export exploitable, reproductible et isolé par tenant.

## Production readiness

- Configuration PostgreSQL complète : Lot 11.
- Observabilité minimale : Lot 12.
- Audit, sécurité consolidée et RGPD : Lot 13.
- Aspire reste un outil de développement et n'est pas une dépendance de production.
