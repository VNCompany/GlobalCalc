using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GlobalCalc.UI.ViewModels;

class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected ServicesManager Services = ServicesManager.Services;
    
    public Infrastructure.WindowContext? WindowContext { get; set; }
    
    protected virtual void OnPropertyChanged([CallerMemberName]string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Dispose() { }
    
}
