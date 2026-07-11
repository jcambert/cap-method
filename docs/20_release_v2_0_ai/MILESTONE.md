# CAP Method v2.0-ai - Jalon IA assistée

## Statut

```text
FROZEN SCOPE - FUTURE MILESTONE
```

Le jalon `v2.0-ai` est figé comme évolution future de CAP Method.

Il ne modifie pas le périmètre publié de `v1.0-pro`.

## Objectif

`v2.0-ai` introduit une analyse assistée par IA dans CAP Method.

L'objectif n'est pas de remplacer le consultant, mais de l'aider à lire, structurer, reformuler et questionner les réponses du bénéficiaire.

## Principe directeur

```text
L'IA assiste l'analyse.
Le consultant valide l'interprétation.
```

Aucune conclusion IA ne doit être considérée comme définitive sans validation humaine.

## Position dans la chaîne CAP

La chaîne cible devient :

```text
ResponseSession
  ↓
AnalysisSnapshot
  ↓
AIAnalysisDraft
  ↓
ConsultantReview
  ↓
FinalSynthesis
  ↓
ActionPlan
  ↓
Exports DOCX/PDF
  ↓
ZIP final
```

## Périmètre inclus

### Analyse IA assistée

Le jalon doit permettre de produire un fichier :

```text
ai-analysis-draft.md
```

Ce fichier contient une pré-analyse structurée destinée au consultant.

Sections attendues :

1. synthèse neutre des réponses ;
2. thèmes récurrents ;
3. valeurs exprimées ;
4. motivations apparentes ;
5. compétences évoquées ;
6. contraintes et freins ;
7. hypothèses professionnelles ;
8. incohérences ou zones à clarifier ;
9. questions d'entretien ;
10. risques d'interprétation ;
11. points de validation consultant.

### Traçabilité

Chaque analyse IA doit conserver :

- la source utilisée ;
- la date de génération ;
- le modèle ou fournisseur utilisé ;
- les règles méthodologiques appliquées ;
- les limites de la génération ;
- le statut de validation consultant.

### Sécurité méthodologique

Les sorties IA doivent utiliser des formulations prudentes :

```text
Les réponses suggèrent...
Une hypothèse possible est...
Ce point mérite validation...
Le consultant pourra explorer...
```

Elles ne doivent pas utiliser de formulations définitives :

```text
Cette personne est...
Le bon métier est...
Il faut absolument...
Son problème principal est...
```

## Périmètre exclu

`v2.0-ai` exclut explicitement :

- décision automatique d'orientation ;
- diagnostic psychologique ;
- profilage définitif ;
- recommandation métier non validée ;
- scoring opaque ;
- génération automatique remise directement au bénéficiaire ;
- suppression de la validation consultant ;
- traitement de données sensibles sans cadrage explicite ;
- automatisation juridique ou réglementaire.

## Livrables attendus

Le jalon devra produire :

```text
questionnaire-engine/ai/
questionnaire-engine/tools/generate-ai-analysis-draft.mjs
questionnaire-engine/ai/AI_ANALYSIS_DRAFT.md
questionnaire-engine/ai/PROMPT_GUARDRAILS.md
questionnaire-engine/ai/CONSULTANT_VALIDATION.md
```

## Contrôles qualité

La CI devra vérifier :

- présence de la documentation IA ;
- génération d'un `ai-analysis-draft.md` de test ;
- présence des sections obligatoires ;
- présence d'un avertissement de validation consultant ;
- absence de termes interdits dans les sorties de test ;
- conservation de la chaîne `v1.0-pro`.

## Critères d'acceptation

`v2.0-ai` sera considéré terminé lorsque :

- l'analyse IA est générée depuis `AnalysisSnapshot` ou `ResponseSession` ;
- le fichier `ai-analysis-draft.md` est structuré ;
- la validation consultant est obligatoire ;
- les prompts sont documentés ;
- les limites sont explicites ;
- la CI protège les garde-fous principaux ;
- la release documente clairement que l'IA est une assistance, pas une décision.

## Décision

Ce jalon est figé comme prochaine grande évolution IA.

Les travaux préparatoires peuvent commencer dans une branche dédiée, mais `v1.0-pro` reste la version stable publiée.
