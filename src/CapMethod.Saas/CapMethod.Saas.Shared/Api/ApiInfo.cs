namespace CapMethod.Saas.Shared.Api;

public sealed record ApiInfo(
    string Name,
    string Version,
    string Architecture,
    bool AzureRequired,
    bool AiRequired);
