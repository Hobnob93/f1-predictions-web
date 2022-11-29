using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class ScoreSystemFactory : IScoreSystemFactory
    {
        private readonly Func<ScoringType, IScoreSystem> _serviceResolver;

        public ScoreSystemFactory(Func<ScoringType, IScoreSystem> serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        public IScoreSystem GetScoreSystem(ScoringType scoringType)
        {
            return _serviceResolver(scoringType);
        }
    }
}
