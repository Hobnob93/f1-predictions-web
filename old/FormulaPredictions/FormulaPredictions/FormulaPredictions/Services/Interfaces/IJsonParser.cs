namespace FormulaPredictions.Services.Interfaces;

public interface IJsonParser
{
    Task<T> ParseFileAsync<T>(string filePath) where T : class;
}
