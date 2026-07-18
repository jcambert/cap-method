# Backlog - v3.1-saas-production-ready

## Objectif de découpage

La v3.1 est découpée en lots courts, chacun fusionnable par PR indépendante.

Règle :

```text
1 lot = 1 branche feature
1 branche = 1 PR
1 PR = squash merge
CI verte obligatoire
```

---

# Priorité P0 - indispensable production-ready

## Lot 0 - Cadrage v3.1 production-ready

Statut :

```text
IN PROGRESS
```

Contenu :

- vision v3.1 ;
- user stories ;
- backlog priorisé ;
- critères production-ready ;
- CI documentaire v3.1.

Livrables :

```text
docs/31_release_v3_1_production_ready/README.md
docs/31_release_v3_1_production_ready/VISION.md
docs/31_release_v3_1_production_ready/USER_STORIES.md
docs/31_release_v3_1_production_ready/BACKLOG.md
docs/31_release_v3_1_production_ready/PRODUCTION_READINESS.md
docs/31_release_v3_1_production_ready/LOT_0_STATUS.md
```

---

## Lot 1 - Navigation SaaS par pages

Objectif :

Remplacer l'écran unique v3.0 par une navigation applicative exploitable.

User stories couvertes :

```text
US-31-NAV-001
US-31-NAV-002
```

Tâches :

- créer un layout applicatif ;
- créer les routes principales ;
- protéger les pages métier ;
- séparer dashboard, bénéficiaires, sessions, paramètres ;
- conserver les appels API existants ;
- ajouter tests de build et documentation.

Critères de sortie :

- l'application n'est plus un écran unique ;
- le consultant dispose d'un tableau de bord minimal ;
- les pages protégées redirigent si non connecté ;
- CI verte.

---

## Lot 2 - Authentification production minimale

Objectif :

Sortir du fonctionnement centré sur le token de développement.

User stories couvertes :

```text
US-31-AUTH-001
US-31-AUTH-002
US-31-PRD-003
```

Tâches :

- introduire un mode d'authentification production minimal ;
- désactiver clairement les tokens dev hors environnement Development ;
- documenter les secrets et variables ;
- renforcer les erreurs d'authentification ;
- tester les endpoints protégés.

Critères de sortie :

- le token dev n'est pas utilisable en production ;
- la configuration production est explicite ;
- les endpoints métier restent protégés ;
- CI verte.

---

## Lot 3 - Modèle de workflow CAP

Objectif :

Ajouter les étapes métier d'une session CAP.

User stories couvertes :

```text
US-31-WF-001
US-31-WF-002
US-31-WF-003
```

Tâches :

- modéliser les étapes de session ;
- ajouter les statuts d'étape ;
- contrôler les transitions ;
- calculer l'avancement ;
- exposer les informations côté API ;
- ajouter tests domaine/application/infrastructure.

Critères de sortie :

- une session peut être démarrée ;
- l'avancement est visible ;
- les transitions invalides sont refusées ;
- CI verte.

---

## Lot 4 - UI workflow CAP

Objectif :

Afficher et piloter le workflow CAP depuis l'interface consultant.

User stories couvertes :

```text
US-31-WF-001
US-31-WF-002
US-31-WF-003
US-31-NAV-002
```

Tâches :

- afficher les étapes d'une session ;
- afficher l'avancement ;
- ajouter actions démarrer / avancer / clôturer selon règles ;
- afficher les erreurs métier ;
- documenter le comportement.

Critères de sortie :

- le consultant peut piloter une session depuis l'UI ;
- les actions indisponibles sont bloquées ou masquées ;
- CI verte.

---

## Lot 5 - Espace bénéficiaire sécurisé

Objectif :

Préparer un accès bénéficiaire limité à sa session.

User stories couvertes :

```text
US-31-AUTH-003
US-31-QST-001
```

Tâches :

- créer les routes bénéficiaire ;
- limiter l'accès à une session ;
- éviter toute exposition inter-tenant ;
- préparer l'affichage des questionnaires ;
- ajouter tests d'isolation.

Critères de sortie :

- un bénéficiaire ne voit qu'une session autorisée ;
- les données d'un autre bénéficiaire sont inaccessibles ;
- CI verte.

---

## Lot 6 - Questionnaires en ligne

Objectif :

Intégrer les questionnaires dans le SaaS.

User stories couvertes :

```text
US-31-QST-001
US-31-QST-002
```

Tâches :

- définir le modèle questionnaire ;
- définir le modèle réponse ;
- stocker les réponses ;
- permettre la reprise ;
- afficher l'avancement ;
- ajouter migrations EF Core.

Critères de sortie :

- le bénéficiaire peut répondre en ligne ;
- les réponses sont persistées ;
- le consultant voit l'avancement ;
- CI verte.

---

# Priorité P1 - nécessaire pour exploitation métier

## Lot 7 - Analyse structurée SaaS

Objectif :

Créer un snapshot d'analyse à partir des réponses SaaS.

User stories couvertes :

```text
US-31-ANA-001
```

Tâches :

- convertir les réponses en snapshot ;
- conserver les garde-fous métier ;
- préparer la relecture consultant ;
- tester la non-régression CAP v1/v2.

---

## Lot 8 - Synthèse éditable

Objectif :

Créer et modifier une synthèse depuis l'application.

User stories couvertes :

```text
US-31-LIV-001
```

Tâches :

- créer le modèle de synthèse ;
- exposer API lecture/écriture ;
- créer UI d'édition ;
- tracer la validation humaine.

---

## Lot 9 - Plan d'action

Objectif :

Créer un plan d'action exploitable.

User stories couvertes :

```text
US-31-LIV-002
```

Tâches :

- modéliser les actions ;
- créer API et UI ;
- rattacher le plan à la session ;
- préparer l'export.

---

## Lot 10 - Exports livrables

Objectif :

Exporter les livrables depuis le SaaS.

User stories couvertes :

```text
US-31-LIV-003
```

Tâches :

- générer un export minimal ;
- valider les sections obligatoires ;
- éviter les données inter-tenant ;
- documenter les limites.

---

# Priorité P2 - durcissement production

## Lot 11 - Configuration production

User stories couvertes :

```text
US-31-PRD-001
```

Contenu :

- configuration PostgreSQL de référence ;
- gestion secrets ;
- profils d'environnement ;
- documentation déploiement.

## Lot 12 - Observabilité minimale

User stories couvertes :

```text
US-31-PRD-002
```

Contenu :

- logs API ;
- erreurs UI ;
- diagnostics ;
- non-exposition des données sensibles.

## Lot 13 - Audit et sécurité minimale

User stories couvertes :

```text
US-31-PRD-003
US-31-BEN-001
US-31-BEN-002
```

Contenu :

- audit minimal des actions sensibles ;
- tests inter-tenant ;
- protection des modifications ;
- identification RGPD des données sensibles.

---

# Cible de release

La release `v3.1-saas-production-ready` pourra être considérée prête quand les lots P0 et P1 seront intégrés, et que les critères production-ready minimum seront validés.

Les lots P2 peuvent être intégrés dans v3.1 ou réservés à v3.1.1 selon l'état de la stabilisation.
