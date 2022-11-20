using F1Predictions;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
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
builder.Services.AddScoped<IDataServicesInitializer, DataServicesInitializer>();

builder.Services.AddScoped<IRawCompResponses, RawCompResponses>();
builder.Services.AddScoped<ICompResponses<Driver>, DriverCompResponses>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddHttpClient();

await builder.Build().RunAsync();
