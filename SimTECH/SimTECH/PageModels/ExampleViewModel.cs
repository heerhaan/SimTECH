using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimTECH.PageModels;

// Also consider this information: https://jonhilton.net/blazor-state-management/

public abstract class ExampleBaseModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetValue<T>(ref T backingFiled, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
        backingFiled = value;
        OnPropertyChanged(propertyName);
    }
}

public class ExampleViewModel : ExampleBaseModel
{
    private List<string> _strings = [];

    public List<string> Strings
    {
        get => _strings;
        set
        {
            SetValue(ref _strings, value);
        }
    }

    public ExampleViewModel()
    {
        GetStrings();
    }

    public void GetStrings()
    {
        // retrieve strings data
    }
}
