# Final Release Checklist - v3.0-saas

## Statut global

```text
PUBLISHED - STABLE RELEASE
```

## 1. Préconditions

```text
[OK] v3.0-saas-rc1 publié
[OK] Documentation RC publiée sur main
[OK] Lots 0 à 15 intégrés
[OK] CI v3-saas-validation existante
[OK] CI de stabilisation finale OK
[OK] PR de stabilisation fusionnée dans main
[OK] Tag v3.0-saas créé
```

## 2. Vérifications techniques finales

```text
[OK] dotnet restore Server OK
[OK] dotnet restore Client OK
[OK] dotnet build Server OK
[OK] dotnet build Client OK
[OK] tests Domain OK
[OK] tests Application OK
[OK] tests Infrastructure OK
[OK] tests Compatibility OK
```

## 3. Vérifications fonctionnelles finales

```text
[OK] Connexion dev JWT possible
[OK] Lecture /api/me possible
[OK] Création bénéficiaire possible
[OK] Création session CAP possible
[OK] Liste sessions CAP possible
[OK] Détail session CAP possible
[OK] TenantId non saisi côté UI
[OK] TenantId non passé en query string métier
```

## 4. Vérifications sécurité / tenant

```text
[OK] Endpoints métier protégés par JWT
[OK] Tenant résolu côté serveur
[OK] Utilisateur résolu côté serveur
[OK] Fallback dev limité à Development
[OK] Session hors tenant non exposée
```

## 5. Vérifications documentation

```text
[OK] Statut RC publié
[OK] Notes RC publiées
[OK] Statut final présent
[OK] Checklist finale présente
[OK] Release notes finales prêtes
[OK] Commande de tag final documentée
[OK] Version stable documentée comme publiée
```

## 6. Décision de tag final

Le tag `v3.0-saas` a été créé après validation explicite.

```text
Tag final = v3.0-saas
Commit taggé = 8786df65125debec2b45662851ecd310f167bec8
Statut = PUBLISHED - STABLE RELEASE
```

## Commande utilisée

```bash
git checkout main
git pull origin main

git tag -a v3.0-saas 8786df65125debec2b45662851ecd310f167bec8 -m "Release v3.0-saas"

git push origin v3.0-saas
```
