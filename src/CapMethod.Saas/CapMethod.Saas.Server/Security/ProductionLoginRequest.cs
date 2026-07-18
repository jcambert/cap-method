namespace CapMethod.Saas.Server.Security;

public sealed record ProductionLoginRequest(
    string Email,
    string Password);
