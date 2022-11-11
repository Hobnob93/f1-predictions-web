﻿using ApexCharts;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Core
{
    public partial class ScatterChart : BaseComponent
    {
        [Parameter, EditorRequired]
        public string Title { get; set; } = string.Empty;

        [Parameter, EditorRequired]
        public List<ChartDataPoint> Data { get; set; } = default!;

        [Parameter, EditorRequired]
        public EventCallback<ChartDataPoint> SelectionCallback { get; set; }

        private ApexChart<ChartDataPoint>? Chart;
        private ApexChartOptions<ChartDataPoint> Options { get; set; } = new();

        protected override void OnInitialized()
        {
            Options.Legend = new Legend
            {
                FontSize = "14px",
                FontWeight = "bold",
                Position = LegendPosition.Right,
                Labels = new LegendLabels
                {
                    Colors = new Color("#000000")
                }
            };

            Options.Markers = new Markers
            {
                Hover = new MarkersHover
                {
                    Size = 13
                },
                Size = 10,
            };

            Options.Tooltip = new Tooltip
            {
                Style = new TooltipStyle
                {
                    FontSize = "14px"
                },
                Theme = "dark"
            };
        }

        private async Task OnChartItemSelected(SelectedData<ChartDataPoint> selectedData)
        {
            var index = selectedData.DataPointIndex;
            var item = Data[index];

            await SelectionCallback.InvokeAsync(item);
        }
    }
}
