using F1Predictions.Api.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Predictions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswersController : ControllerBase
    {
        private readonly IJsonParser _jsonParser;
        private readonly ILogger<AnswersController> _logger;

        public AnswersController(IJsonParser jsonParser, ILogger<AnswersController> logger)
        {
            _jsonParser = jsonParser;
            _logger = logger;
        }

        [HttpGet(Name = "GetAnswers")]
        public async Task<IEnumerable<Answer>> Get()
        {
            try
            {
                return await _jsonParser.ParseAsync<IEnumerable<Answer>>("Data/2022/answers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching answers");

                return Enumerable.Empty<Answer>();
            }
        }
    }
}
