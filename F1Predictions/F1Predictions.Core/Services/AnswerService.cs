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

        public string GetCompetitorAnswerRaw(string id)
        {
            var currentQuestion = _questionsService.CurrentQuestion ?? throw new InvalidOperationException("Current question is null!");
            var type = currentQuestion.GetType();

            var property = type.GetProperty(id) ?? throw new InvalidOperationException($"Could not find property '{id}'");
            var result = property.GetValue(currentQuestion, null) ?? throw new InvalidOperationException($"Could not convert result to 'string'");
            Console.WriteLine($"{result} : {result.GetType()}");
            return (string)result;
        }
    }
}
