using FormulaPredictions.RCL.Services.Interfaces;
using FormulaPredictions.Shared.Models;
using FormulaPredictions.Shared.State;

namespace FormulaPredictions.RCL.Services.Implementations;

public class ScoresManager : IScoresManager
{
    private readonly IScoringSystemFactory _scoreSystemFactory;

    private Dictionary<string, double> _scoresMap = [];

    public ScoresManager(IScoringSystemFactory scoreSystemFactory)
    {
        _scoreSystemFactory = scoreSystemFactory;
    }

    public void CalculateAllScores(AppData appData)
    {
        _scoresMap.Clear();

        foreach (var competitor in appData.Competitors)
        {
            _scoresMap.Add(competitor.Id, 0d);
        }

        var fakeCurrent = new CurrentData(new QuestionResponses(), [], false);
        foreach (var question in appData.Questions)
        {
            fakeCurrent = fakeCurrent with
            {
                Question = question
            };

            var scoreSystem = _scoreSystemFactory.CreateSystemFromScoreType(question.Scoring.Type);
            foreach (var competitor in appData.Competitors)
            {
                var score = scoreSystem.CalculateScoreForCompetitor(competitor, appData, fakeCurrent);
                _scoresMap[competitor.Id] = _scoresMap[competitor.Id] + score;
            }
        }
    }

    public (double Score, Competitor[] Scorers) GetHighestScorers(AppData appData, CurrentData current)
    {
        var scoreSystem = _scoreSystemFactory.CreateSystemFromScoreType(current.Question.Scoring.Type);

        var highestScore = 0d;
        var highestScorers = new List<Competitor>();
        foreach (var competitor in appData.Competitors)
        {
            var score = scoreSystem.CalculateScoreForCompetitor(competitor, appData, current);

            if (score == 0d)
                continue;

            if (score == highestScore)
            {
                highestScorers.Add(competitor);
                continue;
            }

            if (score > highestScore)
            {
                highestScore = score;
                highestScorers.Clear();
                highestScorers.Add(competitor);
            }
        }

        return (highestScore, highestScorers.ToArray());
    }

    public double GetScoreForCompetitor(string competitorId)
    {
        if (_scoresMap.Count == 0)
            throw new Exception($"No scores tracked - please call '{nameof(CalculateAllScores)}' first");

        if (!_scoresMap.TryGetValue(competitorId, out var score))
            throw new Exception($"Competitor ID '{competitorId}' has no score!");

        return score;
    }
}
