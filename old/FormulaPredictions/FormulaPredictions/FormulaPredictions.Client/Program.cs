using FormulaPredictions.Shared.Constants;
using FormulaPredictions.Shared.Services.Implementations;
using FormulaPredictions.Shared.Services.Interfaces;
using FormulaPredictions.Shared.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddHttpClient(HttpClientConstants.HttpClientName, client =>
{
    var hostEnv = builder.HostEnvironment;
    client.BaseAddress = new Uri($"{hostEnv.BaseAddress}api");
});

builder.Services
    .AddTransient<IPredictionsData, PredictionsDataClient>();

builder.Services
    .AddCascadingValue(sp =>
    {
        var initialAppData = AppData.CreateDefault();
        return new CascadingValueSource<AppData>(initialAppData, isFixed: false);
    });

await builder.Build().RunAsync();
