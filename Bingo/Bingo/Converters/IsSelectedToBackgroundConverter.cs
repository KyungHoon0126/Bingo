using System;
using System.Globalization;
using System.Windows.Data;

namespace Bingo.Converters
{
    public class IsSelectedToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return ((bool)value) ? Util.GetHexToRgbColor("#262626") : Brushes.White;
            return ((bool)value) ? @"/Assets/Circle.png" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
