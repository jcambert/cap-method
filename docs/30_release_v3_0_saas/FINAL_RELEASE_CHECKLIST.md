# Final Release Checklist - v3.0-saas

## Statut global

```text
FINAL STABILIZATION - TO VERIFY
```

## 1. Préconditions

```text
[OK] v3.0-saas-rc1 publié
[OK] Documentation RC publiée sur main
[OK] Lots 0 à 15 intégrés
[OK] CI v3-saas-validation existante
[TODO] CI de stabilisation finale OK
[TODO] PR de stabilisation fusionnée dans main
```

## 2. Vérifications techniques finales

```text
[TODO] dotnet restore Server OK
[TODO] dotnet restore Client OK
[TODO] dotnet build Server OK
[TODO] dotnet build Client OK
[TODO] tests Domain OK
[TODO] tests Application OK
[TODO] tests Infrastructure OK
[TODO] tests Compatibility OK
```

## 3. Vérifications fonctionnelles finales

```text
[TODO] Connexion dev JWT possible
[TODO] Lecture /api/me possible
[TODO] Création bénéficiaire possible
[TODO] Création session CAP possible
[TODO] Liste sessions CAP possible
[TODO] Détail session CAP possible
[TODO] TenantId non saisi côté UI
[TODO] TenantId non passé en query string métier
```

## 4. Vérifications sécurité / tenant

```text
[TODO] Endpoints métier protégés par JWT
[TODO] Tenant résolu côté serveur
[TODO] Utilisateur résolu côté serveur
[TODO] Fallback dev limité à Development
[TODO] Session hors tenant non exposée
```

## 5. Vérifications documentation

```text
[OK] Statut RC publié
[OK] Notes RC publiées
[TODO] Statut final présent
[TODO] Checklist finale présente
[TODO] Release notes finales prêtes
[TODO] Commande de tag final documentée
```

## 6. Décision de tag final

Le tag `v3.0-saas` peut être créé uniquement lorsque :

```text
CI main OK après stabilisation
Aucun bug bloquant ouvert
Checklist finale validée
Release notes finales validées
Décision explicite de tag donnée
```

## Commande de tag recommandée

```bash
git checkout main
git pull origin main

git tag -a v3.0-saas <MAIN_SHA_VALIDATED> -m "Release v3.0-saas"

git push origin v3.0-saas
```
