using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class QuestionMaster : IDisposable
    {
        [Inject]
        public IQuestionsDataService QuestionsService { get; set; } = default!;

        private DynamicComponent? ComponentRef { get; set; } = default!;

        private Type QuestionType { get; set; } = typeof(IntroContent);

        protected override void OnInitialized()
        {
            QuestionsService.StateChanged += QuestionChanged;
        }

        private async Task QuestionChanged()
        {
            SetCurrentQuestionType();

            await InvokeAsync(StateHasChanged);

            var component = ComponentRef?.Instance as IRefreshable;

            if (component is not null)
                await component.Refresh();
        }

        private void SetCurrentQuestionType()
        {
            if (QuestionsService.CurrentQuestion is null)
                throw new InvalidOperationException("The current question is null!");

            var scoringType = QuestionsService.CurrentQuestion.Scoring;
            QuestionType = scoringType.ToLower() switch
            {
                "intro" => typeof(IntroContent),
                "gp-team" => typeof(GPTeamContent),
                "gp-drvr" => typeof(GPDriverContent),
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
