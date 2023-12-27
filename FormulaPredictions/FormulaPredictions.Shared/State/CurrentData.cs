﻿using FormulaPredictions.Shared.Models;
using System.Collections.ObjectModel;

namespace FormulaPredictions.Shared.State;

public record CurrentData
(
    QuestionResponses Question,
    ObservableCollection<Competitor> ShowingCompetitorAnswers
)
{
    public Type CompetitorResponseTemplate => 
        Type.GetType($"FormulaPredictions.RCL.Templates.Competitor.{Question.Type}Template,FormulaPredictions.RCL") 
            ?? throw new InvalidOperationException($"Type '{Question.Type}' does not have a Content component");
};