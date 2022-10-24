using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class DataServicesInitializer : IDataServicesInitializer
    {
        private readonly ICompetitorsDataService _competitorsService;
        private readonly ITeamsDataService _teamsService;
        private readonly IQuestionsDataService _questionsService;

        public DataServicesInitializer(ITeamsDataService teamsService, ICompetitorsDataService competitorsService, IQuestionsDataService questionsService)
        {
            _teamsService = teamsService;
            _competitorsService = competitorsService;
            _questionsService = questionsService;
        }

        public async Task InitializeCompetitorsAsync()
        {
            await _competitorsService.InitializeAsync();
        }

        public async Task InitializeTeamsAsync()
        {
            await _teamsService.InitializeAsync();
        }

        public async Task InitializeQuestionsAsync()
        {
            await _questionsService.InitializeAsync();
        }
    }
}
