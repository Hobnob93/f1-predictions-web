using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class ResponseContainer : BaseComponent, IDisposable
    {
        [Inject]
        private IQuestionsDataService QuestionsService { get; set; } = default!;

        [Inject]
        private IScoreManager ScoreManager { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter, EditorRequired]
        public bool IsRightAligned { get; set; }

        [Parameter, EditorRequired]
        public string CompId { get; set; } = default!;

        [Parameter, EditorRequired]
        public string Color { get; set; } = default!;

        [Parameter, EditorRequired]
        public EventCallback OnClicked { get; set; }

        [Parameter]
        public bool IsShowingContent { get; set; }

        private double Score { get; set; }

        protected override void OnInitialized()
        {
            QuestionsService.StateChanging += OnQuestionChanging;
            ScoreManager.OnScoresUpdated += OnScoreUpdated;

            base.OnInitialized();
        }

        private async Task OnQuestionChanging()
        {
            IsShowingContent = false;
            Score = 0;

            await InvokeAsync(StateHasChanged);
        }

        private async Task OnScoreUpdated()
        {
            IsShowingContent = true;
            Score = ScoreManager.GetScore(CompId);

            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            QuestionsService.StateChanging -= OnQuestionChanging;
            ScoreManager.OnScoresUpdated -= OnScoreUpdated;
        }

        private async Task OnClick()
        {
            await OnClicked.InvokeAsync();
        }

        private string OuterClasses => new CssBuilder()
            .AddClass("d-flex")
            .AddClass("z-10")
            .AddClass("gap-5")
            .AddClass("nameplate")
            .AddClass("ml-n14", when: !IsRightAligned)
            .AddClass("mr-n14", when: IsRightAligned)
            .AddClass("flex-row", when: !IsRightAligned)
            .AddClass("flex-row-reverse", when: IsRightAligned)
            .AddClass(Class, when: Class is not null)
            .Build();

        private string InnerClasses => new CssBuilder()
            .AddClass("d-flex")
            .AddClass("align-center")
            .AddClass("competitor")
            .AddClass("nameplate")
            .AddClass("breathe")
            .AddClass("gap-3")
            .AddClass("pe-2", when: !IsRightAligned)
            .AddClass("ps-2", when: IsRightAligned)
            .AddClass("flex-row", when: !IsRightAligned)
            .AddClass("flex-row-reverse", when: IsRightAligned)
            .AddClass("show-content", when: IsShowingContent)
            .Build();

        private string InnerStyle => new StyleBuilder()
            .AddStyle("color", Color)
            .AddStyle("border-color", Color)
            .Build();

        private string ScoreStyle => new StyleBuilder()
            .AddStyle("position", "absolute")
            .AddStyle("color", "gold")
            .AddStyle("z-index", "100")
            .AddStyle("font-size", "22px")
            .AddStyle("font-weight", "bold")
            .AddStyle("display", "none", when: Score == 0.0)
            .Build();

        private string ScoreClass => new CssBuilder()
            .AddClass("ml-n8", when: !IsRightAligned)
            .AddClass("mr-n8", when: IsRightAligned)
            .Build();
    }
}