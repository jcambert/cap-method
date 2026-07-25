# Lot 12 — Observabilité minimale

## Statut

`IMPLEMENTED - CI TO VERIFY`

## Objectif

Fournir un socle d'exploitation permettant de corréler une requête, diagnostiquer un échec et restituer une erreur compréhensible sans exposer de données sensibles.

## Livrables

- middleware de corrélation `CorrelationIdMiddleware` ;
- prise en charge de l'en-tête `X-Correlation-ID` ;
- génération automatique d'un identifiant lorsque l'appelant n'en fournit pas ;
- propagation de l'identifiant dans la réponse HTTP ;
- enrichissement des activités OpenTelemetry ;
- scope de journalisation structuré par requête ;
- durée, méthode, chemin et statut HTTP journalisés ;
- gestion centralisée des exceptions avec `IExceptionHandler` ;
- réponses RFC 7807 `ProblemDetails` ;
- identifiant de corrélation et horodatage dans les extensions ;
- message client neutre ;
- détail technique réservé aux journaux serveur ;
- endpoint de panne volontaire disponible uniquement en développement ;
- tests HTTP de génération, propagation et gestion des erreurs.

## Données journalisées

```text
CorrelationId
RequestMethod
RequestPath
StatusCode
ElapsedMilliseconds
Exception côté serveur uniquement
```

Les corps HTTP, réponses de questionnaires, synthèses, plans d'action, jetons, mots de passe, codes d'accès et chaînes de connexion ne sont pas journalisés par ce lot.

## Diagnostic d'une erreur

1. relever `X-Correlation-ID` dans la réponse ou `correlationId` dans le `ProblemDetails` ;
2. rechercher cet identifiant dans les journaux centralisés ;
3. utiliser la trace OpenTelemetry associée ;
4. analyser l'exception serveur sans demander au bénéficiaire de transmettre ses données métier.

## Endpoint de diagnostic

En environnement `Development` uniquement :

```text
GET /api/dev/diagnostics/failure
```

Il permet de vérifier la chaîne complète : exception, journal serveur, statut 500 et `ProblemDetails` sûr.

## Validation

```bash
dotnet restore src/CapMethod.Saas/CapMethod.Saas.slnx
dotnet build src/CapMethod.Saas/CapMethod.Saas.slnx --no-restore
dotnet test src/CapMethod.Saas/CapMethod.Saas.slnx --no-build
```

La CI doit également exécuter les tests serveur et Aspire.

## Limites assumées

- aucun backend propriétaire de logs imposé ;
- aucun tableau de bord de production imposé ;
- aucune alerte métier automatique ;
- audit des actions sensibles traité au Lot 13.

Le socle reste compatible avec les exporteurs OpenTelemetry configurés par l'environnement d'hébergement.