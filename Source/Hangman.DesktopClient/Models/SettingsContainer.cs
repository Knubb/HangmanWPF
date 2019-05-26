using Hangman.DesktopClient.Interfaces;

namespace Hangman.DesktopClient.Models
{
    public static class SettingsContainer
    {
        private static IHangmanOptionsSettings _hangmanOptions;
        public static IHangmanOptionsSettings HangmanOptions
        {
            get
            {
                if (_hangmanOptions == null)
                {
                    HangmanOptions = new HangmanOptionsSettings();
                    HangmanOptions.LoadFromSource();
                }

                return _hangmanOptions;
            }
            set
            {
                _hangmanOptions = value;
            }
        }



        /// <summary>
        /// Saves all settings
        /// </summary>
        public static void SaveAll()
        {
            //TODO Find a way to call save on every property

            HangmanOptions.Save();
        }
    }
}
