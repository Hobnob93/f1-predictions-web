﻿namespace F1Predictions.Core.Interfaces
{
    public interface IAnswerService
    {
        List<string> GetAnswersRaw();
        string GetCompetitorAnswerRaw(string id);
    }
}
