namespace F1Predictions.Core.Config
{
    public class ApiEndpointConfig
    {
        public const string Section = "ApiEndpoints";

        public string BaseUrl { get; set; } = string.Empty;
        public string Teams { get; set; } = string.Empty;
    }
}
