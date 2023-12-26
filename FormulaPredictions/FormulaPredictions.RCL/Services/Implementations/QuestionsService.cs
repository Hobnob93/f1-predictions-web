using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FormulaPredictions.RCL.Services.Implementations;

public class QuestionsService : IQuestionsService
{
    public QuestionResponses Find(AppData appData, string Id)
    {
        foreach (var question in appData.Questions)
        {
            if (string.Equals(Id, question.Id, StringComparison.OrdinalIgnoreCase))
                return question;
        }

        throw new InvalidOperationException($"Question ID '{Id}' not found!");
    }

    public string GetCurrentGroup(CurrentData? currentQuestion)
    {
        if (currentQuestion is null)
            return "Loading...";

        var firstCharId = currentQuestion.Question.Id.First();
        return firstCharId switch
        {
            '0' => "Competitors",
            '1' => "Championship Predictions",
            '2' => "Team Predictions",
            '3' => "Driver Predictions",
            '4' => "Race Weekend Predictions",
            '5' => "Event Predictions",
            '6' => "Head to Heads",
            '7' => "Championship Order",
            '8' => "Best Predictor...",
            _ => throw new InvalidOperationException($"The group {firstCharId} is unaccounted for!")
        };
    }

    public string GetCurrentId(CurrentData? currentQuestion)
    {
        if (currentQuestion is null)
            return string.Empty;

        return currentQuestion.Question.Id;
    }

    public IGrouping<char, QuestionResponses>[] GetGroupings(AppData appData)
    {
        return appData.Questions
            .GroupBy(d => d.Id.First())
            .ToArray();
    }

    public QuestionResponses? Next(CurrentData? currentQuestion, AppData appData)
    {
        if (currentQuestion is null)
            return null;

        var currentIndex = Array.FindIndex(appData.Questions, q => q.Id == currentQuestion.Question.Id);
        if (currentIndex == appData.Questions.Length - 1)
            return null;

        return appData.Questions[currentIndex + 1];
    }

    public QuestionResponses? Previous(CurrentData? currentQuestion, AppData appData)
    {
        if (currentQuestion is null)
            return null;

        var currentIndex = Array.FindIndex(appData.Questions, q => q.Id == currentQuestion.Question.Id);
        if (currentIndex == 0)
            return null;

        return appData.Questions[currentIndex - 1];
    }
}
