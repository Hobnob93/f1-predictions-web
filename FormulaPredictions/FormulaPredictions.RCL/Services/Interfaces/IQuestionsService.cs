using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IQuestionsService
{
    IGrouping<char, QuestionResponses>[] GetGroupings(AppData appData);
    string GetCurrentGroup(CurrentData? currentQuestion);
    string GetCurrentId(CurrentData? currentQuestion);
    QuestionResponses? Previous(CurrentData? currentQuestion, AppData appData);
    QuestionResponses? Next(CurrentData? currentQuestion, AppData appData);
    QuestionResponses Find(AppData appData, string Id);
}
