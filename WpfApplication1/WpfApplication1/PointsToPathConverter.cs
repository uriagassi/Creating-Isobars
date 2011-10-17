using System;
using System.Linq;
using System.Windows.Data;
using UriAgassi.Isobars.Algo;

namespace UriAgassi.Isobars
{
    public class PointsToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var points = (IsobarPoint[])value;
            var param = parameter.ToString().Split(',');
            var scale = System.Convert.ToDouble(param[0]);
            var format = string.Join(",", param.Skip(1));
            var path = string.Join(" ", points.Select(x => string.Format("L{0},{1} ", x.Location.Y*scale, x.Location.X*scale))).TrimStart('L');
            return string.Format(format, path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}