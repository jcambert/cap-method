namespace CapMethod.Saas.Client.Auth;

public sealed record ProductionLoginRequest(
    string Email,
    string Password);
