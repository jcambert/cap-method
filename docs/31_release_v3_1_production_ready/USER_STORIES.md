# User Stories - v3.1-saas-production-ready

## Personas

```text
Consultant
  Professionnel qui pilote le bilan de compétences.

Bénéficiaire
  Personne accompagnée dans le bilan de compétences.

Administrateur
  Responsable technique ou fonctionnel de l'instance SaaS.

Système
  Application CAP Method SaaS.
```

---

# Epic 1 - Authentification production

## US-31-AUTH-001 - Connexion consultant

En tant que consultant, je veux me connecter avec un compte sécurisé afin d'accéder à mon espace professionnel.

Critères d'acceptation :

- la connexion ne dépend pas du token de développement ;
- l'utilisateur authentifié est identifié côté serveur ;
- le tenant est résolu côté serveur ;
- les endpoints métier restent protégés ;
- les erreurs d'authentification sont compréhensibles côté UI.

## US-31-AUTH-002 - Déconnexion

En tant que consultant, je veux me déconnecter afin de fermer ma session applicative.

Critères d'acceptation :

- le token local est supprimé ;
- l'UI revient à l'écran de connexion ;
- les appels API protégés ne passent plus après déconnexion.

## US-31-AUTH-003 - Accès bénéficiaire sécurisé

En tant que bénéficiaire, je veux accéder uniquement à ma session afin de répondre aux questionnaires sans voir les données d'autres bénéficiaires.

Critères d'acceptation :

- l'accès est limité à une session donnée ;
- aucun identifiant tenant n'est saisi côté UI ;
- l'accès à une autre session est refusé ;
- les réponses sont rattachées à la bonne session.

---

# Epic 2 - Navigation SaaS

## US-31-NAV-001 - Layout applicatif

En tant qu'utilisateur connecté, je veux une navigation claire afin d'accéder rapidement aux fonctions principales.

Critères d'acceptation :

- une structure de pages remplace l'écran unique ;
- les routes principales sont protégées ;
- la navigation affiche les sections disponibles selon le rôle ;
- l'expérience reste compatible Blazor WebAssembly hosted.

## US-31-NAV-002 - Tableau de bord consultant

En tant que consultant, je veux voir mes sessions en cours afin de savoir où agir en priorité.

Critères d'acceptation :

- le tableau de bord liste les sessions actives ;
- chaque session affiche son statut et son avancement ;
- un accès rapide permet d'ouvrir le détail ;
- les données sont filtrées par tenant côté serveur.

---

# Epic 3 - Gestion bénéficiaires

## US-31-BEN-001 - Fiche bénéficiaire

En tant que consultant, je veux consulter la fiche d'un bénéficiaire afin de suivre son accompagnement.

Critères d'acceptation :

- la fiche affiche les informations principales ;
- seules les données du tenant courant sont accessibles ;
- les sessions liées sont visibles ;
- les champs sensibles sont préparés pour une gestion RGPD.

## US-31-BEN-002 - Mise à jour bénéficiaire

En tant que consultant, je veux modifier les informations d'un bénéficiaire afin de corriger ou compléter son dossier.

Critères d'acceptation :

- la modification passe par une API protégée ;
- les données sont validées ;
- un audit minimal est prévu ;
- le bénéficiaire d'un autre tenant ne peut pas être modifié.

---

# Epic 4 - Workflow CAP

## US-31-WF-001 - Étapes d'une session CAP

En tant que consultant, je veux suivre une session CAP par étapes afin de piloter le bilan de manière structurée.

Critères d'acceptation :

- une session possède des étapes métier ;
- chaque étape a un statut ;
- le passage d'étape est contrôlé ;
- l'avancement global est calculé.

## US-31-WF-002 - Démarrage de session

En tant que consultant, je veux démarrer une session CAP afin d'ouvrir le parcours au bénéficiaire.

Critères d'acceptation :

