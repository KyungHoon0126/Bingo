using Bingo.Utils;
using Bingo.ViewModel;
using System.Windows;

namespace Bingo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BingoViewModel bingoViewModel = new BingoViewModel();
        public static Notifier notifier = new Notifier();
    }
}
