using CapMethod.Saas.Application.Sessions;
using CapMethod.Saas.Infrastructure;
using CapMethod.Saas.Shared.Api;
using CapMethod.Saas.Shared.CapSessions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCapMethodSaasInfrastructure();
builder.Services.AddScoped<CreateCapSessionUseCase>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

WebApplication app = builder.Build();

app.UseCors();

app.MapGet("/api/info", () => new ApiInfo(
    "CAP Method SaaS",
    "v3.0-saas",
    "Blazor WebAssembly hosted",
    AzureRequired: false,
    AiRequired: false));

app.MapPost("/api/cap-sessions", async (
    CreateCapSessionRequest request,
    CreateCapSessionUseCase useCase,
    CancellationToken cancellationToken) =>
{
    CreateCapSessionCommand command = new(
        request.TenantId,
        request.BeneficiaryId,
        request.ConsultantId,
        request.EnableAi);

    CreateCapSessionResult result = await useCase.ExecuteAsync(command, cancellationToken);

    CapSessionResponse response = new(
        result.CapSessionId,
        result.TenantId,
        result.BeneficiaryId,
        result.ConsultantId,
        result.Status,
        result.IsAiEnabled,
        result.CreatedAtUtc);

    return Results.Created($"/api/cap-sessions/{response.CapSessionId}", response);
});

app.Run();
