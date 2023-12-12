using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class TeamCompResponses : ICompResponses<Team>, IMultiCompResponses<Team>
    {
        private readonly IRawCompResponses _rawResponses;
        private readonly ITeamsDataService _teamsService;

        public TeamCompResponses(IRawCompResponses rawResponses, ITeamsDataService teamsService)
        {
            _rawResponses = rawResponses;
            _teamsService = teamsService;
        }

        public List<Team> GetAllResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(s => _teamsService.FindItem(s))
                .ToList();
        }

        public Team GetResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return _teamsService.FindItem(rawResponse);
        }

        public List<List<Team>> GetAllMultiResponses()
        {
            var rawResponses = _rawResponses.GetAllRawResponses();

            return rawResponses
                .Select(str => TeamsFromRaw(str)
                    .ToList())
                .ToList();
        }

        public List<Team> GetMultiResponseForComp(string id)
        {
            var rawResponse = _rawResponses.GetRawResponseForComp(id);

            return TeamsFromRaw(rawResponse)
                .ToList();
        }

        private IEnumerable<Team> TeamsFromRaw(string str)
        {
            return str
                .Split(",")
                .Select(str => _teamsService.FindItem(str));
        }
    }
}
