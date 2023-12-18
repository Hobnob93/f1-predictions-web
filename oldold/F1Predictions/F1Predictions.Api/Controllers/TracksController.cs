using F1Predictions.Api.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Predictions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly IJsonParser _jsonParser;
        private readonly ILogger<TracksController> _logger;

        public TracksController(IJsonParser jsonParser, ILogger<TracksController> logger)
        {
            _jsonParser = jsonParser;
            _logger = logger;
        }

        [HttpGet(Name = "GetTracks")]
        public async Task<IEnumerable<Track>> Get()
        {
            try
            {
                return await _jsonParser.ParseAsync<IEnumerable<Track>>("Data/2022/tracks");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching drivers");

                return Enumerable.Empty<Track>();
            }
        }
    }
}
