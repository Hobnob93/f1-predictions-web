using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.ScoringSystems
{
    public class LeaderboardScoringSystem : IScoreSystem
    {
        private readonly IQuestionsDataService _questionsService;
        private readonly IAnswersDataService _answersService;

        public LeaderboardScoringSystem(IQuestionsDataService questionsService, IAnswersDataService answersService)
        {
            _questionsService = questionsService;
            _answersService = answersService;
        }

        public double ScoreForCompResponse(string compResponse)
        {
            var answerIdForCurrentQuestion = _questionsService.CurrentQuestion.Scoring.AnswersId;
            var answerData = _answersService.FindItem(answerIdForCurrentQuestion);

            var leaderboard = answerData.AnswersData;
            if (leaderboard is null || leaderboard.Count == 0)
                throw new InvalidOperationException($"AnswersData for '{answerData.Id}' has not been provided!");

            var indexOfResponse = leaderboard.FindIndex(0, leaderboard.Count, a => a.Id == compResponse);
            if (indexOfResponse == -1)
                return 0;

            return GetScoreFromLeaderboardPositionDistance(indexOfResponse - _questionsService.CurrentQuestion.Scoring.Index ?? 0);
        }

        private double GetScoreFromLeaderboardPositionDistance(int distance)
        {
            return Math.Abs(distance) switch
            {
                0 => 25,
                1 => 18,
                2 => 15,
                3 => 12,
                4 => 10,
                5 => 8,
                6 => 6,
                7 => 4,
                8 => 2,
                9 => 1,
                _ => 0
            };
        }

        public double ExtraToAccountFor(string compId)
        {
            return 0;
        }
    }
}
