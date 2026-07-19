using CapMethod.Saas.Client;
using CapMethod.Saas.Client.ActionPlans;
using CapMethod.Saas.Client.Auth;
using CapMethod.Saas.Client.Questionnaires;
using CapMethod.Saas.Client.Synthesis;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(serviceProvider => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddScoped<BrowserTokenStore>();
builder.Services.AddScoped<CapMethodApiClient>();
builder.Services.AddScoped<BeneficiaryQuestionnaireApiClient>();
builder.Services.AddScoped<SynthesisApiClient>();
builder.Services.AddScoped<ActionPlanApiClient>();

await builder.Build().RunAsync();
