using F1Predictions.Api.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Predictions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly IJsonParser _jsonParser;
        private readonly ILogger<DriversController> _logger;

        public DriversController(IJsonParser jsonParser, ILogger<DriversController> logger)
        {
            _jsonParser = jsonParser;
            _logger = logger;
        }

        [HttpGet(Name = "GetDrivers")]
        public async Task<IEnumerable<Driver>> Get()
        {
            try
            {
                return await _jsonParser.ParseAsync<IEnumerable<Driver>>("Data/2022/drivers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching drivers");

                return Enumerable.Empty<Driver>();
            }
        }
    }
}
