# User Stories - v3.1-saas-production-ready

## Légende

```text
✅ VALIDÉ = fonctionnalité intégrée dans main avec CI verte
🟡 PARTIEL = fonctionnalité disponible mais critère production incomplet
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
| US-31-WF-003 | Clôture de session | 🟡 Partiel | Workflow livré, livrables requis absents |
| US-31-QST-001 | Questionnaires en ligne | 🟡 Partiel | Fonctionnel Lot 6, persistance durable absente |
| US-31-QST-002 | Suivi des réponses | ✅ Validé fonctionnellement | 6 |
| US-31-ANA-001 | Préparation analyse | ✅ Validé fonctionnellement | 7 |
| US-31-LIV-001 | Synthèse éditable | ⏳ À faire | 8 |
| US-31-LIV-002 | Plan d'action | ⏳ À faire | 9 |
| US-31-LIV-003 | Export livrables | ⏳ À faire | 10 |
| US-31-PRD-001 | Configuration production | ⏳ À faire | 11 |
| US-31-PRD-002 | Observabilité minimale | ⏳ À faire | 12 |
| US-31-PRD-003 | Sécurité minimale consolidée | 🟡 Partiel | Base Lots 2/5/7, finalisation Lot 13 |

## Personas

- **Consultant** : pilote le bilan de compétences.
- **Bénéficiaire** : répond aux questionnaires et suit son parcours.
- **Administrateur** : configure et exploite l'instance SaaS.
- **Système** : structure les données sans remplacer la validation humaine.

## Epic 1 - Authentification

### US-31-AUTH-001 - Connexion consultant — ✅ VALIDÉ

Le consultant se connecte sans dépendre du token de développement. L'utilisateur et le tenant sont résolus côté serveur et les endpoints métier restent protégés.

### US-31-AUTH-002 - Déconnexion — ✅ VALIDÉ

Le token local est supprimé et les appels protégés ne sont plus possibles après déconnexion.

### US-31-AUTH-003 - Accès bénéficiaire sécurisé — ✅ VALIDÉ

Le bénéficiaire utilise un token dédié contenant son identifiant. Le tenant et le bénéficiaire sont résolus depuis le JWT ; aucune sélection de tenant n'est acceptée côté UI.

## Epic 2 - Navigation SaaS

### US-31-NAV-001 - Layout applicatif — ✅ VALIDÉ

La navigation par pages remplace l'écran unique et reste compatible avec Blazor WebAssembly hosted.

### US-31-NAV-002 - Tableau de bord consultant — ✅ VALIDÉ

Le consultant accède aux sessions, à leur statut et au workflow depuis l'interface.

## Epic 3 - Gestion bénéficiaires

### US-31-BEN-001 - Fiche bénéficiaire — 🟡 PARTIEL

La création et le rattachement aux sessions existent. La fiche complète, l'identification RGPD et l'audit restent à consolider.

### US-31-BEN-002 - Mise à jour bénéficiaire — ⏳ À FAIRE

La modification protégée, validée et auditée reste au backlog.

## Epic 4 - Workflow CAP

### US-31-WF-001 - Étapes d'une session — ✅ VALIDÉ

Les étapes, statuts, transitions et calculs d'avancement sont disponibles.

### US-31-WF-002 - Démarrage de session — ✅ VALIDÉ

Les transitions de démarrage sont contrôlées et pilotables depuis l'UI.

### US-31-WF-003 - Clôture de session — 🟡 PARTIEL

Le workflow sait progresser, mais la clôture complète dépend encore de la synthèse, du plan d'action et des exports.

## Epic 5 - Questionnaires et réponses

### US-31-QST-001 - Questionnaires en ligne — 🟡 PARTIEL

Le bénéficiaire peut charger un questionnaire, enregistrer un brouillon, reprendre et soumettre ses réponses. Le stockage est actuellement en mémoire serveur : la persistance PostgreSQL reste obligatoire avant production.

### US-31-QST-002 - Suivi des réponses — ✅ VALIDÉ FONCTIONNELLEMENT

La progression et le statut brouillon/soumis sont exposés. L'isolation tenant/bénéficiaire est appliquée côté serveur.

## Epic 6 - Analyse et livrables

### US-31-ANA-001 - Préparation analyse — ✅ VALIDÉ FONCTIONNELLEMENT

Les questionnaires soumis alimentent une analyse structurée déterministe : complétude, profondeur, diversité et mots-clés. Les brouillons sont exclus et aucune conclusion définitive n'est imposée.

Limite : l'analyse n'est pas encore persistée comme snapshot durable.

### US-31-LIV-001 - Synthèse éditable — ⏳ À FAIRE

Le consultant doit pouvoir générer, modifier et valider humainement une synthèse issue de l'analyse.

### US-31-LIV-002 - Plan d'action — ⏳ À FAIRE

Le plan doit être lié à la session et contenir actions, échéances et statuts.

### US-31-LIV-003 - Export livrables — ⏳ À FAIRE

Le SaaS doit fournir au moins un export exploitable, reproductible et isolé par tenant.

## Epic 7 - Production readiness

### US-31-PRD-001 - Configuration production — ⏳ À FAIRE

PostgreSQL doit devenir la référence de tous les agrégats v3.1, avec migrations, secrets et profils documentés. Aspire reste réservé au développement.

### US-31-PRD-002 - Observabilité minimale — ⏳ À FAIRE

Logs API, diagnostics, messages UI et protection des données sensibles restent à finaliser.

### US-31-PRD-003 - Sécurité minimale — 🟡 PARTIEL

Les endpoints, JWT et accès inter-tenant disposent déjà d'une base solide. L'audit, les tests consolidés et l'identification des données sensibles restent au Lot 13.