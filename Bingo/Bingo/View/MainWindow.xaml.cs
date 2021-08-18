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
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.bingoViewModel;
            App.bingoViewModel.OnIsBingo += BingoViewModel_OnIsBingo;
        }

        private void BingoViewModel_OnIsBingo()
        {
            MessageBox.Show("BINGO!!!");
            Environment.Exit(0);
        }
    }
}
