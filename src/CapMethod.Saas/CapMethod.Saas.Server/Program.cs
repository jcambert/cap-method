using CapMethod.Saas.Shared.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

app.Run();
