using F1Predictions.Core.Config;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace F1Predictions.Core.Services
{
    public class WebApiRequest : IWebApiRequest
    {
        private readonly ApiEndpointConfig _config;
        private readonly IHttpClientFactory _clientFactory;

        public WebApiRequest(IOptions<ApiEndpointConfig> config, IHttpClientFactory clientFactory)
        {
            _config = config.Value;
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAsync<T>(ApiEndpoint apiEndpoint, params object[] parameters)
        {
            var client = _clientFactory.CreateClient();
            var endpoint = GetEndpoint(apiEndpoint);

            if (parameters is not null && parameters.Length > 0)
                endpoint = string.Format(endpoint, parameters);

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.BaseUrl}{endpoint}");
            var response = await client.SendAsync(request);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // TODO: check status code
            using var Stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(Stream, options) ?? throw new InvalidCastException("Could not deserialize response stream.");
        }

        private string GetEndpoint(ApiEndpoint endpoint) =>
            endpoint switch
            {
                ApiEndpoint.Competitors => _config.Competitors,
                ApiEndpoint.Teams => _config.Teams,
                ApiEndpoint.Tracks => _config.Tracks,
                ApiEndpoint.Drivers => _config.Drivers,
                ApiEndpoint.Questions => _config.Questions,
                _ => throw new InvalidOperationException($"The endpoint {endpoint} has not been defined.")
            };
    }
}
