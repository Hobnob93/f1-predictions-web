using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IQuestionsService
{
    IGrouping<char, QuestionResponses>[] GetGroupings(AppData appData);
    string GetCurrentGroup(CurrentQuestion? currentQuestion);
    string GetCurrentId(CurrentQuestion? currentQuestion);
    QuestionResponses? Previous(CurrentQuestion? currentQuestion, AppData appData);
    QuestionResponses? Next(CurrentQuestion? currentQuestion, AppData appData);
    QuestionResponses Find(AppData appData, string Id);
}
