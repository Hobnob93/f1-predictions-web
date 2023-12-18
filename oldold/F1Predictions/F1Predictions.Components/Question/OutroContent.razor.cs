using F1Predictions.Components.Response;
using F1Predictions.Core.Models;

namespace F1Predictions.Components.Question
{
    public partial class OutroContent : QuestionContent, IDisposable
    {
        protected override void OnInitialized()
        {
            CompetitorsService.ShowingStateChanged += Refresh;

            base.OnInitialized();
        }

        protected override void SetResponses()
        {
            ResponseData = CompetitorsService.Data
                .Where(comp => comp.IsShowingContent)
                .Select(comp => new ChartDataPoint
                {
                    Id = comp.Id,
                    Name = $"{comp.Id}: {comp.Name}",
                    Color = comp.Color,
                    Value = (decimal)ScoreManager.GetTotalScore(comp.Id)
                })
                .OrderByDescending(cdp => cdp.Value)
                .ToList();
        }

        public void Dispose()
        {
            CompetitorsService.ShowingStateChanged -= Refresh;
        }
    }
}
