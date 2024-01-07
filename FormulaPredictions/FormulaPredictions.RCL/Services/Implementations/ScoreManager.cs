using FormulaPredictions.RCL.Services.Interfaces;

namespace FormulaPredictions.RCL.Services.Implementations;

public class ScoreManager : IScoreManager
{
    private readonly IScoreSystemFactory _scoreSystemFactory;

    public ScoreManager(IScoreSystemFactory scoreSystemFactory)
    {
        _scoreSystemFactory = scoreSystemFactory;
    }
}
