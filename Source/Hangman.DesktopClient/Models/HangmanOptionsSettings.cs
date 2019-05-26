using System;
using System.Collections.Generic;
using Hangman.DesktopClient.Enums;
using Hangman.DesktopClient.Interfaces;

namespace Hangman.DesktopClient.Models
{
    public class HangmanOptionsSettings : IHangmanOptionsSettings
    {
        private const GraphicsOption DefaultGraphicsOption = GraphicsOption.RandomizeOnce;
        public GraphicsOption GraphicsOption { get; set; }
        public List<byte[]> SelectedImageSetData { get; set; }

        public void Save()
        {
            var defaultSettings = Properties.Settings.Default;
            defaultSettings.GraphicsOption = Enum.GetName(typeof(GraphicsOption), GraphicsOption);
            defaultSettings.SelectedImageSet = SelectedImageSetData;
            defaultSettings.Save();
        }

        // What is a source?
        public void LoadFromSource()
        {
            GraphicsOption = ParseGraphicsOption();

            if (GraphicsOption == GraphicsOption.UseSelected)
            {
                SelectedImageSetData = GetImagesFromSettings();
            }
        }

        private GraphicsOption ParseGraphicsOption()
        {
            if (Enum.TryParse(Properties.Settings.Default.GraphicsOption, out GraphicsOption res))
            {
                return res;
            }

            return DefaultGraphicsOption;
        }

        private List<byte[]> GetImagesFromSettings()
        {
            return Properties.Settings.Default.SelectedImageSet;
        }
    }
}
