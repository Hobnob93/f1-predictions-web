using FormulaPredictions.Controllers.Base;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

public class QuestionsController : BasePredictionsController<QuestionsController>
{
    public QuestionsController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<QuestionsController> logger)
        : base(jsonParser, config, logger)
    {
    }

    [HttpGet(Name = "GetQuestions")]
    public async Task<IEnumerable<QuestionResponses>> Get()
    {
        try
        {
            return await _jsonParser.ParseFileAsync<IEnumerable<QuestionResponses>>($"{_config.DataBasePath}/questions");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching question responses");

            return Enumerable.Empty<QuestionResponses>();
        }
    }
}
