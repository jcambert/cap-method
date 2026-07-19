# v3.1-saas-production-ready

## Statut

```text
IN PROGRESS - LOTS 0 À 8 IMPLEMENTÉS
LOT 8 EN ATTENTE DE VALIDATION CI
```

La version `v3.1-saas-production-ready` transforme le socle `v3.0-saas` en application métier exploitable pour conduire un bilan de compétences complet.

## État actuel

| Lot | Objet | Statut |
|---|---|---|
| 0 | Cadrage production-ready | ✅ Validé |
| 1 | Navigation SaaS par pages | ✅ Validé |
| 2 | Authentification production minimale | ✅ Validé |
| 3 | Modèle de workflow CAP | ✅ Validé |
| 4 | UI workflow CAP | ✅ Validé |
| 5 | Espace bénéficiaire sécurisé | ✅ Validé |
| 6 | Questionnaires en ligne | ✅ Validé |
| 7 | Analyse structurée SaaS | ✅ Validé |
| 8 | Synthèse éditable | 🟡 Implémenté, CI à vérifier |
| 9 | Plan d'action | ⏳ À faire |
| 10 | Exports livrables | ⏳ À faire |
| 11 | Configuration production | ⏳ À faire |
| 12 | Observabilité minimale | ⏳ À faire |
| 13 | Audit et sécurité minimale | ⏳ À faire |

## Fonctionnalités disponibles

- navigation applicative consultant ;
- authentification consultant hors token de développement ;
- espace bénéficiaire sécurisé ;
- création et suivi de sessions CAP ;
- workflow CAP pilotable depuis l'interface ;
- questionnaires en ligne avec brouillon et soumission ;
- progression bénéficiaire ;
- analyse structurée déterministe ;
- brouillon de synthèse généré depuis l'analyse ;
- édition consultant et validation humaine explicite ;
- verrouillage de la synthèse après validation ;
- isolation tenant et bénéficiaire côté serveur ;
- solution `.slnx`, packages NuGet centralisés et AppHost Aspire pour le développement ;
- `.gitignore` adapté à .NET, Aspire et aux secrets locaux.

## Limites actuelles

La v3.1 n'est pas encore production-ready.

Points bloquants connus :

- réponses questionnaires stockées en mémoire serveur ;
- analyse structurée générée à la demande et non persistée durablement ;
- synthèse stockée en mémoire serveur, sans historique de versions ;
- plan d'action non livré ;
- exports SaaS non livrés ;
- configuration et migrations PostgreSQL des nouveaux agrégats à compléter ;
- observabilité et audit à finaliser.

## Documents transverses

```text
docs/31_release_v3_1_production_ready/VISION.md
docs/31_release_v3_1_production_ready/USER_STORIES.md
docs/31_release_v3_1_production_ready/BACKLOG.md
docs/31_release_v3_1_production_ready/PRODUCTION_READINESS.md
docs/31_release_v3_1_production_ready/DOCUMENTATION_INDEX.md
```

## Base technique

```text
.NET = 10
C# = LangVersion 14
Solution = src/CapMethod.Saas/CapMethod.Saas.slnx
Packages = src/CapMethod.Saas/Directory.Packages.props
Développement orchestré = Aspire AppHost
Production dépendante d'Aspire = non
```

## Règle de livraison

Aucun lot v3.1 ne doit être fusionné sans :

- périmètre et critères d'acceptation explicites ;
- tests adaptés ;
- documentation de lot mise à jour ;
- documents transverses impactés mis à jour ;
- CI verte ;
- validation explicite.
