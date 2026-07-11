# User stories - CAP Method v2.0-ai

## Objectif du document

Ce document sert de backlog fonctionnel pour la feature `v2.0-ai`.

Il permet :

- de valider le périmètre fonctionnel avant développement ;
- de suivre l'avancement de la feature ;
- de distinguer les besoins consultant, bénéficiaire et système ;
- de protéger les garde-fous méthodologiques ;
- de conserver la compatibilité avec `v1.0-pro`.

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
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

Aucune sortie IA ne doit être remise directement au bénéficiaire sans relecture et validation humaine.

---

# Epic 1 - Génération d'une analyse IA assistée

## US-AI-001 - Générer un brouillon d'analyse IA

**En tant que** consultant,  
**je veux** générer un brouillon d'analyse IA depuis les réponses structurées,  
**afin de** gagner du temps dans la préparation de ma lecture consultant.

### Critères d'acceptation

- La commande génère un fichier `ai-analysis-draft.md`.
- La génération peut partir d'un `AnalysisSnapshot`.
- Le fichier généré est destiné au consultant uniquement.
- Le fichier contient un avertissement méthodologique.
- Le fichier indique que la validation consultant est obligatoire.

### Statut

```text
TODO
```

---

## US-AI-002 - Structurer l'analyse IA en sections obligatoires

**En tant que** consultant,  
**je veux** que l'analyse IA soit structurée en sections fixes,  
**afin de** pouvoir la relire rapidement et comparer les analyses entre bénéficiaires.

### Sections attendues

- avertissement méthodologique ;
- synthèse neutre des réponses ;
- thèmes récurrents ;
- valeurs exprimées ;
- motivations apparentes ;
- compétences évoquées ;
- contraintes et freins ;
- hypothèses professionnelles ;
- points à clarifier ;
- questions d'entretien ;
- risques d'interprétation ;
- validation consultant obligatoire.

### Critères d'acceptation

- Toutes les sections sont présentes dans le Markdown généré.
- L'ordre des sections est stable.
- La CI vérifie la présence des sections obligatoires.

### Statut

```text
TODO
```

---

## US-AI-003 - Produire une synthèse neutre des réponses

**En tant que** consultant,  
**je veux** obtenir une synthèse neutre des réponses,  
**afin de** disposer d'une première lecture sans interprétation excessive.

### Critères d'acceptation

- La synthèse reformule les réponses sans diagnostic.
- Les formulations restent prudentes.
- Les éléments incertains sont signalés comme hypothèses.
- La synthèse ne conclut pas à la place du consultant.

### Statut

```text
TODO
```

---

## US-AI-004 - Identifier les thèmes récurrents

**En tant que** consultant,  
**je veux** que l'IA identifie les thèmes qui reviennent dans les réponses,  
**afin de** repérer rapidement les axes majeurs du bilan.

### Critères d'acceptation

- Les thèmes sont listés clairement.
- Chaque thème est formulé de manière prudente.
- Les thèmes doivent être reliés aux réponses disponibles.
- Les thèmes ne doivent pas être présentés comme des vérités définitives.

### Statut

```text
TODO
```

---

# Epic 2 - Hypothèses professionnelles et points à clarifier

## US-AI-005 - Proposer des hypothèses professionnelles

**En tant que** consultant,  
**je veux** que l'IA propose des hypothèses professionnelles,  
**afin de** préparer la discussion avec le bénéficiaire.

### Critères d'acceptation

- Les hypothèses sont explicitement formulées comme hypothèses.
- Aucune hypothèse n'est présentée comme une orientation définitive.
- Chaque hypothèse contient un point de validation associé.
- Les hypothèses sont utilisables en entretien.

### Statut

```text
TODO
```

---

## US-AI-006 - Identifier les zones à clarifier

**En tant que** consultant,  
**je veux** que l'IA identifie les réponses ambiguës, incomplètes ou contradictoires,  
**afin de** préparer mes questions d'entretien.

### Critères d'acceptation

- Les zones à clarifier sont listées.
- Les contradictions potentielles sont formulées prudemment.
- Chaque point de clarification propose une question possible.
- Aucune contradiction n'est transformée en jugement.

### Statut

```text
TODO
```

---

