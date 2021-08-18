using System.Windows.Media;

namespace Bingo.Utils
{
    public class Util
    {
        public static SolidColorBrush GetHexToRgbColor(string hex)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
        }
    }
}
