# User stories - CAP Method v3.0-saas

## Objectif du document

Ce document sert de backlog fonctionnel pour le jalon `v3.0-saas`.

Il permet :

- de valider le périmètre SaaS avant développement ;
- de conserver une vision produit cohérente ;
- de suivre l'avancement futur du SaaS ;
- de distinguer les besoins consultant, bénéficiaire, administrateur et système ;
- de garder la cohérence avec `v1.0-pro` et `v2.0-ai`.

## Statuts de suivi

```text
TODO        = à faire
READY       = prêt à développer
IN_PROGRESS = en cours
DONE        = terminé
OUT_OF_SCOPE = exclu du jalon
```

## Règle produit principale

```text
Le SaaS orchestre le parcours.
Le moteur CAP produit les livrables.
L'IA assiste si elle est activée.
Le consultant reste responsable de la validation.
```

## Dépendances produit

```text
v1.0-pro
  = socle moteur stable

v2.0-ai
  = analyse IA assistée optionnelle

v3.0-saas
  = plateforme web autour du moteur CAP
```

Le développement réel du SaaS ne doit pas démarrer avant validation suffisante de l'usage terrain et du périmètre IA.

---

# Epic 1 - Socle SaaS et comptes utilisateurs

## US-SAAS-001 - Créer un compte consultant

**En tant que** consultant,  
**je veux** créer un compte utilisateur,  
**afin de** accéder à mon espace CAP Method.

### Critères d'acceptation

- Un consultant peut créer un compte.
- Un consultant peut se connecter.
- Les informations minimales sont collectées.
- Le compte est rattaché à un tenant ou espace professionnel.

### Statut

```text
TODO
```

---

## US-SAAS-002 - Se connecter et se déconnecter

**En tant que** utilisateur,  
**je veux** pouvoir me connecter et me déconnecter,  
**afin de** sécuriser l'accès à mes données.

### Critères d'acceptation

- L'utilisateur peut se connecter.
- L'utilisateur peut se déconnecter.
- Les routes sensibles nécessitent une authentification.
- Une session expirée oblige à se reconnecter.

### Statut

```text
TODO
```

---

## US-SAAS-003 - Gérer les rôles

**En tant qu'** administrateur,  
**je veux** gérer les rôles des utilisateurs,  
**afin de** contrôler les accès aux fonctionnalités.

### Rôles cibles

```text
Admin
Consultant
Beneficiary
```

### Critères d'acceptation

- Les rôles sont définis.
- Les permissions sont documentées.
- Un bénéficiaire ne peut pas accéder aux dossiers d'autres bénéficiaires.
- Un consultant ne peut pas accéder à un autre tenant.

### Statut

```text
TODO
```

---

# Epic 2 - Multi-tenant et isolation des données

## US-SAAS-004 - Créer un espace professionnel

**En tant que** consultant,  
**je veux** disposer d'un espace professionnel,  
**afin de** gérer mes bénéficiaires dans un environnement isolé.

### Critères d'acceptation

- Un tenant peut être créé.
- Un consultant est rattaché à un tenant.
- Les données métier sont rattachées à un tenant.
- Le tenant est visible dans l'administration.

### Statut

```text
TODO
```

---

## US-SAAS-005 - Isoler les données par tenant

**En tant que** responsable sécurité,  
**je veux** que les données soient isolées par tenant,  
**afin de** garantir la confidentialité des parcours CAP.

### Critères d'acceptation

- Chaque bénéficiaire appartient à un tenant.
- Chaque session CAP appartient à un tenant.
- Les requêtes filtrent par tenant.
- Un test vérifie qu'un tenant ne voit pas les données d'un autre.

### Statut

```text
TODO
```

---

# Epic 3 - Gestion des bénéficiaires

## US-SAAS-006 - Créer un bénéficiaire

**En tant que** consultant,  
**je veux** créer un bénéficiaire,  
**afin de** lancer un parcours CAP.

### Critères d'acceptation

- Le consultant peut saisir les informations minimales du bénéficiaire.
- Le bénéficiaire est rattaché au tenant du consultant.
- Le bénéficiaire possède un statut actif/inactif.
- La création est auditée.

### Statut

```text
TODO
```

---

## US-SAAS-007 - Consulter la fiche bénéficiaire

**En tant que** consultant,  
**je veux** consulter la fiche d'un bénéficiaire,  
**afin de** suivre son parcours CAP.

### Critères d'acceptation

- La fiche affiche les informations principales.
- La fiche affiche les sessions CAP associées.
- La fiche affiche le statut des questionnaires.
- La fiche affiche les derniers livrables disponibles.

