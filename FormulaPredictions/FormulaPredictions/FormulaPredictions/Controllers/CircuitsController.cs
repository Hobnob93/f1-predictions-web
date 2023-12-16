using FormulaPredictions.Controllers.Base;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

public class CircuitsController : BasePredictionsController<CircuitsController>
{
    public CircuitsController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<CircuitsController> logger) 
        : base(jsonParser, config, logger)
    {
    }

    [HttpGet(Name = "GetCircuits")]
    public async Task<IEnumerable<Circuit>> Get()
    {
        try
        {
            return await _jsonParser.ParseFileAsync<IEnumerable<Circuit>>($"Data/{_config.Year}/tracks");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching drivers");

            return Enumerable.Empty<Circuit>();
        }
    }
}
