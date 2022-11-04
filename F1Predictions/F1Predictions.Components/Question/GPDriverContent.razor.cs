﻿using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class GPDriverContent : QuestionContent
    {
        [Inject]
        private IDriversDataService DriversService { get; set; } = default!;

        protected override void SetResponses()
        {
            base.SetResponses();

            var driverIds = AnswerService.GetAnswersRaw();
            var drivers = driverIds
                .Distinct()
                .Select(id => DriversService.FindItem(id))
                .ToList();

            ChartOptions.ChartPalette = drivers
                .Select(t => t.Color)
                .ToArray();

            ResponseData = drivers.Select(t => new ChartDataPoint
            {
                Id = t.Id,
                Name = t.LastName,
                Value = driverIds.Count(id => id == t.Id)
            }).ToList();
        }

        private (string Id, string Name) GetCompetitorAnswer(string competitorId)
        {
            var answerId = AnswerService.GetCompetitorAnswerRaw(competitorId);
            var answer = DriversService.Data.Single(t => t.Id == answerId).LastName;

            return (answerId, answer);
        }
    }
}
