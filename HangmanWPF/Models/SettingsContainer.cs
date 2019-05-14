using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF.Models
{
    public static class SettingsContainer
    {
        private static IHangmanOptionsSettings _HangmanOptions;
        public static IHangmanOptionsSettings HangmanOptions
        {
            get
            {
                if (_HangmanOptions == null)
                {
                    HangmanOptions = new HangmanOptionsSettings_SettingsFile();
                    HangmanOptions.LoadFromSource();
                }

                return _HangmanOptions;
            }
            set
            {
                _HangmanOptions = value;
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
