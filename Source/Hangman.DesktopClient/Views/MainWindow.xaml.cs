using System.ComponentModel;
using System.Windows;

namespace Hangman.DesktopClient.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
