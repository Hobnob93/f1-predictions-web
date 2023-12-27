using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations;

public class ResponsesService : IResponsesService
{
    public T[] GetAllResponses<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem
    {
        var dataArray = appData.GetDataArray<T>();
        var competitorAnswerIds = GetCompetitorRawAnswer(competitorId, currentData)
            .Split(',');

        return competitorAnswerIds
            .Select(rca => dataArray.Single(d => d.Id == rca))
            .ToArray();
    }

    public T GetSingleResponse<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem
    {
        var dataArray = appData.GetDataArray<T>();
        var competitorAnswerId = GetCompetitorRawAnswer(competitorId, currentData);

        return dataArray.Single(d => d.Id == competitorAnswerId);
    }

    public T GetValueResponse<T>(string competitorId, CurrentData currentData) where T : struct
    {
        var competitorAnswer = GetCompetitorRawAnswer(competitorId, currentData);

        return typeof(T) switch
        {
            _ when typeof(T) == typeof(bool) => (T)(object)bool.Parse(competitorAnswer),
            _ when typeof(T) == typeof(int) => (T)(object)int.Parse(competitorAnswer),
            _ when typeof(T) == typeof(double) => (T)(object)double.Parse(competitorAnswer),
            _ => throw new InvalidCastException($"The type '{typeof(T)}' is not recognised")
        };
    }

    private string GetCompetitorRawAnswer(string competitorId, CurrentData currentData)
    {
        return currentData.Question.CompetitorResponses
            .Single(cr => string.Equals(cr.Id, competitorId, StringComparison.OrdinalIgnoreCase))
            .Response;
    }
}
