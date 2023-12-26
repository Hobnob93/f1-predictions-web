using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.Shared.State;

public record CurrentQuestion
(
    QuestionResponses Question
)
{
    public Type ComponentForQuestion => 
        Type.GetType($"FormulaPredictions.RCL.Questions.{Question.Type}Content,FormulaPredictions.RCL") 
        ?? throw new InvalidOperationException($"Type '{Question.Type}' does not have a Content component");
};
