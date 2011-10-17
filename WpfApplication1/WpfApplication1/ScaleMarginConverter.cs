using System;
using System.Linq;
using System.Windows.Data;

namespace UriAgassi.Isobars
{
    public class ScaleMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var scale = (double)value;
            var scaleFrom = parameter.ToString().Split(',').Select(System.Convert.ToDouble);
            return string.Format("{0},{1},{2},{3}", scaleFrom.Select((x, i) => "" + x * ((i == 1) ? (scale - (int)scale) : scale)).ToArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
