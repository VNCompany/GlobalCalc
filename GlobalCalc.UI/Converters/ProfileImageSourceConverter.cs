using System;
using System.Globalization;
using System.Windows.Data;

using System.IO;

using GlobalCalc.Shared;

namespace GlobalCalc.UI.Converters;

class ProfileImageSourceConverter : IValueConverter
{
#nullable disable
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var profile = (Profile)value;
        return ServicesManager.Services.Images.GetImageSource(profile!.Id, parameter as string == "True");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException();
    }
    
#nullable restore
}