using Microsoft.AspNetCore.Components;

namespace FormulaPredictions.RCL;

public abstract class OneTimeRenderComponent : ComponentBase
{
    private bool _shouldRender = true;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        _shouldRender = false;
    }

    protected override bool ShouldRender() => _shouldRender;
}
