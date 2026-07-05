# LIVRABLE-006
# Google Sheets – Module 1 : Mon histoire

**Version :** 1.0

---

## Objectif

Ce classeur reçoit automatiquement les réponses du Google Forms **Module 1 – Mon histoire**.

Il prépare les informations, calcule des indicateurs simples, met en évidence les points d'attention et génère une première synthèse destinée à l'accompagnant.

Le classeur ne produit jamais de conclusions définitives.

Il prépare uniquement le travail d'analyse.

---

## Architecture du classeur

Le classeur est composé de huit feuilles.

1. Réponses
2. Paramètres
3. Analyse
4. Hypothèses
5. Fils rouges
6. Restitution
7. Tableau de bord
8. Journal accompagnant

---

## Feuille 1 — Réponses

Cette feuille est alimentée automatiquement par Google Forms.

Ne jamais modifier sa structure.

Colonnes attendues :

- Date
- Participant
- histoire_resume
- evenements_majeurs
- analyse_evenements
- fierte
- reussites
- analyse_reussites
- difficultes
- apprentissages
- experience_ouverte
- personnes_inspirantes
- impact_personnes
- note_enfance
- note_adolescence
- note_jeune_adulte
- note_actuelle
- centres_interet
- fil_rouge
- qualites_recurrentes
- trois_mots
- titre_vie
- grande_lecon
- prise_conscience
- sujets_approfondir

---

## Feuille 2 — Paramètres

Cette feuille contient les listes de référence.

### Catégories d'événements

- Personnel
- Familial
- Professionnel
- Formation
- Santé
- Déménagement
- Rencontre
- Autre

### Niveaux de confiance

- À explorer
- Faible
- Modéré
- Élevé

### Statut d'une hypothèse

- Ouverte
- À confirmer
- Confirmée
- Écartée

---

## Feuille 3 — Analyse

Une ligne par participante.

Colonnes calculées :

### Nombre d'événements racontés

Objectif : vérifier que le récit est suffisamment riche.

### Nombre de réussites citées

Objectif : repérer la capacité de la participante à identifier ses réussites.

### Nombre de difficultés citées

Objectif : identifier les périodes structurantes du parcours.

### Nombre de personnes inspirantes

Objectif : identifier les influences et modèles.

### Écart entre satisfaction actuelle et enfance

Formule : `note_actuelle - note_enfance`

### Écart entre satisfaction actuelle et début de vie adulte

Formule : `note_actuelle - note_jeune_adulte`

### Niveau de détail des réponses

À renseigner manuellement :

- Faible
- Moyen
- Élevé

### Besoin d'entretien complémentaire

Valeurs : Oui / Non

---

## Feuille 4 — Hypothèses

Une ligne par hypothèse.

Colonnes :

- ID
- Participant
- Hypothèse
- Niveau de confiance
- Arguments
- Éléments à confirmer
- Modules concernés
- Commentaires

### Exemple

| ID | Hypothèse | Confiance | Arguments | À confirmer |
|---|---|---|---|---|
| H001 | Le besoin de transmission semble récurrent. | À explorer | Plusieurs personnes inspirantes sont des enseignants. La participante évoque souvent le plaisir d'aider. | Module Motivations, Module Talents |

---

## Feuille 5 — Fils rouges

Objectif : identifier les thèmes qui reviennent plusieurs fois.

Colonnes :

- Fil rouge
- Nombre d'occurrences
- Modules concernés
- Confiance
- Commentaires

### Exemple

| Fil rouge | Occurrences | Modules | Confiance | Commentaires |
|---|---:|---|---|---|
| Transmission | 3 | Histoire, Valeurs, Compétences | Modérée | À vérifier dans les modules suivants. |

---

## Feuille 6 — Restitution

Cette feuille prépare la mini-restitution.

Zones à compléter :

### Ce qui ressort immédiatement

....................................................

### Les principales ressources

....................................................

### Les questions encore ouvertes

....................................................

### Les points à explorer au prochain module

....................................................

### Citation de la participante

Choisir une phrase marquante.

---

## Feuille 7 — Tableau de bord

Indicateurs :

- Formulaire reçu : Oui / Non
- Analyse réalisée : Oui / Non
- Restitution envoyée : Oui / Non
- Module validé : Oui / Non
- Temps d'analyse
- Temps de restitution
- Temps total

---

## Feuille 8 — Journal accompagnant

Après chaque accompagnement, noter :

- Date
- Participant
- Ce qui a bien fonctionné
- Questions difficiles
- Questions inutiles
- Nouvelles idées
- Améliorations proposées
- Décision : À intégrer / À tester / À abandonner

---

## Mise en forme

- Vert : éléments validés
- Orange : éléments à vérifier
- Rouge : information manquante
- Bleu : commentaires de l'accompagnant

---

## Contrôle qualité

Avant de clôturer le module, vérifier :

- [ ] Toutes les réponses sont importées.
- [ ] Les indicateurs sont calculés.
- [ ] Les hypothèses sont documentées.
- [ ] Les fils rouges sont renseignés.
- [ ] La restitution est préparée.
- [ ] Le journal accompagnant est complété.

---

## Livrables générés

À partir de ce classeur, l'accompagnant dispose de :

- une vue synthétique du module ;
- une liste des premières hypothèses ;
- les thèmes récurrents à explorer ;
- les éléments de restitution ;
- un historique d'amélioration de la méthode.

Le classeur facilite l'organisation et la traçabilité, mais il ne remplace jamais le jugement de l'accompagnant.
