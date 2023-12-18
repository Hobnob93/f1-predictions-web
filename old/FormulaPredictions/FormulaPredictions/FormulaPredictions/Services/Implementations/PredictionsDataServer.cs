using FormulaPredictions.Services.Interfaces;
using FormulaPredictions.Shared.Config;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FormulaPredictions.Services.Implementations;

public class PredictionsDataServer : IPredictionsData
{
    private readonly IJsonParser _jsonParser;
    private readonly GeneralConfig _config;

    public PredictionsDataServer(IJsonParser jsonParser, IOptions<GeneralConfig> config)
    {
        _jsonParser = jsonParser;
        _config = config.Value;
    }

    public async Task<Answer[]> GetAnswers()
    {
        return await _jsonParser.ParseFileAsync<Answer[]>($"{_config.DataBasePath}/answers");
    }

    public async Task<Circuit[]> GetCircuits()
    {
        return await _jsonParser.ParseFileAsync<Circuit[]>($"{_config.DataBasePath}/tracks");
    }

    public async Task<Competitor[]> GetCompetitors()
    {
        var competitors = (await _jsonParser.ParseFileAsync<IEnumerable<Competitor>>($"{_config.DataBasePath}/competitors"))
                .OrderBy(c => c.Id)
                .ToArray();

        for (var i = 0; i < competitors.Length; i++)
            competitors[i].Index = i;

        return competitors;
    }

    public async Task<Driver[]> GetDrivers()
    {
        return await _jsonParser.ParseFileAsync<Driver[]>($"{_config.DataBasePath}/drivers");
    }

    public async Task<QuestionResponses[]> GetQuestionResponses()
    {
        return await _jsonParser.ParseFileAsync<QuestionResponses[]>($"{_config.DataBasePath}/questions");
    }

    public async Task<Team[]> GetTeams()
    {
        return await _jsonParser.ParseFileAsync<Team[]>($"{_config.DataBasePath}/teams");
    }
}