## US-AI-007 - Générer des questions d'entretien

**En tant que** consultant,  
**je veux** obtenir des questions d'entretien à partir de l'analyse IA,  
**afin de** conduire une restitution plus précise.

### Critères d'acceptation

- Les questions sont ouvertes.
- Les questions ne sont pas orientées.
- Les questions évitent les formulations culpabilisantes.
- Les questions sont reliées aux hypothèses ou zones à clarifier.

### Statut

```text
TODO
```

---

# Epic 3 - Garde-fous méthodologiques

## US-AI-008 - Appliquer des formulations prudentes

**En tant que** responsable méthodologique,  
**je veux** que les sorties IA utilisent des formulations prudentes,  
**afin de** éviter les conclusions abusives.

### Formulations attendues

```text
Les réponses suggèrent...
Une hypothèse possible est...
Ce point mérite validation...
Le consultant pourra explorer...
```

### Critères d'acceptation

- Les formulations prudentes sont documentées.
- La génération de test contient au moins une formulation prudente.
- La documentation rappelle que l'IA ne décide pas.

### Statut

```text
TODO
```

---

## US-AI-009 - Bloquer les formulations interdites

**En tant que** responsable méthodologique,  
**je veux** détecter les formulations interdites,  
**afin de** éviter les sorties trop affirmatives.

### Formulations interdites

```text
Cette personne est...
Le bon métier est...
Il faut absolument...
Le diagnostic est...
Son problème principal est...
```

### Critères d'acceptation

- La liste des formulations interdites est documentée.
- La CI peut vérifier les sorties de test.
- Les sorties de test ne contiennent pas les formulations interdites.

### Statut

```text
TODO
```

---

## US-AI-010 - Ajouter un avertissement méthodologique obligatoire

**En tant que** consultant,  
**je veux** que chaque analyse IA contienne un avertissement méthodologique,  
**afin de** rappeler les limites de la génération.

### Critères d'acceptation

- Le fichier contient une section `Avertissement méthodologique`.
- L'avertissement indique que la sortie est un brouillon.
- L'avertissement indique que le consultant doit valider.
- L'avertissement indique que le bénéficiaire peut nuancer ou corriger.

### Statut

```text
TODO
```

---

# Epic 4 - Traçabilité et audit

## US-AI-011 - Générer un manifest IA

**En tant que** consultant,  
**je veux** disposer d'un manifest de génération IA,  
**afin de** tracer l'origine et les conditions de génération.

### Contenu attendu

```json
{
  "sessionId": "sample-session",
  "source": "analysis-snapshot",
  "generatedAt": "datetime",
  "provider": "configured-provider",
  "model": "configured-model",
  "status": "draft",
  "requiresConsultantValidation": true,
  "guardrailsApplied": true
}
```

### Critères d'acceptation

- Un manifest IA est généré.
- Le manifest contient la source utilisée.
- Le manifest indique que la validation consultant est obligatoire.
- Le manifest indique les garde-fous appliqués.

### Statut

```text
TODO
```

---

## US-AI-012 - Conserver la compatibilité avec v1.0-pro

**En tant que** mainteneur,  
**je veux** que l'IA soit optionnelle,  
**afin de** préserver la chaîne stable `v1.0-pro`.

### Critères d'acceptation

- Les commandes existantes continuent de fonctionner sans IA.
- Les exports DOCX/PDF/ZIP restent fonctionnels.
- La CI existante reste verte.
- La génération IA n'est pas obligatoire pour produire les livrables classiques.

### Statut

```text
TODO
```

---

# Epic 5 - Commandes et intégration technique

## US-AI-013 - Créer la commande generate-ai-analysis-draft

**En tant que** mainteneur,  
**je veux** une commande dédiée à la génération IA,  
**afin de** intégrer proprement cette étape dans la chaîne CAP.

### Commande cible

```bash
node questionnaire-engine/tools/generate-ai-analysis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/ai/generated/sample.ai-analysis-draft.md
```

### Critères d'acceptation

- La commande existe.
- La commande accepte un fichier `AnalysisSnapshot` en entrée.
- La commande produit un Markdown en sortie.
- La commande échoue clairement si l'entrée est absente ou invalide.

### Statut

```text
TODO
```

---

