# Aspire - environnement distribue de developpement

## Statut

VALIDATED - CI OK

## Architecture

Aspire orchestre maintenant :

- un serveur PostgreSQL avec volume de donnees ;
- la base `capmethod-saas-db` ;
- le serveur ASP.NET Core `capmethod-saas` ;
- le client Blazor WebAssembly heberge par le serveur ;
- un parametre secret pour la cle JWT.

Le serveur attend PostgreSQL, recoit la chaine de connexion `CapMethodSaas` et expose un health check.

## Service Defaults

Le projet `CapMethod.Saas.ServiceDefaults` fournit :

- OpenTelemetry pour les traces, metriques et logs ;
- export OTLP lorsqu'il est configure ;
- service discovery ;
- resilience HTTP standard ;
- endpoints `/health` et `/alive`.

## Correction du client

Le client n'est plus lance comme un service independant sans URL API garantie. Il est heberge par ASP.NET Core et utilise la meme origine que l'API.

## Securite

La cle JWT de developpement est un parametre secret Aspire. Elle n'est pas stockee dans le depot.

Cle attendue :

```text
Parameters:jwt-signing-key
```

Valeur locale possible :

```text
CAP_METHOD_LOCAL_DEV_SIGNING_KEY_0123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ
```

Les suppressions globales `NU1902` et `NU1903` ont ete retirees. Les alertes de vulnerabilite ne sont plus masquees.

## Lancement depuis Visual Studio

Scenario nominal :

1. ouvrir `src/CapMethod.Saas/CapMethod.Saas.slnx` dans Visual Studio ;
2. definir `CapMethod.Saas.AppHost` comme projet de demarrage ;
3. selectionner le profil `CapMethod.Saas.AppHost` ;
4. lancer avec F5 ou Ctrl+F5 ;
5. renseigner `jwt-signing-key` dans la fenetre Aspire si le secret local n'existe pas encore ;
6. cocher `Enregistrer dans les secrets utilisateur` pour ne pas le ressaisir.

Le profil Visual Studio est declare dans :

```text
src/CapMethod.Saas/CapMethod.Saas.AppHost/Properties/launchSettings.json
```

## Lancement depuis Aspire CLI

Depuis `src/CapMethod.Saas` :

```bash
aspire run --project CapMethod.Saas.AppHost/CapMethod.Saas.AppHost.csproj
```

Ou depuis le dossier AppHost :

```bash
aspire run
```

Pour preconfigurer le secret avec Aspire CLI depuis le dossier AppHost :

```bash
aspire secret set Parameters:jwt-signing-key CAP_METHOD_LOCAL_DEV_SIGNING_KEY_0123456789_ABCDEFGHIJKLMNOPQRSTUVWXYZ
```

## Lancement avec dotnet

Le lancement direct reste possible, mais n'est pas le scenario principal :

```bash
dotnet run --project src/CapMethod.Saas/CapMethod.Saas.AppHost/CapMethod.Saas.AppHost.csproj
```

## Tests

`CapMethod.Saas.Aspire.Tests` demarre l'AppHost complet avec `Aspire.Hosting.Testing` et verifie :

- la sante du serveur ;
- `/health` ;
- `/api/info` ;
- le chargement du client Blazor ;
- le demarrage avec PostgreSQL.

## Limites

Aspire reste un outil de developpement et de test. Les migrations completes et la persistance des stores des lots 6 a 8 restent prevues au Lot 11.