- une session en brouillon peut être démarrée ;
- le statut change de manière explicite ;
- l'accès bénéficiaire peut être préparé ;
- les transitions invalides sont refusées.

## US-31-WF-003 - Clôture de session

En tant que consultant, je veux clôturer une session afin de figer le bilan terminé.

Critères d'acceptation :

- la clôture n'est possible que si les livrables obligatoires sont prêts ;
- les réponses restent consultables ;
- les livrables restent exportables ;
- la clôture est tracée.

---

# Epic 5 - Questionnaires et réponses

## US-31-QST-001 - Questionnaires en ligne

En tant que bénéficiaire, je veux répondre aux questionnaires en ligne afin de compléter mon bilan sans utiliser de fichiers externes.

Critères d'acceptation :

- un questionnaire est affiché dans l'espace bénéficiaire ;
- les questions sont rattachées à une session ;
- les réponses sont persistées ;
- une reprise ultérieure est possible.

## US-31-QST-002 - Suivi des réponses

En tant que consultant, je veux voir l'avancement des réponses afin de relancer le bénéficiaire si nécessaire.

Critères d'acceptation :

- le nombre de réponses attendues est visible ;
- le nombre de réponses complétées est visible ;
- le statut de chaque questionnaire est affiché ;
- le consultant ne peut voir que son tenant.

---

# Epic 6 - Analyse et livrables

## US-31-ANA-001 - Préparation analyse

En tant que consultant, je veux préparer une analyse structurée depuis les réponses afin de produire une synthèse exploitable.

Critères d'acceptation :

- les réponses sont transformées en snapshot d'analyse ;
- aucune conclusion automatique définitive n'est imposée ;
- le consultant peut relire les éléments ;
- l'analyse conserve les garde-fous métier.

## US-31-LIV-001 - Synthèse éditable

En tant que consultant, je veux éditer une synthèse afin de produire un livrable validé humainement.

Critères d'acceptation :

- une synthèse est créée à partir de l'analyse ;
- le contenu reste modifiable ;
- les validations humaines sont explicites ;
- les versions peuvent être tracées ultérieurement.

## US-31-LIV-002 - Plan d'action

En tant que consultant, je veux produire un plan d'action afin de formaliser les prochaines étapes du bénéficiaire.

Critères d'acceptation :

- le plan d'action est lié à la session ;
- les actions ont un libellé, une échéance et un statut ;
- le plan est exportable ;
- le consultant reste responsable de la validation.

## US-31-LIV-003 - Export livrables

En tant que consultant, je veux exporter les livrables afin de remettre un dossier au bénéficiaire.

Critères d'acceptation :

- au moins un export PDF ou DOCX est disponible ;
- l'export contient les sections obligatoires ;
- le fichier ne contient pas de données d'un autre tenant ;
- l'export peut être relancé.

---

# Epic 7 - Production readiness

## US-31-PRD-001 - Configuration production

En tant qu'administrateur, je veux une configuration production documentée afin de déployer l'application sans réglages implicites.

Critères d'acceptation :

- les variables de configuration sont listées ;
- les secrets ne sont pas versionnés ;
- PostgreSQL est le mode de persistance de référence ;
- le mode InMemory est explicitement réservé au local/dev.

## US-31-PRD-002 - Observabilité minimale

En tant qu'administrateur, je veux disposer de logs et de diagnostics afin de comprendre les erreurs de production.

Critères d'acceptation :

- les erreurs API sont journalisées ;
- les informations sensibles ne sont pas exposées ;
- les erreurs UI affichent un message utilisateur ;
- la stratégie de logs est documentée.

## US-31-PRD-003 - Sécurité minimale

En tant qu'administrateur, je veux une base de sécurité production afin de limiter les risques de fuite ou d'accès indu.

Critères d'acceptation :

- les endpoints métier sont protégés ;
- les accès inter-tenant sont testés ;
- les tokens dev ne sont pas utilisés en production ;
- les données sensibles sont identifiées.
