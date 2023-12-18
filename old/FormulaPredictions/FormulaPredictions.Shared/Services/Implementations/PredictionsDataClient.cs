using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Services.Base;
using FormulaPredictions.Shared.Services.Interfaces;

namespace FormulaPredictions.Shared.Services.Implementations;

public class PredictionsDataClient : BaseWebApiClient, IPredictionsData
{
    public PredictionsDataClient(IHttpClientFactory httpFactory) 
        : base(httpFactory)
    {
    }

    public async Task<Answer[]> GetAnswers()
    {
        return await TryGet<Answer[]>("answers");
    }

    public async Task<Circuit[]> GetCircuits()
    {
        return await TryGet<Circuit[]>("circuits");
    }

    public async Task<Competitor[]> GetCompetitors()
    {
        return await TryGet<Competitor[]>("competitors");
    }

    public async Task<Driver[]> GetDrivers()
    {
        return await TryGet<Driver[]>("drivers");
    }

    public async Task<QuestionResponses[]> GetQuestionResponses()
    {
        return await TryGet<QuestionResponses[]>("questions");
    }

    public async Task<Team[]> GetTeams()
    {
        return await TryGet<Team[]>("teams");
    }
}
