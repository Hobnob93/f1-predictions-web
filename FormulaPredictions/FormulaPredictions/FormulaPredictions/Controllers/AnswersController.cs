using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IJsonParser _jsonParser;
    private readonly GeneralConfig _config;
    private readonly ILogger<AnswersController> _logger;

    public AnswersController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<AnswersController> logger)
    {
        _jsonParser = jsonParser;
        _config = config.Value;
        _logger = logger;
    }

    [HttpGet(Name = "GetAnswers")]
    public async Task<IEnumerable<Answer>> Get()
    {
        try
        {
            return await _jsonParser.ParseFileAsync<IEnumerable<Answer>>($"Data/{_config.Year}/answers");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching answers");

            return Enumerable.Empty<Answer>();
        }
    }
}
