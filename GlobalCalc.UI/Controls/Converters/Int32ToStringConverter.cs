using System;
using System.Globalization;
using System.Windows.Data;

namespace GlobalCalc.UI.Controls.Converters;

public class Int32ToStringConverter : IValueConverter
{
#nullable disable
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value!.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (int.TryParse((string)value, out int intValue))
            return intValue;
        return 0;
    }
#nullable restore
}