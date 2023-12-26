using FormulaPredictions.Shared.Models;
using System.Collections.ObjectModel;

namespace FormulaPredictions.Shared.State;

public record CurrentData
(
    QuestionResponses Question,
    ObservableCollection<Competitor> ShowingCompetitorAnswers
)
{
    public Type ComponentForQuestion => 
        Type.GetType($"FormulaPredictions.RCL.Questions.{Question.Type}Content,FormulaPredictions.RCL") 
            ?? throw new InvalidOperationException($"Type '{Question.Type}' does not have a Content component");
};
