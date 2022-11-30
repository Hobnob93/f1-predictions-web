using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces
{
    public interface IQuestionsDataService : IDataService<QuestionResponse>
    {
        QuestionResponse CurrentQuestion { get; }
        List<IGrouping<char, QuestionResponse>> QuestionGroups { get; }

        event Func<Task>? StateChanging;
        event Func<Task>? StateChanged;

        bool CanGoForward();
        bool CanGoBack();

        Task Next();
        Task Previous();
        Task GoTo(QuestionResponse question);
    }
}
