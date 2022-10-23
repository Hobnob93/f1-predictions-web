using F1Predictions.Api.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Predictions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompetitorsController : ControllerBase
    {
        private readonly IJsonParser _jsonParser;
        private readonly ILogger<CompetitorsController> _logger;

        public CompetitorsController(IJsonParser jsonParser, ILogger<CompetitorsController> logger)
        {
            _jsonParser = jsonParser;
            _logger = logger;
        }

        [HttpGet(Name = "GetCompetitors")]
        public async Task<IEnumerable<Competitor>> Get()
        {
            try
            {
                return await _jsonParser.ParseAsync<IEnumerable<Competitor>>("Data/2022/competitors");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching competitors");

                return Enumerable.Empty<Competitor>();
            }
        }
    }
}
