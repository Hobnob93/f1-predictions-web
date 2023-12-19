using FormulaPredictions.Client.Pages;
using FormulaPredictions.Components;
using FormulaPredictions.RCL.State;
using FormulaPredictions.Services.Implementations;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services
    .AddSwaggerGen()
    .AddMudServices()
    .AddControllers();

builder.Services
    .Configure<GeneralConfig>(config.GetSection(GeneralConfig.SectionName));

builder.Services
    .AddTransient<IJsonParser, JsonParser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Formula Predictions API V1");
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Home).Assembly)
    .AddAdditionalAssemblies(typeof(CascadingState).Assembly);

app.MapControllers();

app.Run();
