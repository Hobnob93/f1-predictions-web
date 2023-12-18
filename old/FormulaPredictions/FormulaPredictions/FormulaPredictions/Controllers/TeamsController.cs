using FormulaPredictions.Controllers.Base;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

public class TeamsController : BasePredictionsController<TeamsController>
{
    public TeamsController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<TeamsController> logger)
        : base(jsonParser, config, logger)
    {
    }

    [HttpGet(Name = "GetTeams")]
    public async Task<IEnumerable<Team>> Get()
    {
        try
        {
            return await _jsonParser.ParseFileAsync<IEnumerable<Team>>($"{_config.DataBasePath}/teams");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching teams");

            return Enumerable.Empty<Team>();
        }
    }
}
