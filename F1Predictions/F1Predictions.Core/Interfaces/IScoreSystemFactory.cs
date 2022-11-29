using F1Predictions.Core.Enums;

namespace F1Predictions.Core.Interfaces
{
    public interface IScoreSystemFactory
    {
        IScoreSystem GetScoreSystem(ScoringType scoringType);
    }
}
