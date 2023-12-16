using FormulaPredictions.Controllers.Base;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

public class DriversController : BasePredictionsController<DriversController>
{
    public DriversController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<DriversController> logger)
        : base(jsonParser, config, logger)
    {
    }

    [HttpGet(Name = "GetDrivers")]
    public async Task<IEnumerable<Driver>> Get()
    {
        try
        {
            return await _jsonParser.ParseFileAsync<IEnumerable<Driver>>($"Data/{_config.Year}/drivers");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching drivers");

            return Enumerable.Empty<Driver>();
        }
    }
}
