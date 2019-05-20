using System.ComponentModel;
using System.Windows;

namespace HangmanWPF.Views
{
    /// <summary>
    /// Interaction logic for WordHistoryWindow.xaml
    /// </summary>
    public partial class WordHistoryWindow : Window
    {
        public WordHistoryWindow()
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
