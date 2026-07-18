IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ProjectResource> server = builder.AddProject<Projects.CapMethod_Saas_Server>("capmethod-saas-server")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithEnvironment("Persistence__Provider", "InMemory");

builder.AddProject<Projects.CapMethod_Saas_Client>("capmethod-saas-client")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Development")
    .WithReference(server)
    .WaitFor(server);

builder.Build().Run();
