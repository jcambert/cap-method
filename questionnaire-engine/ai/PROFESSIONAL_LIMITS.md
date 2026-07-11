# Limites professionnelles de l'IA - CAP Method v2.0-ai

## Objectif

Ce document définit les limites professionnelles applicables à l'usage de l'IA dans `v2.0-ai`.

Il vise à éviter toute confusion entre :

```text
brouillon IA
  ≠ analyse professionnelle validée
  ≠ diagnostic
  ≠ décision d'orientation
  ≠ synthèse finale remise au bénéficiaire
```

## Règle principale

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

Aucune sortie IA ne doit être considérée comme définitive sans revue humaine.

## Ce que l'IA peut faire

L'IA peut aider le consultant à :

- reformuler une première lecture des réponses ;
- identifier des thèmes récurrents ;
- lister des valeurs ou motivations apparentes ;
- repérer des compétences évoquées ;
- signaler des contraintes ou freins potentiels ;
- proposer des hypothèses professionnelles prudentes ;
- préparer des questions d'entretien ;
- signaler des risques d'interprétation ;
- structurer un brouillon de travail.

## Ce que l'IA ne doit pas faire

L'IA ne doit pas :

- produire un diagnostic psychologique ;
- décider du projet professionnel du bénéficiaire ;
- affirmer qu'un métier est le bon choix ;
- remplacer l'entretien consultant ;
- remplacer la validation du bénéficiaire ;
- produire seule une synthèse finale ;
- livrer directement un document au bénéficiaire ;
- inférer des traits personnels sensibles non exprimés ;
- conclure à partir d'un signal faible ou isolé ;
- transformer une hypothèse en certitude.

## Formulations autorisées

Les formulations doivent rester prudentes :

```text
Les réponses suggèrent...
Une hypothèse possible est...
Ce point mérite validation...
Le consultant pourra explorer...
Il conviendra de vérifier avec le bénéficiaire...
Ce point peut constituer une piste de discussion...
```

## Formulations interdites

Les formulations suivantes sont interdites dans les sorties de test et doivent être évitées dans toute sortie IA :

```text
Cette personne est...
Cette personne doit...
Le bon métier est...
Il faut absolument...
Son problème principal est...
Le diagnostic est...
Le profil psychologique est...
```

## Validation consultant obligatoire

Avant toute réutilisation dans une synthèse finale, le consultant doit :

- relire le brouillon IA ;
- vérifier les éléments avec les réponses sources ;
- supprimer les formulations trop affirmatives ;
- reformuler les hypothèses ;
- préparer les points à clarifier en entretien ;
- valider les éléments retenus avec le bénéficiaire ;
- ne conserver que les éléments professionnellement justifiés.

## Remise au bénéficiaire

Le brouillon IA ne doit pas être remis tel quel au bénéficiaire.

Seuls les livrables relus, adaptés et validés peuvent être transmis :

```text
FinalSynthesis
ActionPlan
Exports DOCX/PDF
```

## Traçabilité obligatoire

Chaque génération IA doit produire ou conserver :

- la source utilisée ;
- la date de génération ;
- le provider ;
- le modèle ;
- le statut `draft` ;
- l'indication `requiresConsultantValidation` ;
- les garde-fous appliqués ;
- le fait que le document n'est pas prêt pour livraison au bénéficiaire.

Cette traçabilité est portée par :

```text
AIAnalysisManifest
```

## Données sensibles

Les réponses du bénéficiaire peuvent contenir des données personnelles ou sensibles.

L'usage de l'IA doit donc respecter les principes suivants :

- minimiser les données transmises ;
- éviter les informations inutiles à l'analyse ;
- ne pas exposer les données à un fournisseur externe sans cadre validé ;
- conserver la possibilité de désactiver l'étape IA ;
- documenter le provider utilisé ;
- informer le consultant du caractère non final de la sortie.

## Responsabilité professionnelle

La responsabilité de la synthèse finale reste humaine.

Le consultant reste responsable :

- du choix des éléments retenus ;
- de la formulation finale ;
- de la cohérence méthodologique ;
- de la restitution au bénéficiaire ;
- de l'adaptation au contexte réel ;
- de la prudence dans l'interprétation.

## Statut dans v2.0-ai

```text
REQUIRED GUARDRAIL
```

Ce document fait partie des garde-fous obligatoires de `v2.0-ai`.
