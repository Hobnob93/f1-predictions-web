using FormulaPredictions.Shared.Constants;
using System.Net.Http.Json;

namespace FormulaPredictions.Shared.Services.Base;

public abstract class BaseWebApiClient
{
    private readonly IHttpClientFactory _httpFactory;

    public BaseWebApiClient(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    private HttpClient CreateClient(string clientName = HttpClientConstants.HttpClientName)
    {
        return _httpFactory.CreateClient(clientName);
    }

    protected async Task<T> TryGet<T>(string endpoint)
    {
        var result = await CreateClient().GetAsync(endpoint);
        result.EnsureSuccessStatusCode();

        return await result.Content.ReadFromJsonAsync<T>()
            ?? throw new InvalidCastException("Data returned from API call was not the expected type");
    }
}
