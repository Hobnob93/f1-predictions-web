using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.RCL.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FormulaPredictions.RCL.Core;

public partial class TitleBar : ComponentBase
{
    [Inject]
    private IQuestionsService QuestionsService { get; set; } = default!;

    [CascadingParameter]
    private CascadingState AppState { get; set; } = default!;

    private string GetQuestionGroupName()
    {
        return QuestionsService.GetCurrentGroup(AppState.CurrentQuestion);
    }

    private string GetQuestionId()
    {
        return QuestionsService.GetCurrentId(AppState.CurrentQuestion);
    }

    private bool CanGoBack()
    {
        return QuestionsService.Previous(AppState.CurrentQuestion, AppState.AppData) is not null;
    }

    private bool CanGoForward()
    {
        return QuestionsService.Next(AppState.CurrentQuestion, AppState.AppData) is not null;
    }

    private void OnBackClicked(MouseEventArgs e)
    {
        var previousQuestion = QuestionsService.Previous(AppState.CurrentQuestion, AppState.AppData);
        if (AppState.CurrentQuestion is null || previousQuestion is null)
            return;

        AppState.CurrentQuestion = AppState.CurrentQuestion with
        {
            Question = previousQuestion
        };
    }

    private void OnForwardClicked(MouseEventArgs e)
    {
        var nextQuestion = QuestionsService.Next(AppState.CurrentQuestion, AppState.AppData);
        if (AppState.CurrentQuestion is null || nextQuestion is null)
            return;

        AppState.CurrentQuestion = AppState.CurrentQuestion with
        {
            Question = nextQuestion
        };
    }

    private void OnQuestionIdClicked(MouseEventArgs e)
    {
        
    }

    private void OnUpdateScoresClicked(MouseEventArgs e)
    {
        
    }
}
