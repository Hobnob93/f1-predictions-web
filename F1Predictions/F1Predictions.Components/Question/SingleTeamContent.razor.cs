﻿using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace F1Predictions.Components.Question
{
    public partial class SingleTeamContent : QuestionContent
    {
        [Inject]
        private ITeamsDataService TeamsService { get; set; } = default!;

        protected override void SetResponses()
        {
            var teamIds = AnswerService.GetAllRawResponses();
            var teams = teamIds
                .Distinct()
                .Select(id => TeamsService.FindItem(id))
                .ToList();

            ResponseData = teams.Select(t => new ChartDataPoint
            {
                Id = t.Id,
                Name = t.Name,
                Color = t.Color,
                Value = teamIds.Count(id => id == t.Id)
            }).ToList();
        }

        private (string Id, string Name) GetCompetitorAnswer(string competitorId)
        {
            var answerId = AnswerService.GetRawResponseForComp(competitorId);
            var answer = TeamsService.Data.Single(t => t.Id == answerId).Name;

            return (answerId, answer);
        }
    }
}
