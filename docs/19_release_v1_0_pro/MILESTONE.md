# CAP Method v1.0-pro - Jalon d'exploitation professionnelle

## Objectif du jalon

Le jalon `v1.0-pro` marque la fin de la phase de construction initiale de CAP Method.

Son objectif est de rendre CAP exploitable dans un cadre professionnel, avec une chaîne stable, documentée, versionnée et validée en intégration continue.

## Définition de terminé

Le jalon est considéré terminé lorsque :

- les questionnaires sont structurés et versionnés ;
- les formulaires peuvent être générés depuis la source CMDL ;
- les réponses peuvent être importées et normalisées ;
- une analyse structurée peut être produite ;
- une synthèse finale structurée peut être générée ;
- un plan d'action peut être généré ;
- les livrables peuvent être exportés en DOCX et PDF ;
- un ZIP final peut être produit ;
- la chaîne est validée par GitHub Actions ;
- la documentation d'exploitation existe ;
- les limites professionnelles sont explicites.

## Périmètre inclus

### Méthode CAP

- modules de travail ;
- synthèse finale ;
- plan d'action ;
- mode d'emploi accompagnant ;
- logique de bilan et d'accompagnement ;
- supports de publication et d'usage.

### Questionnaire Engine

- définitions CMDL `FORM-001` à `FORM-010` ;
- validation CMDL ;
- génération Google Forms / Google Sheets ;
- import CSV ;
- normalisation des réponses ;
- génération `ResponseSession` ;
- génération `AnalysisSnapshot` ;
- génération `SynthesisDraft` ;
- génération `FinalSynthesis` ;
- génération `ActionPlan` ;
- préparation du package ;
- export DOCX ;
- export PDF ;
- ZIP final.

### Qualité

- CI complète ;
- manifest de package ;
- documentation technique ;
- documentation d'exploitation ;
- séparation source / export / revue.

## Périmètre exclu de v1.0-pro

Les éléments suivants sont volontairement exclus du jalon `v1.0-pro` :

- interface web complète ;
- import direct Google Sheets API ;
- génération DOCX avancée avec tableaux Word natifs ;
- mise en page PDF avancée ;
- signature électronique ;
- portail bénéficiaire ;
- authentification multi-utilisateur ;
- stockage cloud applicatif ;
- moteur IA de rédaction finale autonome ;
- automatisation juridique ou réglementaire complète.

Ces éléments pourront faire partie de jalons ultérieurs.

## Niveau d'exploitation attendu

`v1.0-pro` permet :

1. de générer les questionnaires ;
2. de collecter les réponses ;
3. d'importer les réponses ;
4. de produire un pack consultant ;
5. de relire et ajuster humainement les livrables ;
6. de produire des exports DOCX/PDF ;
7. de livrer un ZIP final.

## Règle d'usage professionnel

Les livrables générés sont des supports de travail consultant.

Avant toute remise réelle à un bénéficiaire, le consultant doit :

- relire la synthèse ;
- corriger les formulations génériques ;
- vérifier les informations factuelles ;
- valider le projet cible ;
- adapter le plan d'action ;
- vérifier les données sensibles ;
- produire les exports finaux après validation.

## Critère d'arrêt

À partir du jalon `v1.0-pro`, toute amélioration non bloquante doit être déplacée vers un jalon ultérieur.

Le projet ne doit plus accumuler de nouvelles fonctionnalités avant la publication de la release.

## Jalons suivants possibles

- `v1.1-docx-quality` : tableaux Word natifs, meilleure page de garde, entêtes/pieds de page ;
- `v1.2-google-sheets-api` : import direct Google Sheets ;
- `v2.0-studio` : interface web CAP Method Studio ;
- `v2.1-ai-assisted` : assistance IA encadrée pour la rédaction consultant.
