using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.Models.Charting;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations;

public class ResponsesService : IResponsesService
{
    public RawCompetitorResponse<DriverTrack>[] GetAllDriverTrackResponses(AppData appData, CurrentData currentData)
    {
        var drivers = appData.GetDataArray<Driver>();
        var circuits = appData.GetDataArray<Circuit>();

        return GetAllResponsesForAllCompetitors(currentData)
            .Select(r => (r.CompetitorId, DriverId: r.Response.Split('-').First(), TrackId: r.Response.Split('-').Last()))
            .Select(r => new RawCompetitorResponse<DriverTrack>
            {
                Competitor = appData.Competitors.Single(c => string.Equals(c.Id, r.CompetitorId, StringComparison.OrdinalIgnoreCase)),
                Response = new DriverTrack
                (
                    drivers.Single(d => d.Id == r.DriverId),
                    circuits.Single(c => c.Id == r.TrackId)
                )
            })
            .ToArray();
    }

    public T[] GetAllResponses<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem
    {
        var dataArray = appData.GetDataArray<T>();
        var competitorResponseIds = GetCompetitorRawResponse(competitorId, currentData)
            .Split(',');

        return competitorResponseIds
            .Select(rca => dataArray.Single(d => d.Id == rca))
            .ToArray();
    }

    public RawCompetitorResponse<T>[] GetAllResponses<T>(AppData appData, CurrentData currentData) where T : BaseItem
    {
        var dataArray = appData.GetDataArray<T>();
        var allResponseIds = GetAllResponsesForAllCompetitors(currentData);

        return allResponseIds
            .Select(a => new RawCompetitorResponse<T>
            { 
                Competitor = appData.Competitors.Single(c => string.Equals(c.Id, a.CompetitorId, StringComparison.OrdinalIgnoreCase)),
                Response = dataArray.Single(d => d.Id == a.Response)
            })
            .ToArray();
    }

    public RawCompetitorResponse<T>[] GetAllValueResponses<T>(AppData appData, CurrentData currentData) where T : struct
    {
        return appData.Competitors
            .Select(c => new RawCompetitorResponse<T>
            {
                Competitor = c,
                Response = GetValueResponse<T>(c.Id, currentData)
            })
            .ToArray();
    }

    public DriverTrack[] GetDriverTrackResponses(string competitorId, AppData appData, CurrentData currentData)
    {
        var drivers = appData.GetDataArray<Driver>();
        var circuits = appData.GetDataArray<Circuit>();

        var competitorResponses = GetCompetitorRawResponse(competitorId, currentData)
            .Split(',');

        return competitorResponses
            .Select(str => str.Split('-'))
            .Select(spl => new DriverTrack
            (
                drivers.Single(d => d.Id == spl.First()),
                circuits.Single(c => c.Id == spl.Last())
            ))
            .ToArray();
    }

    public T GetSingleResponse<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem
    {
        var dataArray = appData.GetDataArray<T>();
        var competitorResponseId = GetCompetitorRawResponse(competitorId, currentData);

        return dataArray.Single(d => d.Id == competitorResponseId);
    }

    public T GetValueResponse<T>(string competitorId, CurrentData currentData) where T : struct
    {
        var competitorResponse = GetCompetitorRawResponse(competitorId, currentData);

        return typeof(T) switch
        {
            _ when typeof(T) == typeof(bool) => (T)(object)bool.Parse(competitorResponse),
            _ when typeof(T) == typeof(int) => (T)(object)int.Parse(competitorResponse),
            _ when typeof(T) == typeof(double) => (T)(object)double.Parse(competitorResponse),
            _ => throw new InvalidCastException($"The type '{typeof(T)}' is not recognised")
        };
    }

    private string GetCompetitorRawResponse(string competitorId, CurrentData currentData)
    {
        return currentData.Question.CompetitorResponses
            .Single(cr => string.Equals(cr.Id, competitorId, StringComparison.OrdinalIgnoreCase))
            .Response;
    }

    private IEnumerable<(string CompetitorId, string Response)> GetAllResponsesForAllCompetitors(CurrentData currentData)
    {
        return currentData.Question.CompetitorResponses
            .SelectMany(cr => cr.Response
                .Split(',')
                .Select(s => ( cr.Id, Response: s )))
            .Select(d => (d.Id, d.Response));
    }

    public BaseItem[] GetAllResponses(string competitorId, AppData appData, CurrentData currentData)
    {
        var competitorResponseIds = GetCompetitorRawResponse(competitorId, currentData)
            .Split(',');

        return competitorResponseIds
            .Select(id => new BaseItem
            {
                Id = id
            })
            .ToArray();
    }
}
