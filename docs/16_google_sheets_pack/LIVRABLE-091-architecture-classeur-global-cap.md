# LIVRABLE-091
# Architecture du classeur global CAP

**Version :** 1.0

---

## Objectif

Ce document décrit l'architecture cible du classeur global CAP.

Le classeur global doit permettre de suivre un accompagnement complet, d'analyser les réponses, de produire les restitutions et de piloter le plan d'action.

---

## Nom recommandé

```text
CAP – Classeur global – [Prénom participante]
```

Pour un modèle :

```text
CAP – TEMPLATE – Classeur global
```

---

## Architecture des onglets

```text
00_Tableau_de_bord
01_Index_modules
02_Index_livrables
03_Journal_accompagnant
04_Hypotheses_globales
05_Fils_rouges
06_Points_vigilance
07_Ressources_contraintes
08_Scenarios
09_Plan_action
10_Synthese_finale
M00_Reponses_Accueil
M01_Reponses_Histoire
M02_Reponses_Valeurs
M03_Reponses_Fonctionnement
M04_Reponses_Besoins
M05_Reponses_Motivations
M06_Reponses_Competences
M07_Reponses_Talents
M08_Reponses_Environnement
M09_Reponses_Ressources
M10_Reponses_Scenarios
M11_Reponses_Plan_Action
M01_Analyse
M02_Analyse
M03_Analyse
M04_Analyse
M05_Analyse
M06_Analyse
M07_Analyse
M08_Analyse
M09_Analyse
M10_Analyse
M11_Analyse
```

---

## 00_Tableau_de_bord

Objectif : donner une vue synthétique de l'avancement.

Colonnes recommandées :

- Module
- Statut formulaire
- Réponses reçues
- Analyse faite
- Restitution faite
- Points à suivre
- Date dernière action
- Prochaine étape

---

## 01_Index_modules

Objectif : suivre les modules CAP.

Colonnes :

- Numéro module
- Nom module
- Livrable Forms
- Livrable Sheets
- Livrable cahier
- Livrable Messenger
- Livrable restitution
- Statut
- Commentaire

---

## 02_Index_livrables

Objectif : suivre les documents utilisés.

Colonnes :

- Numéro livrable
- Nom livrable
- Type
- Chemin GitHub
- Utilisé : Oui / Non
- Export disponible : Oui / Non
- Commentaire

---

## 03_Journal_accompagnant

Objectif : garder une trace des décisions et observations.

Colonnes :

- Date
- Module
- Observation
- Hypothèse associée
- Décision
- Prochaine action
- Commentaire

---

## 04_Hypotheses_globales

Objectif : centraliser les hypothèses transversales.

Colonnes :

- ID
- Hypothèse
- Modules sources
- Indices
- Niveau de confiance
- À confirmer
- Décision
- Commentaire

Niveaux de confiance :

- À explorer
- Faible
- Modéré
- Élevé

Décisions :

- Garder
- Ajuster
- Abandonner
- Confirmer plus tard

---

## 05_Fils_rouges

Objectif : repérer ce qui revient dans plusieurs modules.

Colonnes :

- Fil rouge
- Modules concernés
- Indices
- Importance
- Impact sur scénarios
- Commentaire

---

## 06_Points_vigilance

Objectif : suivre les limites, tensions ou risques.

Colonnes :

- Point de vigilance
- Source
- Type
- Impact
- Action de prudence
- Statut
- Commentaire

Types :

- Énergie
- Temps
- Finances
- Confiance
- Relationnel
- Environnement
- Formation
- Autre

---

## 07_Ressources_contraintes

Objectif : centraliser les ressources et contraintes issues du Module 9 mais aussi des modules précédents.

Colonnes :

- Élément
- Type : Ressource / Contrainte
- Famille
- Source
- Impact
- Levier possible
- Commentaire

---

## 08_Scenarios

Objectif : comparer les scénarios professionnels et personnels.

Colonnes :

- Scénario
- Description
- Désirabilité
- Réalisme
- Cohérence valeurs
- Compatibilité contraintes
- Risques
- Conditions de réussite
- Décision

---

## 09_Plan_action

Objectif : suivre les actions concrètes.

Colonnes :

- Action
- Objectif lié
- Échéance
- Priorité
- Ressource nécessaire
- Risque associé
- Statut
- Résultat
- Commentaire

Statuts :

- À faire
- En cours
- Réalisé
- Reporté
- Abandonné

---

## 10_Synthese_finale

Objectif : préparer le document final.

Sections recommandées :

- Fils rouges
- Valeurs principales
- Besoins essentiels
- Motivations
- Compétences
- Talents
- Environnement favorable
- Ressources
- Contraintes
- Scénario prioritaire
- Plan d'action
- Points de vigilance

---

## Onglets réponses brutes

Les onglets `MXX_Reponses` doivent être connectés aux Google Forms.

Règles :

- ne pas modifier les colonnes ;
- ne pas supprimer les lignes ;
- ne pas faire d'analyse dans ces feuilles ;
- protéger si possible.

---

## Onglets analyse

Les onglets `MXX_Analyse` servent à extraire les éléments utiles.

Colonnes recommandées :

- Élément observé
- Source réponse
- Interprétation prudente
- Module lié
- À reprendre en restitution
- Commentaire

---

## Résultat attendu

Cette architecture doit permettre de suivre un parcours CAP complet sans perdre les informations importantes et sans mélanger réponses brutes, analyse et restitution.
