using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IQuestionsDataService _questionsService;

        public AnswerService(IQuestionsDataService questionsService)
        {
            _questionsService = questionsService;
        }

        public T GetCompetitorAnswer<T>(string id)
        {
            var currentQuestion = _questionsService.CurrentQuestion ?? throw new InvalidOperationException("Current question is null!");
            var type = currentQuestion.GetType();

            var property = type.GetProperty(id) ?? throw new InvalidOperationException($"Could not find property '{id}'");
            return (T?)property.GetValue(currentQuestion, null) ?? throw new InvalidOperationException($"Could not convert result to '{nameof(T)}'");
        }
    }
}
