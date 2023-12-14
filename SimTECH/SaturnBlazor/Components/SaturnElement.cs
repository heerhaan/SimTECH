using SaturnBlazor.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace SaturnBlazor.Components;

public class SaturnElement : SaturnComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string HtmlTag { get; set; } = "span";

    [Parameter]
    public ElementReference? Ref { get; set; }

    [Parameter]
    public EventCallback<ElementReference> RefChanged { get; set; }

    public void Refresh() => StateHasChanged();

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        builder.OpenElement(0, HtmlTag);

        foreach (var attribute in UserAttributes)
        {
            // checking if the value is null, we can get rid of null event handlers
            // for example `@onmouseenter=@(IsOpen ? HandleEnter : null)`
            // this is a powerful feature that in normal HTML elements doesn't work, because
            // Blazor adds always the attribute value and creates an EventCallback
            if (attribute.Value != null)
                builder.AddAttribute(1, attribute.Key, attribute.Value);
        }

        builder.AddAttribute(2, "class", Class);

        builder.AddAttribute(3, "style", Style);

        if (Ref != null)
        {
            builder.AddElementReferenceCapture(6, async capturedRef =>
            {
                Ref = capturedRef;
                await RefChanged.InvokeAsync(Ref.Value);
            });
        }

        builder.AddContent(10, ChildContent);

        builder.CloseElement();
    }
}
