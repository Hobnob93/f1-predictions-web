using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IAnswersService
{
    Answer GetCurrentAnswer(AppData appData, CurrentData currentData);
}
