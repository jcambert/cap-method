# Professional DOCX Styling

## Purpose

The DOCX export now includes a minimal professional Word styling layer.

The goal is to move from raw DOCX generation to a more presentable consultant document.

## Current features

The DOCX package now contains:

```text
word/document.xml
word/styles.xml
```

The export includes:

- a simple title page;
- CAP Method subtitle and metadata;
- styled heading levels;
- body text style;
- bullet style;
- checklist style;
- quote style;
- monospace style for Markdown table rows and code-like lines.

## Style IDs

```text
Title
CapSubtitle
CapMeta
Heading1
Heading2
Heading3
CapBullet
CapChecklist
CapTableText
CapCode
CapQuote
Normal
```

## Current limitations

The export is still intentionally lightweight:

- Markdown tables are not native Word tables yet;
- no header/footer yet;
- no page numbering yet;
- no logo or graphical title page yet;
- no configurable Word template yet.

## Next target

Improve table rendering:

```text
Markdown table rows
  ↓
Word native tables
```

This is the next visible quality improvement for the final synthesis and action plan documents.
