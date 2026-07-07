# CMDL to Google Forms Mapping

## Purpose

Define how CMDL questionnaire definitions are translated into Google Forms through Apps Script.

## Mapping table

| CMDL type | Google Forms item |
| --- | --- |
| information | SectionHeaderItem or help text |
| text | TextItem |
| longText | ParagraphTextItem |
| email | TextItem with email validation |
| phone | TextItem |
| date | DateItem |
| singleChoice | MultipleChoiceItem |
| multipleChoice | CheckboxItem |
| rating5 | ScaleItem 1 to 5 |
| rating10 | ScaleItem 1 to 10 |
| matrix | GridItem |
| consent | CheckboxItem |
| fileReference | TextItem or FileUploadItem when allowed |

## Form metadata

CMDL root metadata maps to Google Forms configuration:

- title maps to form title;
- description maps to form description;
- language remains metadata only;
- estimated duration remains documentation metadata;
- status controls whether publication is allowed.

## Section mapping

Each CMDL section becomes either:

- a section header for the first section;
- a page break for later sections.

## Required fields

The CMDL `required` property maps to Apps Script `setRequired(true)`.

## Options

CMDL `options` map to `setChoiceValues` for choice-based questions.

## Known limitations

Google Forms does not support every future CAP Method behavior natively.

Limitations include:

- complex conditional logic;
- advanced scoring;
- rich validation;
- reusable components;
- full business metadata.

These features should stay in CMDL and be implemented by other adapters or post-processing tools.
