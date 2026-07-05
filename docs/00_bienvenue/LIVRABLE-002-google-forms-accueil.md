# LIVRABLE-002
# Google Forms 0 – Accueil et démarrage

**Version :** 1.0

---

## Informations générales

**Nom du formulaire**

CAP – Bienvenue dans votre parcours

**Description**

Bienvenue dans CAP.

Ce premier formulaire permet de préparer le parcours.

Il ne cherche pas à analyser ton profil.

Il permet simplement de faire connaissance, de comprendre tes attentes et de personnaliser ton accompagnement.

**Temps estimé :** 10 à 15 minutes.

---

## Paramètres Google Forms

- Collecter automatiquement la date de réponse : Oui
- Autoriser la modification des réponses : Oui
- Barre de progression : Oui
- Questions obligatoires : Oui, sauf mention contraire
- Validation des réponses : Activée lorsque nécessaire

---

## SECTION 1 — Bienvenue

### Texte

Merci d'avoir choisi de commencer ce parcours.

Il ne s'agit ni d'un examen, ni d'un test.

Tu peux répondre à ton rythme.

Les réponses les plus utiles sont toujours les plus sincères.

---

## SECTION 2 — Faisons connaissance

### Q001 — Comment souhaites-tu que je m'adresse à toi ?

- Type : Réponse courte
- Obligatoire : Oui
- Colonne Google Sheets : `participant_prenom`

### Q002 — Quel âge as-tu ?

- Type : Réponse courte
- Validation : nombre
- Obligatoire : Oui
- Colonne Google Sheets : `age`

### Q003 — Quelle est ta situation actuelle ?

- Type : Liste déroulante
- Choix : Salarié(e), Indépendant(e), Étudiant(e), En recherche d'emploi, En reconversion, Entrepreneur(e), Sans activité actuellement, Retraité(e), Autre
- Colonne Google Sheets : `situation`

### Q004 — Décris en quelques lignes ta situation actuelle.

- Type : Paragraphe
- Colonne Google Sheets : `description_situation`

---

## SECTION 3 — Pourquoi ce parcours ?

### Q005 — Pourquoi as-tu décidé de commencer ce parcours aujourd'hui ?

- Type : Paragraphe
- Colonne Google Sheets : `declencheur`

### Q006 — Qu'aimerais-tu avoir obtenu à la fin de ce parcours ?

- Type : Paragraphe
- Colonne Google Sheets : `attentes`

### Q007 — Dans six mois, comment sauras-tu que ce parcours t'a été utile ?

- Type : Paragraphe
- Colonne Google Sheets : `definition_reussite`

---

## SECTION 4 — Comment te sens-tu aujourd'hui ?

Consigne : pour les questions suivantes, utilise une note de 1 à 10.

1 = Très faible. 10 = Très élevé.

### Q008 — Satisfaction dans ta vie personnelle

- Type : Échelle linéaire 1 à 10
- Colonne Google Sheets : `score_vie_personnelle`

### Q009 — Satisfaction dans ta vie professionnelle

- Type : Échelle linéaire 1 à 10
- Colonne Google Sheets : `score_vie_professionnelle`

### Q010 — Confiance en toi actuellement

- Type : Échelle linéaire 1 à 10
- Colonne Google Sheets : `score_confiance`

### Q011 — Motivation actuelle

- Type : Échelle linéaire 1 à 10
- Colonne Google Sheets : `score_motivation`

### Q012 — Énergie générale

- Type : Échelle linéaire 1 à 10
- Colonne Google Sheets : `score_energie`

### Q013 — Optimisme concernant l'avenir

- Type : Échelle linéaire 1 à 10
- Colonne Google Sheets : `score_optimisme`

---

## SECTION 5 — Organisation

### Q014 — Combien de temps peux-tu consacrer chaque semaine à ce parcours ?

- Type : Choix multiples
- Choix : 15 minutes, 30 minutes, 1 heure, 2 heures, Plus de 2 heures
- Colonne Google Sheets : `temps_disponible`

### Q015 — Quel est généralement le meilleur moment pour toi ?

- Type : Cases à cocher
- Choix : Matin, Midi, Après-midi, Soir, Week-end
- Colonne Google Sheets : `disponibilites`

### Q016 — Préfères-tu avancer :

- Type : Choix multiples
- Choix : En une seule fois, Petit à petit, Sans préférence
- Colonne Google Sheets : `rythme_prefere`

---

## SECTION 6 — Ton engagement

### Texte

Ce parcours fonctionne d'autant mieux que chacun s'y investit avec sincérité.

### Q017 — Je m'engage à répondre honnêtement.

- Type : Case à cocher
- Colonne Google Sheets : `engagement_sincerite`

### Q018 — Je m'engage à prendre le temps de réfléchir.

- Type : Case à cocher
- Colonne Google Sheets : `engagement_reflexion`

### Q019 — Je m'autorise à changer d'avis au cours du parcours.

- Type : Case à cocher
- Colonne Google Sheets : `engagement_evolution`

### Q020 — Écris une phrase que tu aimerais relire lorsque tu douteras.

- Type : Paragraphe
- Colonne Google Sheets : `phrase_motivation`

---

## SECTION 7 — Fin du formulaire

### Texte

Merci.

Tu viens de terminer la première étape.

À partir de maintenant, nous allons construire progressivement une vision plus claire de ton parcours, de tes ressources et de tes aspirations.

Le prochain module s'intitule : **Mon histoire**.

Prends quelques jours si tu en ressens le besoin avant de commencer.

---

## Colonnes Google Sheets attendues

- Date
- participant_prenom
- age
- situation
- description_situation
- declencheur
- attentes
- definition_reussite
- score_vie_personnelle
- score_vie_professionnelle
- score_confiance
- score_motivation
- score_energie
- score_optimisme
- temps_disponible
- disponibilites
- rythme_prefere
- engagement_sincerite
- engagement_reflexion
- engagement_evolution
- phrase_motivation

---

## Checklist avant publication

- [ ] Toutes les questions obligatoires sont configurées.
- [ ] Les échelles vont bien de 1 à 10.
- [ ] Les validations numériques sont actives.
- [ ] La barre de progression est affichée.
- [ ] Le formulaire a été testé avec une réponse fictive.
- [ ] Les colonnes Google Sheets correspondent exactement aux noms définis ci-dessus.
