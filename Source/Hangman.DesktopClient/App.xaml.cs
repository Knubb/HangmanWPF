using HangmanWPF.Models;
using System.Windows;

namespace HangmanWPF
{
    public partial class App : Application
    {

        public App()
        {
            Current.Exit += (sender, args) => { SettingsContainer.SaveAll(); };           
        }
    }
}
