using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations;

public class AnswersService : IAnswersService
{
    public Type GetAnswerOutputType(Answer answer)
    {
        return Type.GetType($"FormulaPredictions.RCL.Templates.Charting.{answer.RenderType}Template,FormulaPredictions.RCL")
            ?? throw new InvalidOperationException($"Type '{answer.RenderType}' does not have a Charting component");
    }

    public Answer GetCurrentAnswer(AppData appData, CurrentData currentData)
    {
        var answerId = currentData.Question.Scoring.AnswersId;
        return appData.Answers.Single(a => string.Equals(a.Id, answerId, StringComparison.OrdinalIgnoreCase));
    }
}
