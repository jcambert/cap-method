using CapMethod.Saas.Application.Ports;
using CapMethod.Saas.Infrastructure.CapEngine;
using Xunit;

namespace CapMethod.Saas.Infrastructure.Tests.CapEngine;

public sealed class LocalCapEngineAdapterTests
{
    [Fact]
    public async Task BuildResponseSessionAsync_should_return_response_session_artifact()
    {
        LocalCapEngineAdapter adapter = new();
        BuildResponseSessionCommand command = new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        CapEngineResult<CapArtifactRef> result = await adapter.BuildResponseSessionAsync(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.Value);
        Assert.Equal("ResponseSession", result.Value.ArtifactType);
        Assert.Equal(command.TenantId, result.Value.TenantId);
    }

    [Fact]
    public async Task GenerateAnalysisSnapshotAsync_should_fail_when_tenant_mismatch()
    {
        LocalCapEngineAdapter adapter = new();
        CapArtifactRef responseSession = new(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "ResponseSession",
            "wrong-tenant/session/response-session.json",
            DateTimeOffset.UtcNow);
        GenerateAnalysisSnapshotCommand command = new(Guid.NewGuid(), Guid.NewGuid(), responseSession);

        CapEngineResult<CapArtifactRef> result = await adapter.GenerateAnalysisSnapshotAsync(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("ResponseSession tenant mismatch.", result.Errors);
    }
}
