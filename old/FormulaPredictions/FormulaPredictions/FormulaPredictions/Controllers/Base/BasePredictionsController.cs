using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class BasePredictionsController<T> : ControllerBase
{
    protected readonly IJsonParser _jsonParser;
    protected readonly GeneralConfig _config;
    protected readonly ILogger<T> _logger;

    public BasePredictionsController(IJsonParser jsonParser, IOptions<GeneralConfig> config, ILogger<T> logger)
    {
        _jsonParser = jsonParser;
        _config = config.Value;
        _logger = logger;
    }
}
