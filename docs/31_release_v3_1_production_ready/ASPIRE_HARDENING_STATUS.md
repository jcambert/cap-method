# Aspire hardening status

## Branche

`feature/v3-1-aspire-hardening`

## Statut

```text
IMPLEMENTED - CI TO VERIFY
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

## Validation attendue

- restore et build de la solution ;
- absence d'alertes de vulnerabilite masquees ;
- lancement du conteneur PostgreSQL ;
- ressource serveur saine ;
- `/health` accessible ;
- `/api/info` accessible ;
- application Blazor accessible ;
- tests distribues verts.

## Suite

Le durcissement general des tests reprend uniquement apres validation de cette PR.
