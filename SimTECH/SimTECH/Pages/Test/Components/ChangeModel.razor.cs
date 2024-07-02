using Microsoft.AspNetCore.Components;
using SimTECH.PageModels;

namespace SimTECH.Pages.Test.Components;

public partial class ChangeModel
{
    private ExampleViewModel _vm { get; set; }

    protected override void OnInitialized()
    {
        _vm = new ExampleViewModel();
        _vm.PropertyChanged += (sender, e) => StateHasChanged();
    }
}
