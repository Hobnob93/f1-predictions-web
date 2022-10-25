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

        public List<string> GetAnswersRaw()
        {
            var currentQuestion = _questionsService.CurrentQuestion ?? throw new InvalidOperationException("Current question is null!");
            var type = currentQuestion.GetType();

            var result = new List<string>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.Length > 2)
                    continue;

                if (string.Equals("Id", property.Name, StringComparison.OrdinalIgnoreCase))
                    continue;

                var value = property.GetValue(currentQuestion, null) ?? throw new InvalidOperationException("Retrived answer value is null");
                result.Add((string)value);
            }

            return result;
        }

        public string GetCompetitorAnswerRaw(string id)
        {
            var currentQuestion = _questionsService.CurrentQuestion ?? throw new InvalidOperationException("Current question is null!");
            var type = currentQuestion.GetType();

            var property = type.GetProperty(id) ?? throw new InvalidOperationException($"Could not find property '{id}'");
            var result = property.GetValue(currentQuestion, null) ?? throw new InvalidOperationException("Retrived answer value is null");
            Console.WriteLine($"{result} : {result.GetType()}");
            return (string)result;
        }
    }
}
