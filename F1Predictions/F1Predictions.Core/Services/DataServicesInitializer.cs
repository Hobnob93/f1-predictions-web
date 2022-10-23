using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class DataServicesInitializer : IDataServicesInitializer
    {
        private readonly ITeamsDataService _teamsService;

        public DataServicesInitializer(ITeamsDataService teamsService)
        {
            _teamsService = teamsService;
        }

        public async Task InitializeTeamsAsync()
        {
            await _teamsService.InitializeAsync();
        }
    }
}
