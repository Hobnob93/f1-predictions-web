using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class QuestionMaster : IDisposable
    {
        [Inject]
        public IQuestionsDataService QuestionsService { get; set; } = default!;

        protected override void OnInitialized()
        {
            QuestionsService.StateChanged += QuestionChanged;

            base.OnInitialized();
        }

        private async Task QuestionChanged()
        {
            await InvokeAsync(StateHasChanged);
        }

        private Type GetCurrentQuestionType()
        {
            if (QuestionsService.CurrentQuestion is null)
                throw new InvalidOperationException("The current question is null!");

            var scoringType = QuestionsService.CurrentQuestion.Scoring;
            return scoringType.ToLower() switch
            {
                "intro" => typeof(IntroContent),
                "gp-team" => typeof(GPTeamContent),
                "val" => typeof(ValueContent),
                _ => throw new InvalidOperationException($"The question type {scoringType} is not recognised.")
            };
        }

        public void Dispose()
        {
            QuestionsService.StateChanged -= QuestionChanged;
        }
    }
}
