import { spawnSync } from 'node:child_process';
import fs from 'node:fs';
import path from 'node:path';

const defaultCmdlFolder = 'questionnaire-engine/cmdl/examples';
const defaultCsvFolder = 'questionnaire-engine/responses/generated/samples';
const defaultOutputFolder = 'questionnaire-engine/deliverables/generated/sample-session';

const cmdlFolder = process.argv[2] ?? defaultCmdlFolder;
const csvFolder = process.argv[3] ?? defaultCsvFolder;
const outputFolder = process.argv[4] ?? defaultOutputFolder;

const paths = {
  responseSession: path.join(outputFolder, 'response-session.json'),
  analysisSnapshot: path.join(outputFolder, 'analysis-snapshot.json'),
  synthesisDraft: path.join(outputFolder, 'synthesis-draft.md'),
  finalSynthesis: path.join(outputFolder, 'final-synthesis.md'),
  actionPlan: path.join(outputFolder, 'action-plan.md'),
  manifest: path.join(outputFolder, 'manifest.json')
};

fs.mkdirSync(outputFolder, { recursive: true });

run('import-response-session.mjs', [cmdlFolder, csvFolder, paths.responseSession]);
run('analyze-response-session.mjs', [paths.responseSession, paths.analysisSnapshot]);
run('generate-synthesis-draft.mjs', [paths.analysisSnapshot, paths.synthesisDraft]);
run('generate-final-synthesis.mjs', [paths.analysisSnapshot, paths.synthesisDraft, paths.finalSynthesis]);
run('generate-action-plan.mjs', [paths.analysisSnapshot, paths.finalSynthesis, paths.actionPlan]);

const manifest = buildManifest(paths, cmdlFolder, csvFolder, outputFolder);
fs.writeFileSync(paths.manifest, JSON.stringify(manifest, null, 2), 'utf8');

console.log('Deliverables generated');
console.log(`Output folder: ${outputFolder}`);
console.log(`Manifest: ${paths.manifest}`);

function run(scriptName, args) {
  const scriptPath = path.join('questionnaire-engine/tools', scriptName);
  const result = spawnSync(process.execPath, [scriptPath, ...args], {
    stdio: 'inherit'
  });

  if (result.status !== 0) {
    throw new Error(`${scriptName} failed with exit code ${result.status}`);
  }
}

function buildManifest(paths, cmdlFolder, csvFolder, outputFolder) {
  const responseSession = JSON.parse(fs.readFileSync(paths.responseSession, 'utf8'));
  const analysisSnapshot = JSON.parse(fs.readFileSync(paths.analysisSnapshot, 'utf8'));

  return {
    id: `${responseSession.id}.deliverables`,
    sessionId: responseSession.id,
    beneficiaryId: responseSession.beneficiaryId,
    consultantId: responseSession.consultantId,
    generatedAt: new Date().toISOString(),
    source: {
      cmdlFolder,
      csvFolder
    },
    outputFolder,
    status: analysisSnapshot.status === 'ready_for_consultant_review' ? 'generated' : 'generated_with_warnings',
    coverage: analysisSnapshot.coverage,
    files: {
      responseSession: paths.responseSession,
      analysisSnapshot: paths.analysisSnapshot,
      synthesisDraft: paths.synthesisDraft,
      finalSynthesis: paths.finalSynthesis,
      actionPlan: paths.actionPlan
    },
    nextReviewSteps: [
      'Relire la synthese finale',
      'Completer les sections generiques',
      'Valider le projet cible avec le beneficiaire',
      'Ajuster les actions et echeances',
      'Preparer les exports DOCX/PDF lorsque les contenus sont stabilises'
    ]
  };
}
