# LIVRABLE-092
# Dictionnaire des colonnes CAP

**Version :** 1.0

---

## Objectif

Ce dictionnaire définit les principales colonnes utilisées dans les Google Sheets CAP.

Il permet de garder une cohérence entre les formulaires, les réponses, les analyses et les restitutions.

---

## Principes de nommage

Les noms de colonnes doivent être :

- courts ;
- explicites ;
- sans accent ;
- en minuscules ;
- séparés par des underscores ;
- stables dans le temps.

Exemples :

```text
score_autonomie
valeurs_prioritaires
scenario_prioritaire
premiere_action_engagement
```

---

## Colonnes transversales

| Colonne | Description | Type |
|---|---|---|
| participant | Nom ou code participante | Texte |
| date_reponse | Date de réponse | Date |
| module | Numéro ou nom du module | Texte |
| statut | Statut de traitement | Liste |
| commentaire_accompagnant | Note accompagnant | Texte long |

---

## Colonnes de scores

| Colonne | Description | Échelle |
|---|---|---|
| score_energie | Niveau d'énergie ressenti | 1 à 10 |
| score_clarte | Niveau de clarté | 1 à 10 |
| score_confiance | Niveau de confiance | 1 à 10 |
| score_autonomie | Besoin ou perception d'autonomie | 1 à 10 |
| score_securite | Besoin ou perception de sécurité | 1 à 10 |
| score_motivation | Niveau de motivation | 1 à 10 |
| score_realisme | Réalisme d'un scénario | 1 à 10 |
| score_desirabilite | Désirabilité d'un scénario | 1 à 10 |

Règle : un score ne doit jamais être interprété seul.

---

## Module 1 – Histoire

| Colonne | Description |
|---|---|
| moments_importants | Événements marquants |
| transitions_parcours | Transitions importantes |
| reussites_significatives | Réussites personnelles ou professionnelles |
| difficultes_traversees | Difficultés rencontrées |
| apprentissages_parcours | Apprentissages issus du parcours |
| fils_rouges_histoire | Fils rouges observés dans l'histoire |

---

## Module 2 – Valeurs

| Colonne | Description |
|---|---|
| valeurs_importantes | Valeurs citées |
| valeurs_prioritaires | Valeurs retenues comme prioritaires |
| valeurs_non_negociables | Valeurs à respecter absolument |
| situations_alignement | Situations où les valeurs sont respectées |
| situations_tension_valeurs | Situations où les valeurs sont contrariées |

---

## Module 3 – Fonctionnement

| Colonne | Description |
|---|---|
| conditions_fonctionnement | Conditions favorables |
| sources_energie | Ce qui donne de l'énergie |
| sources_fatigue | Ce qui fatigue |
| rythme_preferentiel | Rythme favorable |
| mode_decision | Façon de décider |
| besoins_cadre_autonomie | Équilibre cadre/autonomie |

---

## Module 4 – Besoins

| Colonne | Description |
|---|---|
| besoins_essentiels | Besoins principaux |
| besoins_insatisfaits | Besoins peu nourris |
| signaux_besoin_non_respecte | Signaux d'alerte |
| besoins_prioritaires | Besoins à respecter dans les choix |
| conditions_equilibre | Conditions d'équilibre |

---

## Module 5 – Motivations

| Colonne | Description |
|---|---|
| motivations_principales | Moteurs principaux |
| activites_energie | Activités qui donnent de l'énergie |
| activites_demotivation | Activités qui démotivent |
| moteurs_durables | Moteurs qui semblent stables |
| contribution_souhaitee | Type de contribution recherchée |

---

## Module 6 – Compétences

| Colonne | Description |
|---|---|
| competences_maitrisees | Compétences maîtrisées |
| competences_aimees | Compétences appréciées |
| competences_transferables | Compétences transférables |
| preuves_competences | Exemples ou preuves |
| competences_a_developper | Compétences à renforcer |

---

## Module 7 – Talents

| Colonne | Description |
|---|---|
| talents_possibles | Talents identifiés comme hypothèses |
| talents_reconnus | Talents reconnus par autrui |
| talents_energisant | Talents qui donnent de l'énergie |
| talents_sous_utilises | Talents peu utilisés |
| talent_a_assumer | Talent à mieux reconnaître |

---

## Module 8 – Environnement

| Colonne | Description |
|---|---|
| environnement_favorable | Conditions favorables |
| environnements_a_eviter | Contextes coûteux |
| niveau_cadre_souhaite | Niveau de cadre utile |
| autonomie_souhaitee | Autonomie souhaitée |
| rythme_ideal | Rythme favorable |
| non_negociables_environnement | Conditions non négociables |

---

## Module 9 – Ressources et contraintes

| Colonne | Description |
|---|---|
| ressources_personnelles | Ressources internes |
| personnes_soutien | Soutiens relationnels |
| ressources_materielles | Ressources pratiques |
| contraintes_principales | Contraintes majeures |
| contraintes_negociables | Contraintes pouvant être allégées |
| contraintes_non_modifiables | Contraintes à respecter |
| elements_a_securiser | Points à sécuriser |

---

## Module 10 – Scénarios

| Colonne | Description |
|---|---|
| scenario_1_description | Scénario 1 |
| scenario_2_description | Scénario 2 |
| scenario_3_description | Scénario 3 |
| scenario_plus_coherent | Scénario prioritaire provisoire |
| scenario_en_reserve | Scénario secondaire |
| risques_scenarios | Risques associés |
| action_test_scenario | Test terrain possible |

---

## Module 11 – Plan d'action

| Colonne | Description |
|---|---|
| scenario_prioritaire | Scénario choisi provisoirement |
| objectif_30_jours | Objectif à 30 jours |
| objectif_3_mois | Objectif à 3 mois |
| objectif_12_mois | Objectif à 12 mois |
| premiere_action_engagement | Première action prévue |
| date_premiere_action | Date de première action |
| risques_a_securiser | Risques à sécuriser |
| date_revision_plan | Date de révision |

---

## Colonnes d'analyse accompagnant

| Colonne | Description |
|---|---|
| element_observe | Élément extrait d'une réponse |
| source_reponse | Question ou extrait source |
| hypothese | Hypothèse prudente |
| niveau_confiance | Niveau de confiance |
| modules_lies | Modules concernés |
| a_confirmer | Ce qui doit être confirmé |
| decision | Garder / Ajuster / Abandonner |

---

## Résultat attendu

Ce dictionnaire permet de créer des feuilles cohérentes, exploitables et compatibles avec de futurs exports ou traitements automatisés.
