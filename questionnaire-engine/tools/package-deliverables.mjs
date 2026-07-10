import fs from 'node:fs';
import path from 'node:path';
import zlib from 'node:zlib';

const crcTable = buildCrcTable();

const defaultPackageFolder = 'questionnaire-engine/deliverables/packages/CAP-DELIVERABLES-sample-session';
const packageFolder = process.argv[2] ?? defaultPackageFolder;
const outputFolder = process.argv[3] ?? path.dirname(packageFolder);

const manifestPath = path.join(packageFolder, 'manifest.json');
const manifest = readJson(manifestPath);
const zipName = `${manifest.id}.zip`;
const zipPath = path.join(outputFolder, zipName);

const entries = collectFiles(packageFolder).map(filePath => ({
  path: path.relative(path.dirname(packageFolder), filePath).replaceAll(path.sep, '/'),
  content: fs.readFileSync(filePath)
}));

fs.mkdirSync(outputFolder, { recursive: true });
fs.writeFileSync(zipPath, createZip(entries));

manifest.files.packageZip = zipPath;
manifest.checks.packageGenerated = true;
manifest.checks.readyForDelivery = Boolean(
  manifest.checks.markdownGenerated &&
  manifest.checks.exportsGenerated &&
  manifest.checks.pdfGenerated &&
  manifest.checks.packageGenerated
);
manifest.lastPackagedAt = new Date().toISOString();

fs.writeFileSync(manifestPath, JSON.stringify(manifest, null, 2), 'utf8');

console.log('Final ZIP package generated');
console.log(`ZIP: ${zipPath}`);
console.log(`Ready for delivery: ${manifest.checks.readyForDelivery}`);

function collectFiles(folder) {
  const result = [];
  for (const entry of fs.readdirSync(folder, { withFileTypes: true })) {
    const entryPath = path.join(folder, entry.name);
    if (entry.isDirectory()) {
      result.push(...collectFiles(entryPath));
      continue;
    }
    if (entry.isFile()) {
      result.push(entryPath);
    }
  }
  return result.sort();
}

function createZip(entries) {
  const fileRecords = [];
  const centralRecords = [];
  let offset = 0;

  for (const entry of entries) {
    const name = Buffer.from(entry.path, 'utf8');
    const data = Buffer.isBuffer(entry.content) ? entry.content : Buffer.from(entry.content, 'utf8');
    const compressed = zlib.deflateRawSync(data);
    const crc = crc32(data);
    const localHeader = Buffer.alloc(30);

    localHeader.writeUInt32LE(0x04034b50, 0);
    localHeader.writeUInt16LE(20, 4);
    localHeader.writeUInt16LE(0, 6);
    localHeader.writeUInt16LE(8, 8);
    localHeader.writeUInt16LE(0, 10);
    localHeader.writeUInt16LE(0, 12);
    localHeader.writeUInt32LE(crc, 14);
    localHeader.writeUInt32LE(compressed.length, 18);
    localHeader.writeUInt32LE(data.length, 22);
    localHeader.writeUInt16LE(name.length, 26);
    localHeader.writeUInt16LE(0, 28);

    fileRecords.push(localHeader, name, compressed);

    const centralHeader = Buffer.alloc(46);
    centralHeader.writeUInt32LE(0x02014b50, 0);
    centralHeader.writeUInt16LE(20, 4);
    centralHeader.writeUInt16LE(20, 6);
    centralHeader.writeUInt16LE(0, 8);
    centralHeader.writeUInt16LE(8, 10);
    centralHeader.writeUInt16LE(0, 12);
    centralHeader.writeUInt16LE(0, 14);
    centralHeader.writeUInt32LE(crc, 16);
    centralHeader.writeUInt32LE(compressed.length, 20);
    centralHeader.writeUInt32LE(data.length, 24);
    centralHeader.writeUInt16LE(name.length, 28);
    centralHeader.writeUInt16LE(0, 30);
    centralHeader.writeUInt16LE(0, 32);
    centralHeader.writeUInt16LE(0, 34);
    centralHeader.writeUInt16LE(0, 36);
    centralHeader.writeUInt32LE(0, 38);
    centralHeader.writeUInt32LE(offset, 42);

    centralRecords.push(centralHeader, name);
    offset += localHeader.length + name.length + compressed.length;
  }

  const centralDirectory = Buffer.concat(centralRecords);
  const endRecord = Buffer.alloc(22);
  endRecord.writeUInt32LE(0x06054b50, 0);
  endRecord.writeUInt16LE(0, 4);
  endRecord.writeUInt16LE(0, 6);
  endRecord.writeUInt16LE(entries.length, 8);
  endRecord.writeUInt16LE(entries.length, 10);
  endRecord.writeUInt32LE(centralDirectory.length, 12);
  endRecord.writeUInt32LE(offset, 16);
  endRecord.writeUInt16LE(0, 20);

  return Buffer.concat([...fileRecords, centralDirectory, endRecord]);
}

function crc32(buffer) {
  let crc = 0xffffffff;
  for (const byte of buffer) {
    crc = (crc >>> 8) ^ crcTable[(crc ^ byte) & 0xff];
  }
  return (crc ^ 0xffffffff) >>> 0;
}

function buildCrcTable() {
  return Array.from({ length: 256 }, (_, index) => {
    let value = index;
    for (let bit = 0; bit < 8; bit += 1) {
      value = value & 1 ? 0xedb88320 ^ (value >>> 1) : value >>> 1;
    }
    return value >>> 0;
  });
}

function readJson(filePath) {
  if (!fs.existsSync(filePath)) {
    throw new Error(`Missing manifest: ${filePath}`);
  }

  return JSON.parse(fs.readFileSync(filePath, 'utf8'));
}
