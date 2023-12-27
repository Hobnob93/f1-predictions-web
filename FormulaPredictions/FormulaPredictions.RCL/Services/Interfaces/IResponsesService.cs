using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.Models.Base;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IResponsesService
{
    T[] GetAllResponses<T>(string competitorId, AppData appData, CurrentData currentData) where T: BaseItem;
    T GetSingleResponse<T>(string competitorId, AppData appData, CurrentData currentData) where T: BaseItem;
}
