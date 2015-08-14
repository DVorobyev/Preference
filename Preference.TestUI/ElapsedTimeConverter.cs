using System;
using System.Globalization;
using System.Management.Instrumentation;
using System.Windows;
using System.Windows.Data;

namespace Preference.TestUI
{
    [ValueConversion(typeof(int), typeof(TimeSpan))]
    public class ElapsedTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
