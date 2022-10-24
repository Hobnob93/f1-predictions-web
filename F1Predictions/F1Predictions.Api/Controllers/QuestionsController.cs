using F1Predictions.Api.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Predictions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IJsonParser _jsonParser;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IJsonParser jsonParser, ILogger<QuestionsController> logger)
        {
            _jsonParser = jsonParser;
            _logger = logger;
        }

        [HttpGet(Name = "GetQuestions")]
        public async Task<IEnumerable<QuestionResponse>> Get()
        {
            try
            {
                return await _jsonParser.ParseAsync<IEnumerable<QuestionResponse>>("Data/2022/questions");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching questions");

                return Enumerable.Empty<QuestionResponse>();
            }
        }
    }
}