## US-AI-014 - Ajouter une génération de test sans fournisseur externe obligatoire

**En tant que** mainteneur,  
**je veux** pouvoir tester la chaîne IA sans dépendre d'un fournisseur externe,  
**afin de** garder une CI fiable.

### Critères d'acceptation

- Un mode de génération de test existe.
- La CI peut générer un `ai-analysis-draft.md` sans clé API.
- Le résultat de test respecte les sections obligatoires.
- Le résultat de test respecte les garde-fous.

### Statut

```text
TODO
```

---

## US-AI-015 - Préparer l'intégration future d'un fournisseur IA

**En tant que** mainteneur,  
**je veux** isoler l'appel IA derrière une interface ou un provider,  
**afin de** pouvoir changer de fournisseur sans réécrire la chaîne CAP.

### Critères d'acceptation

- Le fournisseur IA est abstrait.
- Le mode test ne dépend pas du provider réel.
- Les paramètres provider/model sont traçables.
- La configuration ne contient pas de clé secrète dans le dépôt.

### Statut

```text
TODO
```

---

# Epic 6 - Validation consultant

## US-AI-016 - Marquer l'analyse IA comme brouillon non validé

**En tant que** consultant,  
**je veux** que l'analyse IA soit marquée comme brouillon,  
**afin de** éviter toute confusion avec une synthèse finale.

### Critères d'acceptation

- Le fichier indique `Brouillon IA` ou équivalent.
- Le manifest indique `status: draft`.
- Le fichier rappelle qu'il ne doit pas être remis tel quel au bénéficiaire.

### Statut

```text
TODO
```

---

## US-AI-017 - Préparer une étape ConsultantReview

**En tant que** consultant,  
**je veux** disposer d'une étape de revue après l'analyse IA,  
**afin de** valider, corriger ou rejeter les hypothèses proposées.

### Critères d'acceptation

- La documentation décrit l'étape `ConsultantReview`.
- Les hypothèses IA sont considérées comme non validées par défaut.
- Le consultant peut utiliser ou ignorer l'analyse IA.
- La synthèse finale ne doit pas dépendre automatiquement d'une hypothèse non validée.

### Statut

```text
TODO
```

---

# Epic 7 - Documentation et exploitation

## US-AI-018 - Documenter la procédure d'utilisation IA

**En tant que** consultant,  
**je veux** une procédure d'utilisation de l'analyse IA,  
**afin de** savoir comment l'exploiter sans confusion méthodologique.

### Critères d'acceptation

- La procédure explique quand générer l'analyse IA.
- La procédure explique comment la relire.
- La procédure explique ce qui peut être repris dans la synthèse finale.
- La procédure rappelle les limites et garde-fous.

### Statut

```text
TODO
```

---

## US-AI-019 - Documenter les limites professionnelles de l'IA

**En tant que** responsable du projet,  
**je veux** documenter les limites professionnelles de l'IA,  
**afin de** éviter une mauvaise utilisation de CAP.

### Critères d'acceptation

- Les limites sont explicites.
- Le document interdit les diagnostics.
- Le document interdit les décisions automatiques.
- Le document impose la validation consultant.

### Statut

```text
TODO
```

---

# Epic 8 - Suivi d'avancement

## US-AI-020 - Suivre l'avancement des user stories

**En tant que** mainteneur,  
**je veux** utiliser ce document comme suivi d'avancement,  
**afin de** piloter la feature `v2.0-ai` sans perdre le périmètre.

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

## Lot 1 - Socle IA sans fournisseur externe

```text
US-AI-001
US-AI-002
US-AI-010
US-AI-013
US-AI-014
US-AI-016
```

## Lot 2 - Garde-fous

```text
US-AI-008
US-AI-009
US-AI-011
US-AI-012
US-AI-019
```

## Lot 3 - Analyse utile consultant

```text
US-AI-003
US-AI-004
US-AI-005
US-AI-006
US-AI-007
US-AI-017
```

## Lot 4 - Préparation provider IA réel

```text
US-AI-015
US-AI-018
US-AI-020
```

## Décision

Ce document doit être validé avant le développement de `feature/v2-ai`.

Une fois validé, chaque lot pourra être traité progressivement avec mise à jour des statuts.