### Statut

```text
TODO
```

---

## US-SAAS-008 - Archiver un bénéficiaire

**En tant que** consultant,  
**je veux** archiver un bénéficiaire,  
**afin de** conserver l'historique sans encombrer mon espace actif.

### Critères d'acceptation

- Un bénéficiaire peut être archivé.
- Les données ne sont pas supprimées automatiquement.
- Les dossiers archivés restent consultables selon les droits.
- L'action est auditée.

### Statut

```text
TODO
```

---

# Epic 4 - Sessions CAP

## US-SAAS-009 - Créer une session CAP

**En tant que** consultant,  
**je veux** créer une session CAP pour un bénéficiaire,  
**afin de** lancer un parcours de bilan.

### Critères d'acceptation

- Une session CAP est créée pour un bénéficiaire.
- La session possède un statut initial `Draft`.
- La session est rattachée au consultant et au tenant.
- La session possède un identifiant unique.

### Statut

```text
TODO
```

---

## US-SAAS-010 - Suivre le statut d'une session CAP

**En tant que** consultant,  
**je veux** suivre l'avancement d'une session CAP,  
**afin de** savoir où en est le bénéficiaire.

### Statuts cibles

```text
Draft
QuestionnairesSent
InProgress
ResponsesCompleted
AnalysisGenerated
ConsultantReview
BeneficiaryReview
Validated
Delivered
Archived
```

### Critères d'acceptation

- Le statut courant est visible.
- Les transitions principales sont documentées.
- Le statut change automatiquement après certaines actions.
- Les transitions sensibles sont auditables.

### Statut

```text
TODO
```

---

# Epic 5 - Questionnaires en ligne

## US-SAAS-011 - Afficher les questionnaires depuis CMDL

**En tant que** bénéficiaire,  
**je veux** remplir les questionnaires directement dans l'application,  
**afin de** ne plus dépendre de Google Forms.

### Critères d'acceptation

- Les définitions CMDL peuvent être rendues en interface web.
- Les types de questions principaux sont supportés.
- Les questions sont affichées dans l'ordre attendu.
- Le rendu respecte les modules CAP.

### Statut

```text
TODO
```

---

## US-SAAS-012 - Envoyer une invitation bénéficiaire

**En tant que** consultant,  
**je veux** envoyer une invitation au bénéficiaire,  
**afin de** lui donner accès à ses questionnaires.

### Critères d'acceptation

- Le consultant peut déclencher une invitation.
- Le bénéficiaire reçoit un lien sécurisé.
- Le lien est rattaché à une session CAP.
- L'envoi est journalisé.

### Statut

```text
TODO
```

---

## US-SAAS-013 - Sauvegarder les réponses progressivement

**En tant que** bénéficiaire,  
**je veux** sauvegarder mes réponses au fil de l'eau,  
**afin de** reprendre plus tard sans perdre mon travail.

### Critères d'acceptation

- Les réponses sont enregistrées progressivement.
- Le bénéficiaire peut quitter puis reprendre.
- Les réponses sont rattachées à la session CAP.
- Le consultant peut voir l'état d'avancement sans lire inutilement les réponses en cours.

### Statut

```text
TODO
```

---

## US-SAAS-014 - Soumettre un formulaire terminé

**En tant que** bénéficiaire,  
**je veux** soumettre un formulaire terminé,  
**afin de** signaler au consultant que mes réponses sont prêtes.

### Critères d'acceptation

- Un formulaire peut être soumis.
- Les réponses soumises sont horodatées.
- Le statut de progression est mis à jour.
- Le consultant peut voir que le formulaire est terminé.

### Statut

```text
TODO
```

---

# Epic 6 - Analyse et moteur CAP intégré

## US-SAAS-015 - Générer une ResponseSession depuis les réponses en ligne

**En tant que** système,  
**je veux** transformer les réponses en ligne en `ResponseSession`,  
**afin de** réutiliser le moteur CAP existant.

### Critères d'acceptation

- Les réponses web sont converties en structure compatible.
- Le format reste aligné avec `v1.0-pro`.
- La génération est reproductible.
- Les erreurs de données sont explicites.

### Statut

```text
TODO
```

---

## US-SAAS-016 - Générer l'analyse structurée

**En tant que** consultant,  
**je veux** générer l'analyse structurée depuis la session CAP,  
**afin de** préparer ma synthèse.

### Critères d'acceptation

- Le SaaS déclenche la génération `AnalysisSnapshot`.
- Le résultat est rattaché à la session CAP.
- L'analyse est consultable par le consultant.
- Le bénéficiaire ne reçoit pas automatiquement cette analyse brute.

