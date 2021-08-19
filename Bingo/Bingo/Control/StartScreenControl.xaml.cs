using System;
using System.Windows;
using System.Windows.Controls;

namespace Bingo.Control
{
    /// <summary>
    /// Interaction logic for StartScreenControl.xaml
    /// </summary>
    public partial class StartScreenControl : UserControl
    {
        public delegate void LoadMainWindowEventHandler(object sender, EventArgs e);
        public event LoadMainWindowEventHandler OnLoadMainWindow;

        public StartScreenControl()
        {
            InitializeComponent();
        }

        private void btnGameStart_Click(object sender, RoutedEventArgs e)
        {
            OnLoadMainWindow?.Invoke(this, e);
        }
    }
}
