# CAP Method Questionnaire Engine

This folder contains the questionnaire, response import, analysis and synthesis-draft automation for CAP Method.

## Goal

Provide a neutral source of truth and processing chain for CAP Method questionnaires, independent from Google Forms, Google Sheets, Blazor, PDF, DOCX or AI adapters.

The current chain is:

```text
CMDL questionnaire definitions
  ↓
Google Forms / Google Sheets generation
  ↓
CSV response import
  ↓
ResponseSession JSON
  ↓
AnalysisSnapshot JSON
  ↓
Consultant synthesis draft Markdown
```

## Main concepts

- **CMDL**: CAP Method Definition Language.
- **Form definition**: one questionnaire described as YAML.
- **Google Forms generator**: produces Google Apps Script from CMDL definitions.
- **ResponseForm**: normalized response for one form.
- **ResponseSession**: normalized response set for a full beneficiary journey.
- **AnalysisSnapshot**: first structured analysis output for consultant review.
- **SynthesisDraft**: Markdown working document generated for the consultant.

## Current structure

```text
questionnaire-engine/
├── README.md
├── cmdl/
│   ├── specification.md
│   └── examples/
│       ├── FORM-001.cmdl.yaml
│       ├── FORM-002.cmdl.yaml
│       ├── FORM-003.cmdl.yaml
│       ├── FORM-004.cmdl.yaml
│       ├── FORM-005.cmdl.yaml
│       ├── FORM-006.cmdl.yaml
│       ├── FORM-007.cmdl.yaml
│       ├── FORM-008.cmdl.yaml
│       ├── FORM-009.cmdl.yaml
│       └── FORM-010.cmdl.yaml
├── generators/
│   └── google-forms/
├── responses/
├── analysis/
├── synthesis/
└── tools/
    ├── validate-cmdl.mjs
    ├── import-response-csv.mjs
    ├── generate-sample-response-csvs.mjs
    ├── import-response-session.mjs
    ├── analyze-response-session.mjs
    └── generate-synthesis-draft.mjs
```

## Current status

| Area | Status |
|---|---|
| CMDL definitions FORM-001 to FORM-010 | ✅ Operational |
| CMDL validation | ✅ Operational |
| Google Forms generation | ✅ Operational and manually tested |
| Beneficiary test campaign plan | ✅ Ready |
| Response data model | ✅ Defined |
| Normalization rules | ✅ Defined |
| FORM-001 CSV import | ✅ Operational |
| Full ResponseSession import | ✅ Operational |
| AnalysisSnapshot generation | ✅ Operational |
| SynthesisDraft Markdown generation | ✅ Operational |
| CI end-to-end validation | ✅ Operational |
| Final synthesis generator | ⏳ Next |
| Action plan generator | ⏳ Next |

## Main commands

Validate CMDL examples:

```bash
node questionnaire-engine/tools/validate-cmdl.mjs
```

Generate Google Apps Script suite from CMDL:

```bash
node questionnaire-engine/generators/google-forms/generate-google-forms.mjs questionnaire-engine/cmdl/examples
```

Generate sample response CSV files from CMDL:

```bash
node questionnaire-engine/tools/generate-sample-response-csvs.mjs
```

Import one FORM-001 response CSV:

```bash
node questionnaire-engine/tools/import-response-csv.mjs \
  questionnaire-engine/cmdl/examples/FORM-001.cmdl.yaml \
  questionnaire-engine/responses/samples/FORM-001.responses.sample.csv \
  questionnaire-engine/responses/generated/FORM-001.response.normalized.json
```

Import a full response session:

```bash
node questionnaire-engine/tools/import-response-session.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/responses/generated/session.response.normalized.json
```

Generate an analysis snapshot:

```bash
node questionnaire-engine/tools/analyze-response-session.mjs \
  questionnaire-engine/responses/generated/session.response.normalized.json \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json
```

Generate a consultant synthesis draft:

```bash
node questionnaire-engine/tools/generate-synthesis-draft.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/synthesis/generated/sample.synthesis-draft.md
```

## CI validation

The GitHub Actions workflow validates the full chain:

```text
CMDL validation
  ↓
Sample response CSV generation
  ↓
FORM-001 response import
  ↓
Full ResponseSession import
  ↓
AnalysisSnapshot generation
  ↓
SynthesisDraft Markdown generation
```

Workflow:

```text
.github/workflows/cmdl-validation.yml
```

## Important rules

- CMDL remains the source of truth for questionnaire structure.
- Google Forms and Google Sheets are collection channels, not the source of truth.
- Google Sheets column labels are used only as an import mapping mechanism.
- `questionId` and `formId` must come from CMDL.
- `SynthesisDraft` is a consultant working document, not a final automatic bilan.
- Final synthesis and action plan must remain human-reviewed.

## Next steps

1. Create the final synthesis structure.
2. Generate a final synthesis draft from the current `SynthesisDraft`.
3. Create an action plan template.
4. Generate the first action plan draft.
5. Add DOCX/PDF export later, once the Markdown deliverables are stable.
