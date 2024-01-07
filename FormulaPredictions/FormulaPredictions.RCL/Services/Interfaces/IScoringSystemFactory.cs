using FormulaPredictions.Shared.Enums;

namespace FormulaPredictions.RCL.Services.Interfaces;

public interface IScoringSystemFactory
{
    IScoringSystem CreateSystemFromScoreType(ScoringType scoringType);
}
