using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations;

public class ResponsesService : IResponsesService
{
    public T[] GetAllResponses<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem
    {
        var dataArray = appData.GetDataArray<T>();
        var competitorAnswerIds = currentData.Question.CompetitorResponses
            .Single(cr => cr.Id == competitorId)
            .Response.Split(',');

        return competitorAnswerIds
            .Select(rca => dataArray.Single(d => d.Id == rca))
            .ToArray();
    }

    public T GetSingleResponse<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem
    {
        var dataArray = appData.GetDataArray<T>();
        var competitorAnswerId = currentData.Question.CompetitorResponses
            .Single(cr => cr.Id == competitorId)
            .Response;

        return dataArray.Single(d => d.Id == competitorAnswerId);
    }
}
