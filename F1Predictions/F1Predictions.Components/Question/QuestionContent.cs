﻿using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Question
{
    public abstract class QuestionContent : ComponentBase, IRefreshable
    {
        [Inject]
        protected ICompetitorsDataService CompetitorsService { get; set; } = default!;

        [Inject]
        protected IRawCompResponses AnswerService { get; set; } = default!;

        public IRefreshable? RefreshableChart { get; set; }
        public List<ChartDataPoint> ResponseData { get; protected set; } = new();

        protected abstract void SetResponses();

        protected override void OnInitialized()
        {
            SetResponses();
        }

        public async Task Refresh()
        {
            SetResponses();

            await InvokeAsync(StateHasChanged);

            if (RefreshableChart is not null)
                await RefreshableChart.Refresh();
        }

        protected async Task OnSelectedChartDataPoint(ChartDataPoint dataPoint)
        {
            await CompetitorsService.ShowAllWithAnswer(dataPoint.Id, AnswerService);
        }
    }
}
