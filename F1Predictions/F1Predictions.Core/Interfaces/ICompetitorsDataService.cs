using F1Predictions.Core.Models;

namespace F1Predictions.Core.Interfaces
{
    public interface ICompetitorsDataService : IDataService<Competitor>
    {
        event Func<Task>? ShowingStateChanged;

        Task ResetShowingStates();
        Task ShowAllWithAnswer(string answerId, IAnswerService answerService);
        Task ShowCompetitor(string competitorId);
    }
}
