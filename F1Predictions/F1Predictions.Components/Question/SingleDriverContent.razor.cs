using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class SingleDriverContent : QuestionContent
    {
        [Inject]
        private ICompResponses<Driver> Responses { get; set; } = default!;

        protected override void SetResponses()
        {
            var drivers = Responses.GetAllResponses();
            var uniqueDrivers = drivers
                .GroupBy(d => d.Id)
                .Select(g => g.First())
                .ToList();

            ResponseData = drivers.Select(ud => new ChartDataPoint
            {
                Id = ud.Id,
                Name = ud.LastName,
                Color = ud.Color,
                Value = drivers.Count(d => d.Id == ud.Id)
            }).ToList();
        }
    }
}
