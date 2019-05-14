using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace HangmanWPF
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
