using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class TeamCompResponses : ICompResponses<Team>
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
    }
}
