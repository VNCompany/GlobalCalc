using System.Windows;

namespace GlobalCalc.UI.Infrastructure;

internal sealed class WindowContext
{
    private readonly Window _window;

    public bool? DialogResult
    {
        get => _window.DialogResult;
        set => _window.DialogResult = value;
    }
    
    public WindowContext(Window window)
    {
        _window = window;
    }

    public void Close() => _window.Close();
}
