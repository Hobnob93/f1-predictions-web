using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class DataServicesInitializer : IDataServicesInitializer
    {
        private readonly ICompetitorsDataService _competitorsService;
        private readonly ITeamsDataService _teamsService;
        private readonly IQuestionsDataService _questionsService;
        private readonly IDriversDataService _driversDataService;

        public DataServicesInitializer(ITeamsDataService teamsService, ICompetitorsDataService competitorsService, IQuestionsDataService questionsService,
            IDriversDataService driversDataService)
        {
            _teamsService = teamsService;
            _competitorsService = competitorsService;
            _questionsService = questionsService;
            _driversDataService = driversDataService;
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

        public async Task InitializeDriversAsync()
        {
            await _driversDataService.InitializeAsync();
        }
    }
}
