using System.ComponentModel;
using System.Windows;

namespace Hangman.DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for HangmanOptionsWindow.xaml
    /// </summary>
    public partial class HangmanOptionsWindow : Window
    {
        public HangmanOptionsWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
