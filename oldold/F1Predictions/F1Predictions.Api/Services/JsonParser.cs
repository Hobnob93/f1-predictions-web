using F1Predictions.Api.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace F1Predictions.Api.Services
{
    public class JsonParser : IJsonParser
    {
        public async Task<T> ParseAsync<T>(string fileLocation) where T : class
        {
            using var openStream = File.OpenRead($"{fileLocation}.json");

            return await JsonSerializer.DeserializeAsync<T>(openStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            }) ?? throw new InvalidOperationException("Failed to parse json into target instance!");
        }
    }
}
