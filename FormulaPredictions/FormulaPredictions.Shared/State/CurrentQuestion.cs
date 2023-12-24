using FormulaPredictions.Shared.Enums;
using FormulaPredictions.Shared.Models;

namespace FormulaPredictions.Shared.State;

public record CurrentQuestion
(
    QuestionResponses Question
)
{
    public Type ComponentForQuestion => Question.Type switch
    {
        _ => throw new InvalidOperationException($"The question type {Question.Type} is not recognised.")
    };
};
