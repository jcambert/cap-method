using CapMethod.Saas.Application.Ports;

namespace CapMethod.Saas.Infrastructure.CapEngine;

public sealed class LocalCapEngineAdapter : ICapEnginePort
{
    public Task<CapEngineResult<CapArtifactRef>> BuildResponseSessionAsync(
        BuildResponseSessionCommand command,
        CancellationToken cancellationToken)
    {
        CapArtifactRef artifact = new(
            command.TenantId,
            command.CapSessionId,
            "ResponseSession",
            $"{command.TenantId}/{command.CapSessionId}/response-session.json",
            DateTimeOffset.UtcNow);

        return Task.FromResult(CapEngineResult<CapArtifactRef>.Success(artifact));
    }

    public Task<CapEngineResult<CapArtifactRef>> GenerateAnalysisSnapshotAsync(
        GenerateAnalysisSnapshotCommand command,
        CancellationToken cancellationToken)
    {
        if (command.ResponseSession.TenantId != command.TenantId)
        {
            return Task.FromResult(CapEngineResult<CapArtifactRef>.Failure("ResponseSession tenant mismatch."));
        }

        CapArtifactRef artifact = new(
            command.TenantId,
            command.CapSessionId,
            "AnalysisSnapshot",
            $"{command.TenantId}/{command.CapSessionId}/analysis-snapshot.json",
            DateTimeOffset.UtcNow);

        return Task.FromResult(CapEngineResult<CapArtifactRef>.Success(artifact));
    }

    public Task<CapEngineResult<CapArtifactRef>> GenerateDeliverablePackageAsync(
        GenerateDeliverablePackageCommand command,
        CancellationToken cancellationToken)
    {
        if (command.FinalSynthesis.TenantId != command.TenantId)
        {
            return Task.FromResult(CapEngineResult<CapArtifactRef>.Failure("FinalSynthesis tenant mismatch."));
        }

        if (command.ActionPlan.TenantId != command.TenantId)
        {
            return Task.FromResult(CapEngineResult<CapArtifactRef>.Failure("ActionPlan tenant mismatch."));
        }

        CapArtifactRef artifact = new(
            command.TenantId,
            command.CapSessionId,
            "DeliverablePackage",
            $"{command.TenantId}/{command.CapSessionId}/deliverable-package.zip",
            DateTimeOffset.UtcNow);

        return Task.FromResult(CapEngineResult<CapArtifactRef>.Success(artifact));
    }
}
