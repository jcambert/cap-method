# Statut Lot 15 - Préparation Release Candidate v3 SaaS

## Branche

```text
feature/v3-lot15-release-candidate
```

## Objectif

Préparer la release candidate `v3.0-saas-rc1` avec une documentation de validation claire, traçable et exploitable.

Le Lot 15 doit permettre :

- de synthétiser l'état global de `v3.0-saas` ;
- de documenter les lots intégrés ;
- de produire une checklist release candidate ;
- de produire les notes de release candidate ;
- de documenter les conditions de tag ;
- de préparer la validation finale avant `v3.0-saas-rc1`.

## User stories du Lot 15

```text
US-SAAS-1501 - Ajouter le statut global release candidate
US-SAAS-1502 - Ajouter la checklist release candidate
US-SAAS-1503 - Ajouter les notes de release candidate
US-SAAS-1504 - Documenter les conditions de tag v3.0-saas-rc1
US-SAAS-1505 - Préparer la validation finale avant tag
```

## Règles

```text
Changement fonctionnel API = non
Changement de contrat DTO = non
Changement serveur = non
Changement client = non
Documentation RC = oui
Squash merge obligatoire = oui
Suppression de branche après merge = oui
Tag uniquement après CI main OK = oui
```

## Implémentation actuelle

### US-SAAS-1501 - Ajouter le statut global release candidate

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `RELEASE_CANDIDATE_STATUS.md` ajouté ;
- statut global `RELEASE CANDIDATE - TO VERIFY` documenté ;
- lots 0 à 15 synthétisés.

### US-SAAS-1502 - Ajouter la checklist release candidate

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `RELEASE_CANDIDATE_CHECKLIST.md` ajouté ;
- critères techniques documentés ;
- critères fonctionnels documentés ;
- critères de sécurité documentés ;
- critères de tag documentés.

### US-SAAS-1503 - Ajouter les notes de release candidate

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- `RELEASE_CANDIDATE_NOTES.md` ajouté ;
- contenu inclus documenté ;
- limitations connues documentées ;
- suite post-RC documentée.

### US-SAAS-1504 - Documenter les conditions de tag v3.0-saas-rc1

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- conditions de publication du tag documentées ;
- commande de tag recommandée documentée ;
- règle `CI main OK avant tag` documentée.

### US-SAAS-1505 - Préparer la validation finale avant tag

Statut :

```text
IMPLEMENTED - CI TO VERIFY
```

Réalisé :

- statut Lot 15 créé ;
- validation finale orientée release candidate documentée.

## Statut global

```text
IMPLEMENTED - CI TO VERIFY
```

## Prochaine étape

Attendre la CI automatique, corriger si nécessaire, valider la documentation, ouvrir la PR Lot 15 vers `main`, effectuer le squash merge, puis préparer le tag `v3.0-saas-rc1`.
