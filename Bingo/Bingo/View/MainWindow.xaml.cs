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
                App.bingoViewModel.OnIsNBingo += BingoViewModel_OnIsBingo;
            };
        }

        private void BingoViewModel_OnIsBingo()
        {
            App.notifier.ShowNotifyMessage("🎉 Congraturate Bingo 🎉", "😊 !BINGO! ✌");
            ExitBingoGame();
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
            ExitBingoGame();
        }

        private void ExitBingoGame()
        {
            Environment.Exit(0);
        }
    }
}
