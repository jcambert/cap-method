import fs from 'node:fs';
import path from 'node:path';

const defaultInputFolder = 'questionnaire-engine/deliverables/generated/sample-session';
const defaultOutputRoot = 'questionnaire-engine/deliverables/packages';
const defaultPackageVersion = '0.1.0';

const inputFolder = process.argv[2] ?? defaultInputFolder;
const outputRoot = process.argv[3] ?? defaultOutputRoot;
const packageVersion = process.argv[4] ?? defaultPackageVersion;

const inputManifestPath = path.join(inputFolder, 'manifest.json');
const inputManifest = readJson(inputManifestPath);
const packageId = `CAP-DELIVERABLES-${sanitizeSegment(inputManifest.sessionId)}`;
const packageFolder = path.join(outputRoot, packageId);
const sourceFolder = path.join(packageFolder, 'source');
const exportsFolder = path.join(packageFolder, 'exports');
const reviewFolder = path.join(packageFolder, 'review');

fs.mkdirSync(sourceFolder, { recursive: true });
fs.mkdirSync(exportsFolder, { recursive: true });
fs.mkdirSync(reviewFolder, { recursive: true });

const sourceFiles = {
  responseSession: copySource('response-session.json', sourceFolder),
  analysisSnapshot: copySource('analysis-snapshot.json', sourceFolder),
  synthesisDraft: copySource('synthesis-draft.md', sourceFolder),
  finalSynthesis: copySource('final-synthesis.md', sourceFolder),
  actionPlan: copySource('action-plan.md', sourceFolder)
};

const reviewFiles = {
  consultantReview: path.join(reviewFolder, 'consultant-review.md'),
  beneficiaryValidation: path.join(reviewFolder, 'beneficiary-validation.md')
};

fs.writeFileSync(reviewFiles.consultantReview, renderConsultantReview(inputManifest), 'utf8');
fs.writeFileSync(reviewFiles.beneficiaryValidation, renderBeneficiaryValidation(inputManifest), 'utf8');

const packageManifest = buildPackageManifest(inputManifest, packageId, packageFolder, sourceFiles, reviewFiles, packageVersion);
const outputManifestPath = path.join(packageFolder, 'manifest.json');
fs.writeFileSync(outputManifestPath, JSON.stringify(packageManifest, null, 2), 'utf8');

console.log('Deliverable package prepared');
console.log(`Package folder: ${packageFolder}`);
console.log(`Manifest: ${outputManifestPath}`);

function copySource(fileName, targetFolder) {
  const sourcePath = path.join(inputFolder, fileName);
  const targetPath = path.join(targetFolder, fileName);

  if (!fs.existsSync(sourcePath)) {
    throw new Error(`Missing source file: ${sourcePath}`);
  }

  fs.copyFileSync(sourcePath, targetPath);
  return targetPath;
}

function buildPackageManifest(inputManifest, packageId, packageFolder, sourceFiles, reviewFiles, packageVersion) {
  return {
    id: packageId,
    sessionId: inputManifest.sessionId,
    beneficiaryId: inputManifest.beneficiaryId,
    consultantId: inputManifest.consultantId,
    packageVersion,
    generatedAt: new Date().toISOString(),
    reviewStatus: 'generated',
    source: inputManifest.source,
    packageFolder,
    files: {
      source: sourceFiles,
      exports: {
        finalSynthesisDocx: null,
        finalSynthesisPdf: null,
        actionPlanDocx: null,
        actionPlanPdf: null
      },
      review: reviewFiles
    },
    checks: {
      markdownGenerated: true,
      consultantReviewed: false,
      exportsGenerated: false,
      pdfGenerated: false,
      readyForDelivery: false
    },
    coverage: inputManifest.coverage,
    nextReviewSteps: inputManifest.nextReviewSteps ?? []
  };
}

function renderConsultantReview(manifest) {
  return [
    '# Revue consultant',
    '',
    `- Session : ${manifest.sessionId}`,
    `- Bénéficiaire : ${manifest.beneficiaryId}`,
    `- Consultant : ${manifest.consultantId}`,
    '- Date de revue :',
    '- Synthèse finale relue : non',
    '- Plan d action relu : non',
    '- Corrections restantes :',
    '- Décision : à compléter',
    '',
    '## Notes consultant',
    '',
    'À compléter.'
  ].join('\n');
}

function renderBeneficiaryValidation(manifest) {
  return [
    '# Validation bénéficiaire',
    '',
    `- Session : ${manifest.sessionId}`,
    `- Bénéficiaire : ${manifest.beneficiaryId}`,
    '- Date de validation :',
    '- Synthèse reconnue comme fidèle : non',
    '- Plan d action validé : non',
    '- Commentaires :',
    '- Décision : à corriger',
    '',
    '## Commentaires bénéficiaire',
    '',
    'À compléter.'
  ].join('\n');
}

function readJson(filePath) {
  if (!fs.existsSync(filePath)) {
    throw new Error(`Missing manifest: ${filePath}`);
  }

  return JSON.parse(fs.readFileSync(filePath, 'utf8'));
}

function sanitizeSegment(value) {
  return String(value ?? 'unknown')
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .replace(/[^a-zA-Z0-9._-]/g, '-')
    .replace(/-+/g, '-')
    .replace(/^-|-$/g, '');
}
