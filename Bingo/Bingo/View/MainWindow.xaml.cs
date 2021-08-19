using System;
using System.Windows;
using System.Windows.Input;

namespace Bingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                this.DataContext = App.bingoViewModel;
                ctrlStartScreen.OnLoadMainWindow += CtrlStartScreen_OnLoadMainWindow;
                App.bingoViewModel.OnIsNBingo += BingoViewModel_OnIsBingo;
            };
        }

        private void CtrlStartScreen_OnLoadMainWindow(object sender, EventArgs e)
        {
            ctrlStartScreen.Visibility = Visibility.Collapsed;
            gdMain.Visibility = Visibility.Visible;
        }

        private void BingoViewModel_OnIsBingo()
        {
            App.notifier.ShowNotifyMessage("🎉 Congraturate Bingo 🎉", "😊 !BINGO! ✌");
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && sender.Equals(e.OriginalSource))
            {
                this.DragMove();
            }
        }

        private void btnExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Collapsed;
            btnCloseMenu.Visibility = Visibility.Visible;
        }
    }
}
