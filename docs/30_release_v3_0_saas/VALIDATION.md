# Validation - CAP Method v3.0-saas

## Statut

```text
BACKLOG VALIDATED
```

Le backlog fonctionnel `v3.0-saas` est validé.

Le document de référence est :

```text
docs/30_release_v3_0_saas/USER_STORIES.md
```

## Décision

Les user stories de `v3.0-saas` sont validées comme base de cadrage produit.

Le développement SaaS ne démarre pas immédiatement.

Il reste positionné après :

1. l'exploitation terrain de `v1.0-pro` ;
2. le développement et la stabilisation de `v2.0-ai` ;
3. la validation du MVP SaaS minimal.

## Branche recommandée future

```text
product/v3-saas-foundation
```

ou :

```text
feature/v3-saas
```

## Règles de démarrage futur

Avant développement SaaS :

- valider les retours terrain de `v1.0-pro` ;
- stabiliser ou cadrer suffisamment `v2.0-ai` ;
- confirmer le MVP minimal ;
- ne pas démarrer par la totalité du SaaS ;
- commencer par le Lot 1 SaaS foundation.

## Lot 1 validé pour cadrage futur

```text
US-SAAS-001
US-SAAS-002
US-SAAS-003
US-SAAS-004
US-SAAS-005
US-SAAS-006
US-SAAS-009
US-SAAS-010
```

## Objectif du premier lot SaaS

Poser le socle minimal :

```text
Identity
Tenant
Consultant
Beneficiary
CapSession
```

Aucun développement SaaS ne doit contourner la validation consultant ni casser la compatibilité avec le moteur CAP.

## Validation produit

Le jalon `v3.0-saas` est validé comme trajectoire produit stratégique, mais il n'est pas le prochain chantier de développement.
