using F1Predictions.Core.Models;
using F1Predictions.Core.Services;

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
