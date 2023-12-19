using FormulaPredictions.Services.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FormulaPredictions.Services.Implementations;

public class JsonParser : IJsonParser
{
    public async Task<T> ParseFileAsync<T>(string filePath) where T : class
    {
        using var openStream = File.OpenRead($"{filePath}.json");

        return await JsonSerializer.DeserializeAsync<T>(openStream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        }) ?? throw new InvalidOperationException("Failed to parse json into target instance");
    }
}
