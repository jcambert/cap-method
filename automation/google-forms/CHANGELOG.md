# Changelog - Google Forms Automation

All notable changes to the CAP Method® Google Forms automation are documented in this file.

## 1.0.0 - Initial automation baseline

### Added

- Added Google Apps Script generator for the CAP Method® questionnaire suite.
- Added automatic creation of 10 Google Forms:
  - FORM-001 - Diagnostic initial du bénéficiaire;
  - FORM-002 - Valeurs professionnelles;
  - FORM-003 - Compétences et réalisations;
  - FORM-004 - Personnalité et mode de fonctionnement;
  - FORM-005 - Intérêts professionnels;
  - FORM-006 - Analyse des pistes professionnelles;
  - FORM-007 - Plan d’action professionnel;
  - FORM-008 - Évaluation intermédiaire;
  - FORM-009 - Évaluation finale du bilan;
  - FORM-010 - Suivi à 6 mois.
- Added automatic creation of one linked Google Sheets response file per generated form.
- Added automatic organization of generated resources inside a dedicated Google Drive folder.
- Added execution logging with form edit URLs, public URLs, and spreadsheet URLs.
- Added README with installation, configuration, production usage, and roadmap.

### Notes

This version keeps the generator as a single Apps Script file because the initial prototype was tested successfully before repository integration.

The next professionalization step is to externalize questionnaire definitions into structured JSON or YAML files and keep Apps Script as a generation engine only.
