using System;
using System.Windows.Data;

namespace UriAgassi.Isobars
{
    public class ConditionalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var param = parameter.ToString().Split(',');
            return ((value == null && string.IsNullOrEmpty(param[0])) || (value != null && value.ToString() == param[0])) ? param[1] : param[2];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}