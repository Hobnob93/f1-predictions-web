using F1Predictions.Api.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Predictions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly IJsonParser _jsonParser;
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(IJsonParser jsonParser, ILogger<TeamsController> logger)
        {
            _jsonParser = jsonParser;
            _logger = logger;
        }

        [HttpGet(Name = "GetTeams")]
        public async Task<IEnumerable<Team>> Get()
        {
            try
            {
                return await _jsonParser.ParseAsync<IEnumerable<Team>>("Data/2022/teams");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching teams");

                return Enumerable.Empty<Team>();
            }
        }
    }
}
