using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IScoreSystem
{
    double CalculateScoreForCompetitor(Competitor competitor, AppData appData, CurrentData current);
}
