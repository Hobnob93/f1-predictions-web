using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces
{
    public interface IQuestionsDataService : IDataService<QuestionResponse>
    {
        QuestionResponse? CurrentQuestion { get; }

        event Func<Task>? StateChanged;

        bool CanGoForward();
        bool CanGoBack();

        Task Next();
        Task Previous();
    }
}
