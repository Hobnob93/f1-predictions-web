using F1Predictions;
using F1Predictions.Core.Config;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using F1Predictions.Core.ScoringSystems;
using F1Predictions.Core.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var config = builder.Configuration;
builder.Services.Configure<ApiEndpointConfig>(config.GetSection(ApiEndpointConfig.Section));

builder.Services.AddTransient<IWebApiRequest, WebApiRequest>();

builder.Services.AddScoped<ICompetitorsDataService, CompetitorsDataService>();
builder.Services.AddScoped<ITeamsDataService, TeamsDataService>();
builder.Services.AddScoped<IDriversDataService, DriversDataService>();
builder.Services.AddScoped<ITracksDataService, TracksDataService>();
builder.Services.AddScoped<IQuestionsDataService, QuestionsDataService>();
builder.Services.AddScoped<IAnswersDataService, AnswersDataService>();
builder.Services.AddScoped<IDataServicesInitializer, DataServicesInitializer>();

builder.Services.AddScoped<IRawCompResponses, RawCompResponses>();
builder.Services.AddScoped<ICompResponses<Driver>, DriverCompResponses>();
builder.Services.AddScoped<ICompResponses<Team>, TeamCompResponses>();
builder.Services.AddScoped<ICompResponses<Track>, TrackCompResponses>();
builder.Services.AddScoped<ICompResponses<HeadToHead>, HeadToHeadCompResponses>();
builder.Services.AddScoped<IMultiCompResponses<DriverTrack>, DriverTrackCompResponses>();
builder.Services.AddScoped<IMultiCompResponses<Driver>, DriverCompResponses>();
builder.Services.AddScoped<IMultiCompResponses<Team>, TeamCompResponses>();
builder.Services.AddScoped<IMultiCompResponses<Track>, TrackCompResponses>();

builder.Services.AddScoped<ICompScoreTrackerFactory, CompScoreTrackerFactory>();
builder.Services.AddScoped<IScoreSystemFactory, ScoreSystemFactory>();
builder.Services.AddScoped<IScoreTracker, ScoreTracker>();

builder.Services.AddScoped<LeaderboardScoringSystem>();
builder.Services.AddScoped<ValueScoringSystem>();

builder.Services.AddScoped<Func<ScoringType, IScoreSystem>>(provider => key =>
{
    return key switch
    {
        ScoringType.Leaderboard => provider.GetService<LeaderboardScoringSystem>(),
        ScoringType.Value => provider.GetService<ValueScoringSystem>(),
        _ => (IScoreSystem?)null
    } ?? throw new InvalidOperationException($"The scoring type '{key}' does not have an associated scoring system");
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddHttpClient();

await builder.Build().RunAsync();
