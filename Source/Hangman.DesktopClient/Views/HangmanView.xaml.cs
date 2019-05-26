using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Hangman.DesktopClient.Views
{
    public partial class HangmanView : UserControl
    {
        public HangmanView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //If a key is pressed.. Search for the element (Letter button) with the same character bound to it and if clickable (IsHitTestVisible), execute its command
            for (int i = 0; i < LettersPanel.Items.Count; i++)
            {

                var obj = LettersPanel.ItemContainerGenerator.ContainerFromIndex(i);
                int childrencount = VisualTreeHelper.GetChildrenCount(obj);

                for (int x = 0; x < childrencount; x++)
                {
                    var o = VisualTreeHelper.GetChild(obj, x);
                    Button b = o as Button;

                    if (b.IsHitTestVisible)
                    {
                        char para = (char)b.CommandParameter;

                        if (para.ToString().ToUpper() == e.Key.ToString().ToUpper())
                        {
                            b.Command.Execute(b.CommandParameter);
                            return;
                        }
                    }
                }
            }
        }
    }
}
