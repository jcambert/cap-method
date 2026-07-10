# Final ZIP Package Command

## Purpose

`package-deliverables.mjs` creates the final ZIP archive for a prepared CAP Method deliverable package.

It packages the full folder:

```text
CAP-DELIVERABLES-{session-id}/
```

including:

```text
source/
exports/
review/
manifest.json
```

## Command

Default command:

```bash
node questionnaire-engine/tools/package-deliverables.mjs
```

Custom command:

```bash
node questionnaire-engine/tools/package-deliverables.mjs \
  questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session \
  questionnaire-engine/deliverables/packages
```

## Output

```text
CAP-DELIVERABLES-{session-id}.zip
```

## Manifest update

The command updates:

```yaml
files:
  packageZip: path
checks:
  packageGenerated: true
  readyForDelivery: boolean
lastPackagedAt: datetime
```

`readyForDelivery` becomes true only when:

```text
markdownGenerated = true
exportsGenerated = true
pdfGenerated = true
packageGenerated = true
```

## Current content

The ZIP contains:

- JSON source files;
- Markdown source files;
- DOCX exports;
- PDF exports;
- review placeholders;
- package manifest.

## Important rule

The ZIP is the distribution package.

Source Markdown remains the source of truth.

If a correction is needed, update Markdown sources, regenerate DOCX/PDF, then rebuild the ZIP.

## Next target

Update global documentation and mark the end-to-end package chain as complete.
