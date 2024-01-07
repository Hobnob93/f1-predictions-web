using FormulaPredictions.RCL.Services.Implementations;
using FormulaPredictions.RCL.Services.Interfaces;
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
    .AddTransient<IPredictionsData, PredictionsDataClient>()
    .AddTransient<IQuestionsService, QuestionsService>()
    .AddTransient<IResponsesService, ResponsesService>()
    .AddTransient<IAnswersService, AnswersService>()
    .AddTransient<IScoringSystemFactory, ScoreSystemFactory>()
    .AddTransient<IScoreManager, ScoreManager>();

await builder.Build().RunAsync();
