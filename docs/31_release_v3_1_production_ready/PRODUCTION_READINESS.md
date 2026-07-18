# Production Readiness - v3.1-saas-production-ready

## Objectif

Ce document définit les critères minimaux pour considérer `v3.1-saas-production-ready` comme exploitable en production métier.

La v3.1 ne vise pas une plateforme SaaS commerciale complète. Elle vise une application suffisamment sûre, structurée et utilisable pour conduire un bilan de compétences complet.

---

# 1. Sécurité

## Critères obligatoires

```text
Token de développement désactivé hors Development = obligatoire
Endpoints métier protégés = obligatoire
Tenant résolu côté serveur = obligatoire
Accès inter-tenant testé = obligatoire
Secrets non versionnés = obligatoire
Erreurs techniques non exposées côté UI = obligatoire
```

## Points de contrôle

- aucune route métier ne doit dépendre d'un `TenantId` saisi côté UI ;
- les tokens de développement doivent être indisponibles en production ;
- l'espace bénéficiaire doit être cloisonné ;
- les erreurs d'autorisation doivent être explicites sans fuite technique.

---

# 2. Données et persistance

## Critères obligatoires

```text
PostgreSQL utilisé comme référence production = obligatoire
Mode InMemory réservé au local/dev/test = obligatoire
Migrations EF Core présentes = obligatoire
Données métier rattachées au tenant = obligatoire
Données sensibles identifiées = obligatoire
```

## Points de contrôle

- les nouveaux agrégats doivent avoir une stratégie de persistance ;
- chaque requête métier doit filtrer par tenant côté serveur ;
- les migrations doivent être documentées ;
- les données de réponse et livrables doivent être isolées.

---

# 3. Parcours métier

## Critères obligatoires

```text
Création bénéficiaire = obligatoire
Création session CAP = obligatoire
Workflow CAP par étapes = obligatoire
Questionnaires en ligne = obligatoire
Réponses persistées = obligatoire
Suivi d'avancement = obligatoire
Analyse structurée = obligatoire
Synthèse éditable = obligatoire
Plan d'action = obligatoire
Export livrable minimal = obligatoire
```

## Définition d'un bilan exploitable

Un bilan est considéré exploitable si :

- le consultant peut créer le dossier ;
- le bénéficiaire peut répondre ;
- le consultant peut suivre l'avancement ;
- une analyse peut être préparée ;
- une synthèse peut être relue et modifiée ;
- un plan d'action peut être produit ;
- un livrable peut être exporté.

---

# 4. Expérience utilisateur

## Critères obligatoires

```text
Navigation par pages = obligatoire
Tableau de bord consultant = obligatoire
Détail session exploitable = obligatoire
Affichage erreurs métier = obligatoire
État de chargement visible = obligatoire
Actions indisponibles contrôlées = obligatoire
```

## Points de contrôle

- l'utilisateur ne doit pas manipuler directement des identifiants techniques ;
- l'écran principal ne doit plus être un unique écran de démonstration ;
- chaque action métier doit avoir un résultat visible ;
- les erreurs doivent permettre de comprendre quoi corriger.

---

# 5. Qualité technique

## Critères obligatoires

```text
Tests domaine = obligatoire
Tests application = obligatoire
Tests infrastructure = obligatoire
Tests compatibilité CAP v1/v2 = obligatoire
Build client = obligatoire
Build server = obligatoire
CI GitHub Actions verte = obligatoire
Documentation de lot = obligatoire
```

## Règles de conception

- conserver .NET 10 et C# LangVersion 14 ;
- conserver la séparation Domain / Application / Infrastructure / Server / Client / Shared ;
- éviter de centraliser la logique métier dans l'UI ;
- documenter les décisions structurantes ;
- préserver la compatibilité CAP v1 sans IA obligatoire ;
- préserver l'IA optionnelle.

---

# 6. Observabilité minimale

## Critères obligatoires

```text
Logs API exploitables = obligatoire
Erreurs métier distinguées des erreurs techniques = obligatoire
Messages UI non techniques = obligatoire
Aucune donnée sensible en clair dans les logs = obligatoire
```

## Hors périmètre possible v3.1

```text
Monitoring avancé
Alerting complet
Tracing distribué complet
Dashboard exploitation complet
```

Ces éléments pourront être traités en v3.1.1 ou v3.2 si nécessaire.

---

# 7. Critères de release v3.1

La version `v3.1-saas-production-ready` peut être taguée seulement si :

```text
Lots P0 intégrés = oui
Lots P1 intégrés = oui
CI main OK = oui
Documentation finale v3.1 publiée = oui
Checklist production-ready validée = oui
Aucun bug bloquant connu = oui
Parcours bilan complet démontrable = oui
```

## Tag cible

```text
v3.1-saas-production-ready
```
