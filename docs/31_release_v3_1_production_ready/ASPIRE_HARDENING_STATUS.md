# Aspire hardening status

## Branche

`feature/v3-1-aspire-hardening`

## Statut

```text
VALIDATED - CI OK
```

## Livrables

- Aspire SDK mis a jour en 13.4.6.
- Suppression des suppressions globales NU1902 et NU1903.
- PostgreSQL orchestre avec volume de donnees.
- Base injectee sous `ConnectionStrings:CapMethodSaas`.
- Cle JWT fournie par parametre secret Aspire.
- Serveur conditionne par PostgreSQL et health check.
- Blazor WebAssembly heberge par ASP.NET Core.
- Projet ServiceDefaults.
- OpenTelemetry, service discovery et resilience HTTP.
- Endpoints `/health` et `/alive`.
- Projet de tests distribues Aspire.
- CI et solution `.slnx` mises a jour.
- Documentation Aspire reecrite.

## Validation CI

- Restore solution : OK.
- Build solution : OK.
- Tests existants : OK.
- Test distribue Aspire : OK.
- Conteneur PostgreSQL lance par Aspire : OK.
- Ressource serveur saine : OK.
- `/health` accessible : OK.
- `/api/info` accessible : OK.
- Application Blazor accessible : OK.

## Suite

Le durcissement general des tests peut reprendre apres merge de cette PR.