### Statut

```text
TODO
```

---

## US-SAAS-017 - Intégrer l'analyse IA optionnelle

**En tant que** consultant,  
**je veux** activer ou non l'analyse IA,  
**afin de** conserver le contrôle méthodologique.

### Critères d'acceptation

- L'analyse IA est optionnelle.
- Elle produit un `AIAnalysisDraft`.
- Elle respecte les garde-fous `v2.0-ai`.
- Elle ne remplace pas la validation consultant.

### Statut

```text
TODO
```

---

# Epic 7 - Synthèse, revue et plan d'action

## US-SAAS-018 - Générer une synthèse finale éditable

**En tant que** consultant,  
**je veux** générer une synthèse finale éditable,  
**afin de** préparer le livrable bénéficiaire.

### Critères d'acceptation

- Une synthèse est générée depuis l'analyse.
- La synthèse est éditable dans l'interface.
- Les modifications sont sauvegardées.
- La synthèse est marquée comme non validée au départ.

### Statut

```text
TODO
```

---

## US-SAAS-019 - Valider la synthèse consultant

**En tant que** consultant,  
**je veux** valider la synthèse finale,  
**afin de** confirmer qu'elle peut être utilisée en restitution.

### Critères d'acceptation

- La synthèse peut passer en statut validé.
- La validation est horodatée.
- Le consultant validateur est enregistré.
- Une synthèse non validée ne doit pas être livrée comme finale.

### Statut

```text
TODO
```

---

## US-SAAS-020 - Générer un plan d'action éditable

**En tant que** consultant,  
**je veux** générer un plan d'action éditable,  
**afin de** construire une suite opérationnelle au bilan.

### Critères d'acceptation

- Le plan d'action est généré depuis la synthèse validée ou en cours de revue.
- Le consultant peut le modifier.
- Les actions sont structurées par horizon temporel.
- Le plan d'action possède un statut de validation.

### Statut

```text
TODO
```

---

# Epic 8 - Exports et livraison

## US-SAAS-021 - Générer les exports DOCX/PDF

**En tant que** consultant,  
**je veux** générer les exports DOCX et PDF depuis l'interface,  
**afin de** livrer des documents professionnels.

### Critères d'acceptation

- Les exports sont générés depuis les données validées.
- Le DOCX est téléchargeable.
- Le PDF est téléchargeable.
- Les exports sont rattachés à une version de synthèse et de plan d'action.

### Statut

```text
TODO
```

---

## US-SAAS-022 - Générer un ZIP final

**En tant que** consultant,  
**je veux** générer un ZIP final de session,  
**afin de** archiver ou transmettre un dossier complet.

### Critères d'acceptation

- Le ZIP contient les exports.
- Le ZIP contient un manifest.
- Le ZIP respecte la logique `v1.0-pro`.
- La génération est auditée.

### Statut

```text
TODO
```

---

## US-SAAS-023 - Remettre les livrables au bénéficiaire

**En tant que** consultant,  
**je veux** rendre les livrables disponibles au bénéficiaire,  
**afin de** finaliser le parcours CAP.

### Critères d'acceptation

- Seuls les livrables validés peuvent être remis.
- Le bénéficiaire peut télécharger les documents autorisés.
- Le téléchargement est journalisé.
- Les brouillons IA ne sont jamais remis automatiquement.

### Statut

```text
TODO
```

---

# Epic 9 - Notifications et suivi

## US-SAAS-024 - Notifier le bénéficiaire

**En tant que** système,  
**je veux** notifier le bénéficiaire aux étapes clés,  
**afin de** faciliter le suivi du parcours.

### Événements cibles

- invitation ;
- questionnaires à compléter ;
- rappel ;
- livrables disponibles.

### Critères d'acceptation

- Les notifications sont déclenchées sur événements.
- Les messages sont traçables.
- Les notifications peuvent être désactivées ou limitées.
- Aucune donnée sensible inutile n'est exposée dans la notification.

### Statut

```text
TODO
```

---

## US-SAAS-025 - Notifier le consultant

**En tant que** consultant,  
**je veux** être notifié des événements importants,  
**afin de** piloter mes accompagnements.

### Événements cibles

- bénéficiaire invité ;
- questionnaire terminé ;
- réponses complètes ;
- analyse prête ;
- synthèse à valider ;
- livrables générés.

### Critères d'acceptation

- Les notifications sont visibles dans le tableau de bord.
- Les notifications importantes sont priorisées.
- Les notifications peuvent être marquées comme traitées.

### Statut

```text
TODO
```

---

# Epic 10 - Audit, conformité et données personnelles

