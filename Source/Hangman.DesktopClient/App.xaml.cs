using Hangman.DesktopClient.Models;
using System.Windows;

namespace Hangman.DesktopClient
{
    public partial class App : Application
    {

        public App()
        {
            Current.Exit += (sender, args) => { SettingsContainer.SaveAll(); };           
        }
    }
}
