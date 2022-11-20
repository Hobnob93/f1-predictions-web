using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class RawCompResponses : IRawCompResponses
    {
        private readonly IQuestionsDataService _questionsService;

        public RawCompResponses(IQuestionsDataService questionsService)
        {
            _questionsService = questionsService;
        }

        public List<string> GetAllRawResponses()
        {
            var currentQuestion = GetQuestionResponse();
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

        public string GetRawResponseForComp(string id)
        {
            var currentQuestion = GetQuestionResponse();
            var type = currentQuestion.GetType();

            var property = type.GetProperty(id) ?? throw new InvalidOperationException($"Could not find property '{id}'");
            var result = property.GetValue(currentQuestion, null) ?? throw new InvalidOperationException("Retrived answer value is null");

            return (string)result;
        }

        private QuestionResponse GetQuestionResponse()
        {
            return _questionsService.CurrentQuestion ?? throw new InvalidOperationException("Current question is null!");
        }
    }
}
