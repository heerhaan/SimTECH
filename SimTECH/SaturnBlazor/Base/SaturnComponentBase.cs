using Microsoft.AspNetCore.Components;
using SaturnBlazor.Interfaces;
using System.ComponentModel;

namespace SaturnBlazor.Base;

public abstract class SaturnComponentBase : ComponentBase, ISaturnStateChanged
{
    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public object? Tag { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object?> UserAttributes { get; set; } = [];

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
    }

    void ISaturnStateChanged.StateHasChanged() => StateHasChanged();
}
