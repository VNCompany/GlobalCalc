using System;
using System.Windows;
using System.Collections.Generic;

using GlobalCalc.UI.ViewModels;

namespace GlobalCalc.UI.Infrastructure;

internal static class ViewsProvider
{
    private static Dictionary<Type, Type> _assoc = new()
    {
        { typeof(MainViewModel), typeof(Views.Main) },
        { typeof(FacadeViewModel), typeof(Views.Facade) },
        { typeof(ProfilesViewModel), typeof(Views.Profiles) }
    };

    private static Window GetWindow(ViewModelBase viewModel)
    {
        var window = (Window)Activator.CreateInstance(_assoc[viewModel.GetType()])!;
        window.DataContext = viewModel;
        viewModel.WindowContext = new WindowContext(window);
        return window;
    }

    public static void ShowWindow(ViewModelBase viewModel)
    {
        GetWindow(viewModel).Show();
    }

    public static bool? ShowWindowDialog(ViewModelBase viewModel) =>
        GetWindow(viewModel).ShowDialog();
}