using CapMethod.Saas.Application.Ports;

namespace CapMethod.Saas.Infrastructure.CapEngine;

public sealed class LocalAiAnalysisAdapter : IAiAnalysisPort
{
    public Task<CapEngineResult<CapArtifactRef>> GenerateAiAnalysisDraftAsync(
        GenerateAiAnalysisDraftCommand command,
        CancellationToken cancellationToken)
    {
        if (!command.IsAiEnabled)
        {
            return Task.FromResult(CapEngineResult<CapArtifactRef>.Failure("AI is disabled for this session."));
        }

        if (command.AnalysisSnapshot.TenantId != command.TenantId)
        {
            return Task.FromResult(CapEngineResult<CapArtifactRef>.Failure("AnalysisSnapshot tenant mismatch."));
        }

        CapArtifactRef artifact = new(
            command.TenantId,
            command.CapSessionId,
            "AIAnalysisDraft",
            $"{command.TenantId}/{command.CapSessionId}/ai-analysis-draft.md",
            DateTimeOffset.UtcNow);

        return Task.FromResult(CapEngineResult<CapArtifactRef>.Success(artifact));
    }

    public Task<CapEngineResult<CapArtifactRef>> GenerateAiAnalysisManifestAsync(
        GenerateAiAnalysisManifestCommand command,
        CancellationToken cancellationToken)
    {
        if (command.AnalysisSnapshot.TenantId != command.TenantId)
        {
            return Task.FromResult(CapEngineResult<CapArtifactRef>.Failure("AnalysisSnapshot tenant mismatch."));
        }

        if (command.AiAnalysisDraft.TenantId != command.TenantId)
        {
            return Task.FromResult(CapEngineResult<CapArtifactRef>.Failure("AIAnalysisDraft tenant mismatch."));
        }

        CapArtifactRef artifact = new(
            command.TenantId,
            command.CapSessionId,
            "AIAnalysisManifest",
            $"{command.TenantId}/{command.CapSessionId}/ai-analysis-manifest.json",
            DateTimeOffset.UtcNow);

        return Task.FromResult(CapEngineResult<CapArtifactRef>.Success(artifact));
    }
}
