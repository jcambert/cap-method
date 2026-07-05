# LIVRABLE-086
# Guide de création Google Forms CAP

**Version :** 1.0

---

## Objectif

Ce guide explique comment créer les Google Forms CAP à partir des livrables Markdown.

Il permet de transformer les contenus de chaque module en formulaires prêts à envoyer à une participante.

---

## Principe général

Chaque module CAP dispose d'un livrable Google Forms source.

Ces fichiers contiennent :

- le titre du formulaire ;
- l'objectif ;
- les paramètres recommandés ;
- les sections ;
- les questions ;
- le type de champ ;
- le nom de colonne Google Sheets attendu.

---

## Formulaires à créer

| Module | Formulaire | Livrable source |
|---|---|---|
| Module 0 | Accueil et démarrage | LIVRABLE-002 |
| Module 1 | Mon histoire | LIVRABLE-005 |
| Module 2 | Mes valeurs | LIVRABLE-011 |
| Module 3 | Ma façon de fonctionner | LIVRABLE-017 |
| Module 4 | Mes besoins | LIVRABLE-023 |
| Module 5 | Mes motivations profondes | LIVRABLE-029 |
| Module 6 | Mes compétences | LIVRABLE-035 |
| Module 7 | Mes talents | LIVRABLE-041 |
| Module 8 | Mon environnement idéal | LIVRABLE-047 |
| Module 9 | Mes ressources et contraintes | LIVRABLE-053 |
| Module 10 | Projet de vie et scénarios | LIVRABLE-059 |
| Module 11 | Mon plan d'action | LIVRABLE-065 |

---

## Paramètres recommandés pour tous les formulaires

Dans Google Forms :

- Collecter les adresses e-mail : selon contexte.
- Limiter à une réponse : non, sauf usage interne contrôlé.
- Autoriser la modification des réponses : oui.
- Afficher la barre de progression : oui.
- Mélanger l'ordre des questions : non.
- Transformer en quiz : non.
- Message de confirmation : personnalisé.

---

## Message de confirmation type

Merci pour tes réponses.

Je vais les lire tranquillement et revenir vers toi avec une restitution.

Il n'y a pas de bonne ou mauvaise réponse : l'objectif est de mieux comprendre ce qui est juste pour toi.

---

## Méthode de création

### Étape 1 – Créer le formulaire

Créer un nouveau Google Forms.

Nommer le formulaire selon le modèle :

```text
CAP – Module X : Nom du module
```

### Étape 2 – Copier l'introduction

Reprendre le texte d'introduction du livrable source.

L'introduction doit rappeler :

- l'objectif du module ;
- le temps estimé ;
- l'absence de bonne ou mauvaise réponse ;
- la possibilité de répondre avec des exemples.

### Étape 3 – Créer les sections

Créer une section Google Forms pour chaque section du livrable.

Ne pas regrouper tous les items sur une seule page si le formulaire dépasse 20 questions.

### Étape 4 – Créer les questions

Pour chaque question :

- copier le libellé ;
- choisir le type indiqué ;
- ajouter les choix si nécessaire ;
- rendre obligatoire sauf mention contraire ;
- vérifier la colonne attendue.

### Étape 5 – Connecter Google Sheets

Créer ou sélectionner le classeur de réponses.

Le nom recommandé est :

```text
CAP – Réponses – Module X – Nom du module
```

### Étape 6 – Tester

Faire une réponse fictive complète.

Vérifier :

- que les sections s'affichent correctement ;
- que les choix sont complets ;
- que les réponses arrivent dans Google Sheets ;
- que les colonnes sont lisibles ;
- que le formulaire n'est pas trop long.

---

## Convention de nommage des colonnes

Les colonnes doivent rester proches des noms techniques prévus dans les livrables.

Exemple :

```text
score_besoin_autonomie
competences_transferables
scenario_prioritaire
```

Pourquoi :

- faciliter les exports ;
- garder une cohérence entre les modules ;
- permettre un futur traitement automatique ;
- éviter les colonnes trop longues ou ambiguës.

---

## Conseils de lisibilité

Pour une participante, éviter :

- les sections trop longues ;
- les questions doubles ;
- les formulations abstraites ;
- les choix trop nombreux sans option Autre.

Préférer :

- des questions simples ;
- des exemples lorsque nécessaire ;
- des paragraphes pour les réponses narratives ;
- des échelles de 1 à 10 pour les intensités.

---

## Gestion des formulaires longs

Certains modules peuvent être exigeants.

Options possibles :

- laisser la modification des réponses activée ;
- inviter à répondre en plusieurs fois ;
- rappeler que les réponses courtes sont acceptées ;
- utiliser le cahier de réflexion uniquement si nécessaire.

---

## Contrôle final avant envoi

Avant d'envoyer un formulaire :

- [ ] Titre correct.
- [ ] Introduction claire.
- [ ] Sections créées.
- [ ] Questions obligatoires paramétrées.
- [ ] Choix vérifiés.
- [ ] Barre de progression activée.
- [ ] Modification des réponses activée.
- [ ] Google Sheets connecté.
- [ ] Réponse test réalisée.
- [ ] Lien de partage vérifié.

---

## Résultat attendu

À la fin de cette étape, l'accompagnant dispose de formulaires CAP prêts à envoyer, reliés à des feuilles de réponses exploitables.
