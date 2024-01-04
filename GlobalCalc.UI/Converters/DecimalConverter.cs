using System;
using System.Globalization;
using System.Windows.Data;

namespace GlobalCalc.UI.Converters
{
    class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((decimal)value).ToString("f2");

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value;
    }
}
