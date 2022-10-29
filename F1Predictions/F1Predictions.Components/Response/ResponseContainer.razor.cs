﻿using BlazorComponentUtilities;
using F1Predictions.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace F1Predictions.Components.Response
{
    public partial class ResponseContainer : BaseComponent, IDisposable
    {
        [Inject]
        private IQuestionsDataService QuestionsService { get; set; } = default!;

        [Parameter, EditorRequired]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter, EditorRequired]
        public int Index { get; set; }

        [Parameter, EditorRequired]
        public string Color { get; set; } = default!;

        private bool IsRightAligned => Index % 2 == 1;
        private bool IsShowingContent;

        protected override void OnInitialized()
        {
            QuestionsService.StateChanging += OnQuestionChanging;

            base.OnInitialized();
        }

        private async Task OnQuestionChanging()
        {
            IsShowingContent = false;

            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            QuestionsService.StateChanging -= OnQuestionChanging;
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
    }
}