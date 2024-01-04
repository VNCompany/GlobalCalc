using System;
using System.Windows;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

using GlobalCalc.UI.ViewModels;
using System.Linq;

namespace GlobalCalc.UI.Infrastructure;

internal static class ViewsProvider
{
    private static Dictionary<string, Type> viewTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.FullName?.StartsWith("GlobalCalc.UI.Views.") ?? false)
        .ToDictionary(t => t.FullName![20..]);

    public static void ShowWindow(string name, ViewModelBase context)
    {
        Window? window = GetWindow(name, context);
        if (window != null) window.Show(); else InvalidView(name);
    }

    public static bool? ShowWindowDialog(string name, ViewModelBase context)
    {
        Window? window = GetWindow(name, context);
        if (window != null)
            return window.ShowDialog();

        InvalidView(name);
        return null;
    }

    private static Window? GetWindow(string name, ViewModelBase context)
    {
        if (viewTypes.TryGetValue(name, out Type? value))
        {
            var window = (Window)Activator.CreateInstance(value)!;
            window.DataContext = context;
            context.WindowContext = new WindowContext(window);
            return window;
        }

        return null;
    }

    private static void InvalidView(string name) => Trace.WriteLine($"View `{name}` doesn't exists");
}