## US-SAAS-026 - Journaliser les actions sensibles

**En tant que** responsable conformité,  
**je veux** journaliser les actions sensibles,  
**afin de** assurer la traçabilité des parcours.

### Actions à auditer

- création bénéficiaire ;
- invitation ;
- soumission réponse ;
- génération analyse ;
- génération IA ;
- modification synthèse ;
- validation ;
- export ;
- téléchargement ;
- archivage ;
- suppression.

### Critères d'acceptation

- Les actions sensibles sont horodatées.
- L'utilisateur acteur est enregistré.
- La session CAP concernée est enregistrée.
- Les logs d'audit ne contiennent pas inutilement le contenu sensible.

### Statut

```text
TODO
```

---

## US-SAAS-027 - Exporter les données d'un bénéficiaire

**En tant que** consultant ou administrateur autorisé,  
**je veux** exporter les données d'un bénéficiaire,  
**afin de** répondre aux exigences de portabilité et d'archivage.

### Critères d'acceptation

- Les données d'un bénéficiaire peuvent être exportées.
- L'export contient les sessions et livrables associés.
- L'action est auditée.
- L'export respecte les droits d'accès.

### Statut

```text
TODO
```

---

## US-SAAS-028 - Supprimer ou anonymiser les données

**En tant que** administrateur autorisé,  
**je veux** supprimer ou anonymiser certaines données,  
**afin de** respecter les obligations de conservation et de protection des données.

### Critères d'acceptation

- Une procédure de suppression existe.
- Une procédure d'anonymisation existe.
- Les impacts sont documentés.
- L'action est auditée.

### Statut

```text
TODO
```

---

# Epic 11 - Administration et exploitation

## US-SAAS-029 - Disposer d'un tableau de bord consultant

**En tant que** consultant,  
**je veux** disposer d'un tableau de bord,  
**afin de** voir mes bénéficiaires et sessions en cours.

### Critères d'acceptation

- Le tableau de bord affiche les sessions actives.
- Le tableau de bord affiche les statuts principaux.
- Les actions prioritaires sont visibles.
- Les notifications récentes sont visibles.

### Statut

```text
TODO
```

---

## US-SAAS-030 - Superviser l'application en production

**En tant que** exploitant,  
**je veux** disposer de logs, supervision et sauvegardes,  
**afin de** exploiter le SaaS de manière fiable.

### Critères d'acceptation

- Les erreurs applicatives sont loggées.
- Les métriques minimales sont disponibles.
- Une stratégie de sauvegarde est documentée.
- Une procédure de restauration est documentée.

### Statut

```text
TODO
```

---

# Epic 12 - Suivi d'avancement

## US-SAAS-031 - Suivre l'avancement des user stories SaaS

**En tant que** mainteneur,  
**je veux** utiliser ce document comme suivi d'avancement,  
**afin de** piloter le jalon `v3.0-saas` sans perdre le périmètre.

### Critères d'acceptation

- Chaque user story possède un statut.
- Les statuts sont mis à jour à chaque étape.
- Les user stories terminées passent à `DONE`.
- Les éléments exclus passent à `OUT_OF_SCOPE`.

### Statut

```text
TODO
```

---

# Priorisation recommandée

## Lot 1 - Cadrage SaaS foundation

```text
US-SAAS-001
US-SAAS-002
US-SAAS-003
US-SAAS-004
US-SAAS-005
US-SAAS-006
US-SAAS-009
US-SAAS-010
```

## Lot 2 - Parcours bénéficiaire et questionnaires

```text
US-SAAS-011
US-SAAS-012
US-SAAS-013
US-SAAS-014
US-SAAS-015
```

## Lot 3 - Moteur CAP intégré

```text
US-SAAS-016
US-SAAS-018
US-SAAS-019
US-SAAS-020
```

## Lot 4 - IA optionnelle dans le SaaS

```text
US-SAAS-017
```

Ce lot dépend directement de `v2.0-ai`.

## Lot 5 - Exports, livraison et archivage

```text
US-SAAS-021
US-SAAS-022
US-SAAS-023
US-SAAS-027
```

## Lot 6 - Notifications, audit et exploitation

```text
US-SAAS-024
US-SAAS-025
US-SAAS-026
US-SAAS-028
US-SAAS-029
US-SAAS-030
US-SAAS-031
```

## Décision

Ce document doit être validé avant le développement de `feature/v3-saas` ou `product/v3-saas-foundation`.

Le développement SaaS doit démarrer après :

1. validation terrain de `v1.0-pro` ;
2. cadrage ou stabilisation de `v2.0-ai` ;
3. validation du MVP SaaS minimal.
