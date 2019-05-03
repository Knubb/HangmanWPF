using System;
using System.Collections.Generic;

namespace HangmanWPF.Models
{
    //Holds the Hangmanoptions setting, saves and loads to/from settings.settings
    public class HangmanOptionsSettings_SettingsFile : IHangmanOptionsSettings
    {

        private const GraphicsOption _DefaultGraphicsOption = GraphicsOption.RandomizeOnce;

        public GraphicsOption GraphicsOption { get; set; }

        public List<byte[]> SelectedImageSetData { get; set; }

        public void Save()
        {
            Properties.Settings.Default.GraphicsOption = Enum.GetName(typeof(GraphicsOption), this.GraphicsOption);

            Properties.Settings.Default.SelectedImageSet = this.SelectedImageSetData;

            Properties.Settings.Default.Save();
        }

        public void LoadFromSource()
        {
            GraphicsOption = LoadGraphicsOption();

            if (GraphicsOption == GraphicsOption.UseSelected)
            {
                SelectedImageSetData = LoadSelectedImageSetData();
            }
        }

        private GraphicsOption LoadGraphicsOption()
        {
            if (Enum.TryParse(Properties.Settings.Default.GraphicsOption, out GraphicsOption res))
            {
                return res;
            }

            return _DefaultGraphicsOption;
        }

        private List<byte[]> LoadSelectedImageSetData()
        {
            return Properties.Settings.Default.SelectedImageSet;
        }
    }
}
