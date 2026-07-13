# ConsultantReview - CAP Method v2.0-ai

## Objectif

`ConsultantReview` est l'étape humaine obligatoire entre le brouillon IA et les livrables finaux.

```text
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
ConsultantReview
  ↓
FinalSynthesis
  ↓
ActionPlan
```

## Règle principale

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

Le brouillon IA ne peut pas être transmis tel quel au bénéficiaire.

## Entrées

Le consultant s'appuie sur :

```text
AIAnalysisDraft
AIAnalysisManifest
AnalysisSnapshot
ResponseSession
PROFESSIONAL_LIMITS.md
```

## Sorties attendues

La revue consultant doit produire ou préparer :

```text
FinalSynthesis
ActionPlan
Questions d'entretien
Points à clarifier
Hypothèses validées
Hypothèses rejetées
```

## Checklist de revue

### 1. Vérifier la source

- [ ] Le `ResponseSession` est complet.
- [ ] Le `AnalysisSnapshot` est cohérent.
- [ ] Le manifest IA indique le statut `draft`.
- [ ] Le manifest IA indique `requiresConsultantValidation: true`.

### 2. Relire les formulations IA

- [ ] Les formulations restent prudentes.
- [ ] Aucune formulation interdite n'est conservée.
- [ ] Les hypothèses sont bien présentées comme hypothèses.
- [ ] Les points sensibles ne sont pas transformés en certitudes.

### 3. Valider les thèmes récurrents

- [ ] Les thèmes repérés sont reliés à des réponses sources.
- [ ] Les thèmes faibles sont supprimés ou reformulés.
- [ ] Les thèmes majeurs sont préparés pour l'entretien.

### 4. Valider les compétences évoquées

- [ ] Les compétences sont reliées à des exemples.
- [ ] Les compétences supposées sont transformées en questions.
- [ ] Les compétences confirmées sont préparées pour la synthèse finale.

### 5. Valider les contraintes et freins

- [ ] Les contraintes sont reformulées sans jugement.
- [ ] Les freins sont vérifiés avec le bénéficiaire.
- [ ] Les contraintes non confirmées restent des points à clarifier.

### 6. Préparer les questions d'entretien

- [ ] Les questions sont ouvertes.
- [ ] Les questions ne sont pas orientées.
- [ ] Les questions permettent de confirmer ou nuancer les hypothèses.
- [ ] Les questions couvrent motivations, compétences, contraintes et priorités.

### 7. Décider ce qui peut passer en synthèse finale

- [ ] Les hypothèses validées peuvent être reprises.
- [ ] Les hypothèses fragiles restent exclues.
- [ ] Les éléments sensibles sont reformulés avec prudence.
- [ ] La synthèse finale reste professionnelle, contextualisée et humaine.

## Tableau de décision consultant

| Élément IA | Décision consultant | Action |
|---|---|---|
| Thème récurrent confirmé | Conserver | Préparer une formulation synthèse |
| Thème faible | Nuancer | Transformer en question d'entretien |
| Hypothèse professionnelle plausible | Explorer | Préparer une validation bénéficiaire |
| Hypothèse trop forte | Rejeter | Supprimer du livrable final |
| Contrainte déclarée | Vérifier | Clarifier en entretien |
| Signal sensible | Prudence | Reformuler ou exclure |

## Questions d'entretien génériques

- Qu'est-ce qui vous paraît le plus juste dans cette première lecture ?
- Quels éléments souhaitez-vous nuancer ou corriger ?
- Quelles compétences reconnaissez-vous comme réellement mobilisées ?
- Quelles contraintes sont incontournables ?
- Quelles contraintes sont négociables ?
- Quelles pistes vous semblent réalistes à court terme ?
- Quelles pistes vous semblent motivantes mais encore floues ?
- Quels points doivent être clarifiés avant de retenir un projet ?

## Critère de passage vers FinalSynthesis

Un élément issu du brouillon IA peut passer vers `FinalSynthesis` uniquement si :

```text
source identifiable
  + formulation prudente
  + validation consultant
  + validation ou clarification bénéficiaire
```

## Statut

```text
REQUIRED HUMAN REVIEW STEP
```
