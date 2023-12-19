using FormulaPredictions.Controllers.Base;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

public class CompetitorsController : BasePredictionsController<CompetitorsController>
{
    public CompetitorsController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<CompetitorsController> logger)
        : base(jsonParser, config, logger)
    {
    }

    [HttpGet(Name = "GetCompetitors")]
    public async Task<IEnumerable<Competitor>> Get()
    {
        try
        {
            var competitors = (await _jsonParser.ParseFileAsync<IEnumerable<Competitor>>($"{_config.DataBasePath}/competitors"))
                .OrderBy(c => c.Id)
                .ToList();

            for (var i = 0; i < competitors.Count; i++)
                competitors[i].Index = i;

            return competitors;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching competitors");

            return Enumerable.Empty<Competitor>();
        }
    }
}
