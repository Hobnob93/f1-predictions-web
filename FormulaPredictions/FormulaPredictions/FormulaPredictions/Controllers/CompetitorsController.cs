using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormulaPredictions.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompetitorsController : ControllerBase
{
    private readonly IJsonParser _jsonParser;
    private readonly GeneralConfig _config;
    private readonly ILogger<CompetitorsController> _logger;

    public CompetitorsController(IJsonParser jsonParser, GeneralConfig config, ILogger<CompetitorsController> logger)
    {
        _jsonParser = jsonParser;
        _config = config;
        _logger = logger;
    }

    [HttpGet(Name = "GetCompetitors")]
    public async Task<IEnumerable<Competitor>> Get()
    {
        try
        {
            var competitors = (await _jsonParser.ParseFileAsync<IEnumerable<Competitor>>($"Data/{_config.Year}/competitors"))
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
