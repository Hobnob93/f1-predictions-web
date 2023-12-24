using FormulaPredictions.RCL.State;
using FormulaPredictions.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FormulaPredictions.RCL.Core;

public partial class TitleBar : ComponentBase
{
    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private string GetQuestionGroupName()
    {
        if (AppState.CurrentQuestion is null)
            return "Loading...";

        var firstCharId = AppState.CurrentQuestion.Question.Id.First();
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

    private string GetQuestionId()
    {
        if (AppState.CurrentQuestion is null)
            return string.Empty;

        return AppState.CurrentQuestion.Question.Id;
    }

    private bool CanGoBack()
    {
        return false;
    }

    private bool CanGoForward()
    {
        return false;
    }

    private void OnBackClicked(MouseEventArgs e)
    {

    }

    private void OnForwardClicked(MouseEventArgs e)
    {

    }

    private void OnQuestionIdClicked(MouseEventArgs e)
    {

    }

    private void OnUpdateScoresClicked(MouseEventArgs e)
    {
        
    }
}
