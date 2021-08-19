using System;
using System.Windows;

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
            MessageBox.Show("BINGO!!!");
            Environment.Exit(0);
        }
    }
}
