using FormulaPredictions.Controllers.Base;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

public class ConfigController : BasePredictionsController<ConfigController>
{
    public ConfigController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<ConfigController> logger) 
        : base(jsonParser, config, logger)
    {
    }

    [HttpGet(Name = "GetConfig")]
    public GeneralConfig Get()
    {
        try
        {
            return _config;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching teams");

            return new();
        }
    }
}
