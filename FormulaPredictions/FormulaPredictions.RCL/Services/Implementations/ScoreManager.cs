using FormulaPredictions.RCL.Services.Interfaces;

namespace FormulaPredictions.RCL.Services.Implementations;

public class ScoreManager : IScoreManager
{
    private readonly IScoringSystemFactory _scoreSystemFactory;

    public ScoreManager(IScoringSystemFactory scoreSystemFactory)
    {
        _scoreSystemFactory = scoreSystemFactory;
    }
}
