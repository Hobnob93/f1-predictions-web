using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class DataServicesInitializer : IDataServicesInitializer
    {
        private readonly ICompetitorsDataService _competitorsService;
        private readonly ITeamsDataService _teamsService;

        public DataServicesInitializer(ITeamsDataService teamsService, ICompetitorsDataService competitorsService)
        {
            _teamsService = teamsService;
            _competitorsService = competitorsService;
        }

        public async Task InitializeCompetitorsAsync()
        {
            await _competitorsService.InitializeAsync();
        }

        public async Task InitializeTeamsAsync()
        {
            await _teamsService.InitializeAsync();
        }
    }
}
