﻿using F1Predictions.Core.Interfaces;

namespace F1Predictions.Core.Services
{
    public class DataServicesInitializer : IDataServicesInitializer
    {
        private readonly ICompetitorsDataService _competitorsService;
        private readonly ITeamsDataService _teamsService;
        private readonly IQuestionsDataService _questionsService;
        private readonly IDriversDataService _driversDataService;
        private readonly ITracksDataService _tracksDataService;

        public DataServicesInitializer(ITeamsDataService teamsService, ICompetitorsDataService competitorsService, IQuestionsDataService questionsService,
            IDriversDataService driversDataService, ITracksDataService tracksDataService)
        {
            _teamsService = teamsService;
            _competitorsService = competitorsService;
            _questionsService = questionsService;
            _driversDataService = driversDataService;
            _tracksDataService = tracksDataService;
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

        public async Task InitializeTracksAsync()
        {
            await _tracksDataService.InitializeAsync();
        }
    }
}
