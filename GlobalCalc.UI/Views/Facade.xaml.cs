using System.Windows;

namespace GlobalCalc.UI.Views;

public partial class Facade : Window
{
    public Facade()
    {
        InitializeComponent();
    }

    private void ActionButton_OnClick(object sender, RoutedEventArgs args)
    {
        Close();
    }
}