using System.Collections.Generic;
using Hangman.DesktopClient.Enums;

namespace Hangman.DesktopClient.Interfaces
{
    public interface IHangmanOptionsSettings : ISettings
    {
        GraphicsOption GraphicsOption { get; set; }
        List<byte[]> SelectedImageSetData { get; set; }
    }
}