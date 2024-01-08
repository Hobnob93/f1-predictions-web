using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IScoresManager
{
    (double Score, Competitor[] Scorers) GetHighestScorers(AppData appData, CurrentData current);
    void CalculateAllScores(AppData appData);
    double GetScoreForCompetitor(string competitorId);
}
