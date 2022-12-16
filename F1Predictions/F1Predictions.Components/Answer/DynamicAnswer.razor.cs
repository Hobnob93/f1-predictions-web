using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Answer
{
    public partial class DynamicAnswer : IRefreshable
    {
        [Inject]
        protected IQuestionsDataService Questions { get; set; } = default!;

        [Inject]
        protected IAnswersDataService Answers { get; set; } = default!;

        public F1Predictions.Core.Models.Answer Answer { get; set; } = default!;
        public IRefreshable? AnswersChart { get; set; }
        public List<ChartDataPoint> AnswerData { get; protected set; } = new();


        protected override void OnInitialized()
        {
            Initialize();
        }

        public async Task Refresh()
        {
            Initialize();

            await InvokeAsync(StateHasChanged);

            if (AnswersChart is not null)
                await AnswersChart.Refresh();
        }

        private void Initialize()
        {
            var currentQuestion = Questions.CurrentQuestion;
            var answerId = currentQuestion.Scoring.AnswersId;

            Answer = Answers.FindItem(answerId);
        }
    }
}
