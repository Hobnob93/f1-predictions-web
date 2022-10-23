using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Pages
{
    public partial class Results
    {
        [Inject]
        public ICompetitorsDataService CompetitorsService { get; set; } = default!;
    }
}
