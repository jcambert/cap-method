# CAP Workflow UI - v3.1 production-ready

## Objectif

Exposer dans l'interface SaaS la progression métier d'une session CAP.

Le Lot 4 transforme le modèle de workflow introduit au Lot 3 en éléments visibles par le consultant.

## Périmètre

```text
UI de progression workflow = oui
Résumé workflow dans la liste sessions = oui
Détail workflow sur la session = oui
Questionnaires en ligne complets = non
Actions de changement d'étape = non
Persistance workflow séparée = non
Migration EF = non
```

## Principe

Le workflow affiché est dérivé de deux informations déjà présentes dans les réponses API :

```text
CapSession.Status
CapSession.IsAiEnabled
```

Aucun nouveau contrat API n'est requis pour ce lot.

## Composants ajoutés

```text
WorkflowProgressCard
CapWorkflowProgressProjector
CapWorkflowProgressView
CapWorkflowStepView
CapWorkflowStepStateView
```

## Affichage liste sessions

La liste des sessions affiche désormais :

```text
Statut technique
Étape métier courante
Pourcentage d'avancement
```

## Affichage détail session

Le détail d'une session affiche désormais :

```text
Étape courante
Progression en pourcentage
Liste ordonnée des étapes
État de chaque étape : terminé, en cours, à venir, optionnel IA
```

## Décisions techniques

```text
Enum = non
switch/case = non
Contrat API = inchangé
Projection UI déterministe = oui
Dépendance au domaine serveur côté client = non
```

## Limites assumées

Ce lot n'ajoute pas encore :

```text
boutons d'avancement métier
questionnaires bénéficiaire
édition de synthèse
validation consultant
export livrables
```

Ces éléments seront traités dans les lots suivants.
