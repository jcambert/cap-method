# Statut Lot 0 - CAP Method v3.0-saas

## Branche

```text
feature/v3-saas
```

## Statut global

```text
STARTED
```

## Objectif du Lot 0

Verrouiller le cadrage technique avant de coder le socle SaaS.

Le Lot 0 doit garantir :

- une stack open source utilisable professionnellement sans licence applicative payante obligatoire ;
- une architecture Blazor WebAssembly hosted ;
- une portabilité locale sans dépendance forte à Azure ;
- un usage Azure limité au développement / expérimentation ;
- la compatibilité avec `v1.0-pro` sans IA ;
- la compatibilité avec `v2.0-ai` avec IA optionnelle ;
- l'encapsulation du moteur CAP existant via adaptateurs.

## User stories du Lot 0

```text
US-SAAS-000 - Valider la stack open source professionnelle
US-SAAS-032 - Conserver le mode CAP v1 sans IA
US-SAAS-033 - Conserver le mode CAP v2 avec IA optionnelle
US-SAAS-034 - Créer un adaptateur moteur CAP
```

## Décisions déjà validées

### Stack applicative

```text
Blazor WebAssembly hosted
ASP.NET Core
PostgreSQL
EF Core
MudBlazor
```

### Azure

```text
Azure = développement / expérimentation uniquement
```

Azure ne doit pas devenir une dépendance de production obligatoire.

### Compatibilité CAP

```text
CAP v1 sans IA = obligatoire
CAP v2 avec IA optionnelle = obligatoire
```

## Réalisations actuelles

### US-SAAS-000

Statut :

```text
DONE
```

Justification :

```text
docs/30_release_v3_0_saas/TECH_STACK.md
```

La stack est documentée, les licences acceptées sont listées et les modèles à éviter sont explicités.

### US-SAAS-032

Statut :

```text
READY
```

Objectif de développement :

- définir le flux CAP sans IA dans le SaaS ;
- conserver les formats `ResponseSession`, `AnalysisSnapshot`, `SynthesisDraft`, `FinalSynthesis`, `ActionPlan` ;
- conserver les exports DOCX/PDF/ZIP.

### US-SAAS-033

Statut :

```text
READY
```

Objectif de développement :

- définir le mode IA optionnel ;
- conserver `AIAnalysisDraft` ;
- conserver `AIAnalysisManifest` ;
- conserver `ConsultantReview` obligatoire ;
- empêcher toute remise automatique d'un brouillon IA.

### US-SAAS-034

Statut :

```text
READY
```

Objectif de développement :

- définir le port applicatif du moteur CAP ;
- définir les adaptateurs local / Azure dev / tests ;
- isoler le moteur CAP du SaaS ;
- isoler Azure de la logique métier.

## Prochaine étape recommandée

Créer le document d'architecture du socle applicatif `Client / Server / Shared` et le contrat du `CapEngineAdapter`.
