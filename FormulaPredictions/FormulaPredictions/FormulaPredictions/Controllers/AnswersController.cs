using FormulaPredictions.Controllers.Base;
using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

public class AnswersController : BasePredictionsController<AnswersController>
{
    public AnswersController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<AnswersController> logger)
        : base(jsonParser, config, logger)
    {
    }

    [HttpGet(Name = "GetAnswers")]
    public async Task<IEnumerable<Answer>> Get()
    {
        try
        {
            return await _jsonParser.ParseFileAsync<IEnumerable<Answer>>($"{_config.DataBasePath}/answers");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching answers");

            return Enumerable.Empty<Answer>();
        }
    }
}
