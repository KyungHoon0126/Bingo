using Bingo.Utils;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Bingo.Converters
{
    public class IsSelectedToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Util.GetHexToRgbColor("#262626") : Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
