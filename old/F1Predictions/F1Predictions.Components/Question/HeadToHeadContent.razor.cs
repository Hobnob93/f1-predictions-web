using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public partial class HeadToHeadContent : QuestionContent
    {
        [Inject]
        private ICompResponses<HeadToHead> Responses { get; set; } = default!;

        protected override void SetResponses()
        {
            var drivers = Responses.GetAllResponses()
                .SelectMany(h2h => new List<Driver> { h2h.QualiChoice, h2h.RaceChoice })
                .ToList();

            var uniqueDrivers = Responses.GetAllResponses()
                .SelectMany(d => d.DriverOptions)
                .GroupBy(d => d.Id)
                .Select(g => g.First())
                .ToList();

            ResponseData = uniqueDrivers.Select(ud => new ChartDataPoint
            {
                Id = ud.Id,
                Name = ud.LastName,
                Color = ud.Color,
                Value = drivers.Count(d => d.Id == ud.Id)
            }).ToList();
        }

        private string GetContainerClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("d-flex")
                .AddClass("flex-row", when: !compIsRightAligned)
                .AddClass("flex-row-reverse", when: compIsRightAligned)
                .AddClass("align-end")
                .Build();
        }

        private string GetDriverClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("ms-n4", when: !compIsRightAligned)
                .AddClass("me-n4", when: compIsRightAligned)
                .Build();
        }

        private string GetTextClasses(bool compIsRightAligned)
        {
            return new CssBuilder()
                .AddClass("ms-n12", when: !compIsRightAligned)
                .AddClass("me-n9", when: compIsRightAligned)
                .Build();
        }
    }
}
