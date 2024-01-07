using FormulaPredictions.RCL.Services.Implementations.ScoringSystems;
using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Enums;

namespace FormulaPredictions.RCL.Services.Implementations;

public class ScoreSystemFactory : IScoringSystemFactory
{
    private readonly IResponsesService _responsesService;

    public ScoreSystemFactory(IResponsesService responsesService)
    {
        _responsesService = responsesService;
    }

    public IScoringSystem CreateSystemFromScoreType(ScoringType scoringType)
    {
        return scoringType switch
        {
            ScoringType.Bool => new BoolScoringSystem(_responsesService),
            ScoringType.ChampOrder => new ChampOrderScoringSystem(_responsesService),
            ScoringType.GainLose => new GainLoseScoringSystem(_responsesService),
            ScoringType.GainWhenX => new GainWhenXScoringSystem(_responsesService),
            ScoringType.GetChoiceValue => new GetChoiceValueScoringSystem(_responsesService),
            ScoringType.HeadToHead => new HeadToHeadScoringSystem(_responsesService),
            ScoringType.Leaderboard => new LeaderboardScoringSystem(_responsesService),
            ScoringType.MultiDriverOnTrack => new MultiDriverOnTrackScoringSystem(_responsesService),
            ScoringType.Value => new ValueScoringSystem(_responsesService),
            ScoringType.Versus => new VersusScoringSystem(_responsesService),
            _ => throw new NotImplementedException($"No scoring type '{scoringType}'")
        };
    }
}
