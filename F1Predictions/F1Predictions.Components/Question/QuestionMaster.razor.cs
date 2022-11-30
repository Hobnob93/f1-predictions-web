using F1Predictions.Core.Enums;
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
            var questionType = QuestionsService.CurrentQuestion.Type;
            QuestionType = questionType switch
            {
                F1Predictions.Core.Enums.QuestionType.Intro => typeof(IntroContent),
                F1Predictions.Core.Enums.QuestionType.SingleTeam => typeof(SingleTeamContent),
                F1Predictions.Core.Enums.QuestionType.SingleDriver => typeof(SingleDriverContent),
                F1Predictions.Core.Enums.QuestionType.SingleTrack => typeof(SingleTrackContent),
                F1Predictions.Core.Enums.QuestionType.SingleBool => typeof(SingleBooleanContent),
                F1Predictions.Core.Enums.QuestionType.Value => typeof(ValueContent),
                F1Predictions.Core.Enums.QuestionType.HeadToHead => typeof(HeadToHeadContent),
                F1Predictions.Core.Enums.QuestionType.MultiTeam => typeof(MultiTeamContent),
                F1Predictions.Core.Enums.QuestionType.MultiDriver => typeof(MultiDriverContent),
                F1Predictions.Core.Enums.QuestionType.MultiTrack => typeof(MultiTrackContent),
                F1Predictions.Core.Enums.QuestionType.MultiDriverTrack => typeof(MultiDriverTrackContent),
                _ => throw new InvalidOperationException($"The question type {questionType} is not recognised.")
            };
        }

        public void Dispose()
        {
            QuestionsService.StateChanged -= QuestionChanged;
        }
    }
}
