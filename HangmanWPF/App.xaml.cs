using HangmanWPF.Models;
using System.Windows;

namespace HangmanWPF
{
    public partial class App : Application
    {

        public App()
        {
            Application.Current.Exit += (sender, args) => { SettingsContainer.SaveAll(); };           
        }
    }
}
