# CAP Method Questionnaire Engine

This folder contains the questionnaire, response import, analysis and deliverable generation automation for CAP Method.

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
SynthesisDraft Markdown
  ↓
FinalSynthesis Markdown
  ↓
ActionPlan Markdown
  ↓
Manifest JSON
```

## Main concepts

- **CMDL**: CAP Method Definition Language.
- **Form definition**: one questionnaire described as YAML.
- **Google Forms generator**: produces Google Apps Script from CMDL definitions.
- **ResponseForm**: normalized response for one form.
- **ResponseSession**: normalized response set for a full beneficiary journey.
- **AnalysisSnapshot**: first structured analysis output for consultant review.
- **SynthesisDraft**: Markdown working document generated for the consultant.
- **FinalSynthesis**: structured final synthesis Markdown document.
- **ActionPlan**: professional action plan Markdown document.
- **Manifest**: JSON index of generated files and metadata.
- **Versioned package**: future export-ready folder separating source, exports and review files.

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
├── deliverables/
└── tools/
    ├── validate-cmdl.mjs
    ├── import-response-csv.mjs
    ├── generate-sample-response-csvs.mjs
    ├── import-response-session.mjs
    ├── analyze-response-session.mjs
    ├── generate-synthesis-draft.mjs
    ├── generate-final-synthesis.mjs
    ├── generate-action-plan.mjs
    └── generate-deliverables.mjs
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
| FinalSynthesis Markdown generation | ✅ Operational |
| ActionPlan Markdown generation | ✅ Operational |
| End-to-end deliverable generation | ✅ Operational |
| CI end-to-end validation | ✅ Operational |
| DOCX/PDF export plan | 🧭 Defined |
| Versioned deliverable package structure | 🧭 Defined |
| Package preparation command | ⏳ Next |
| DOCX/PDF export commands | ⏳ Later |

## Recommended command

Generate the full consultant deliverable pack:

```bash
node questionnaire-engine/tools/generate-deliverables.mjs \
  questionnaire-engine/cmdl/examples \
  questionnaire-engine/responses/generated/samples \
  questionnaire-engine/deliverables/generated/sample-session
```

Generated files:

```text
response-session.json
analysis-snapshot.json
synthesis-draft.md
final-synthesis.md
action-plan.md
manifest.json
```

## Export planning documents

```text
questionnaire-engine/deliverables/EXPORT_PLAN.md
questionnaire-engine/deliverables/VERSIONED_PACKAGE.md
```

Target package structure:

```text
CAP-DELIVERABLES-{session-id}/
├── source/
├── exports/
├── review/
└── manifest.json
```

## Unit commands

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

Generate a final synthesis:

```bash
node questionnaire-engine/tools/generate-final-synthesis.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/synthesis/generated/sample.synthesis-draft.md \
  questionnaire-engine/synthesis/generated/sample.final-synthesis.md
```

Generate an action plan:

```bash
node questionnaire-engine/tools/generate-action-plan.mjs \
  questionnaire-engine/analysis/generated/sample.analysis-snapshot.json \
  questionnaire-engine/synthesis/generated/sample.final-synthesis.md \
  questionnaire-engine/synthesis/generated/sample.action-plan.md
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
  ↓
FinalSynthesis Markdown generation
  ↓
ActionPlan Markdown generation
  ↓
End-to-end deliverable generation
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
- Generated Markdown files are working deliverables, not automatically approved final documents.
- Final synthesis and action plan must remain human-reviewed.
- DOCX/PDF export must happen after Markdown validation.
- PDF files are distribution artifacts, not editable sources.

## Next steps

1. Create `prepare-deliverable-package.mjs`.
2. Generate `source/`, `exports/`, `review/` and enhanced `manifest.json`.
3. Add CI validation for the package structure.
4. Add DOCX export command later.
5. Add PDF export command later.
