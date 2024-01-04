using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.Models.Charting;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IResponsesService
{
    T[] GetAllResponses<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem;
    RawCompetitorResponse<T>[] GetAllResponses<T>(AppData appData, CurrentData currentData) where T : BaseItem;
    RawCompetitorResponse<T>[] GetAllValueResponses<T>(AppData appData, CurrentData currentData) where T : struct;
    T GetSingleResponse<T>(string competitorId, AppData appData, CurrentData currentData) where T : BaseItem;
    T GetValueResponse<T>(string competitorId, CurrentData currentData) where T : struct;
    DriverTrack[] GetDriverTrackResponses(string competitorId, AppData appData, CurrentData currentData);
}
