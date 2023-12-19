using FormulaPredictions.Shared.Constants;
using FormulaPredictions.Shared.Services.Implementations;
using FormulaPredictions.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddMudServices()
    .AddHttpClient(HttpClientConstants.HttpClientName, client =>
    {
        var hostEnv = builder.HostEnvironment;
        client.BaseAddress = new Uri($"{hostEnv.BaseAddress}api");
    });

builder.Services
    .AddTransient<IPredictionsData, PredictionsDataClient>();

await builder.Build().RunAsync();
