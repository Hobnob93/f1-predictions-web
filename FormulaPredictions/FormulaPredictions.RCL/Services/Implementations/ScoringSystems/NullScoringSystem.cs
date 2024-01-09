using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations.ScoringSystems;

public class NullScoringSystem : IScoringSystem
{
    public double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current)
    {
        return 0d;
    }
}
