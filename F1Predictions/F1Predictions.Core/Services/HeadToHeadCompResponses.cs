using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class HeadToHeadCompResponses : ICompResponses<HeadToHead>
    {
        private readonly IRawCompResponses _rawResponses;
        private readonly IDriversDataService _driversService;
        private readonly ITeamsDataService _teamsService;

        public HeadToHeadCompResponses(IRawCompResponses rawResponses, IDriversDataService driversService, ITeamsDataService teamsService)
        {
            _rawResponses = rawResponses;
            _driversService = driversService;
            _teamsService = teamsService;
        }

        public List<HeadToHead> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => HeadToHeadFromRaw(str))
                .ToList();
        }

        public HeadToHead GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return HeadToHeadFromRaw(rawResponse);
        }

        private HeadToHead HeadToHeadFromRaw(string str)
        {
            var components = str.Split(",");

            var qualiChoice = _driversService.FindItem(components.First());
            var raceChoice = _driversService.FindItem(components.Last());

            var teamId = qualiChoice.TeamId;
            var driversFromTeam = _teamsService.FindItem(teamId)
                .DriverIds
                .Select(id => _driversService.FindItem(id))
                .ToList();

            return new HeadToHead(driversFromTeam, qualiChoice, raceChoice);
        }
    }
}
