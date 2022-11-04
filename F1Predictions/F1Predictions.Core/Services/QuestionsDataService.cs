﻿using F1Predictions.Core.Enum;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Models;

namespace F1Predictions.Core.Services
{
    public class QuestionsDataService : BaseDataService<QuestionResponse>, IQuestionsDataService
    {
        private readonly IWebApiRequest _apiWebRequest;

        public List<QuestionResponse> Data { get; private set; } = new();
        public QuestionResponse? CurrentQuestion { get; private set; }

        public event Func<Task>? StateChanging;
        public event Func<Task>? StateChanged;

        private int _currentIndex;

        public QuestionsDataService(IWebApiRequest apiWebRequest)
        {
            _apiWebRequest = apiWebRequest;
        }

        public QuestionResponse FindItem(string id)
        {
            return FindItem(Data, id);
        }

        public async Task InitializeAsync()
        {
            Data = (await FetchFromApi(_apiWebRequest, ApiEndpoint.Questions))
                .ToList();

            _currentIndex = 0;
            await UpdateCurrentQuestion(notify: false);
        }

        public bool CanGoForward()
        {
            return _currentIndex < Data.Count - 1;
        }

        public bool CanGoBack()
        {
            return _currentIndex > 0;
        }

        public async Task Next()
        {
            _currentIndex++;
            await UpdateCurrentQuestion();
        }

        public async Task Previous()
        {
            _currentIndex--;
            await UpdateCurrentQuestion();
        }

        public async Task GoTo(int index)
        {
            _currentIndex = index;
            await UpdateCurrentQuestion();
        }

        private async Task UpdateCurrentQuestion(bool notify = true)
        {
            if (notify && StateChanging is not null)
            {
                await StateChanging.Invoke();
                await Task.Delay(750);
            }

            CurrentQuestion = Data[_currentIndex];

            if (notify && StateChanged is not null)
                await StateChanged.Invoke();
        }
    }
}
