IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ParameterResource> jwtSigningKey =
    builder.AddParameter("jwt-signing-key", secret: true);

IResourceBuilder<PostgresServerResource> postgres =
    builder.AddPostgres("postgres")
        .WithDataVolume();

IResourceBuilder<PostgresDatabaseResource> database =
    postgres.AddDatabase("capmethod-saas-db");

builder.AddProject<Projects.CapMethod_Saas_Server>("capmethod-saas")
    .WithReference(database, "CapMethodSaas")
    .WaitFor(database)
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithEnvironment("Persistence__Provider", "PostgreSql")
    .WithEnvironment("Authentication__Jwt__SigningKey", jwtSigningKey)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

builder.Build().Run();
