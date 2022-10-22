using F1Predictions.Api.Interfaces;
using F1Predictions.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJsonParser, JsonParser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt => opt
    .AllowAnyOrigin()
    .WithMethods("GET", "POST", "OPTIONS")
    .WithHeaders("Authorization", "Content-Type"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
