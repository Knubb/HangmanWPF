using System.Collections.Generic;
using HangmanWPF.Enums;

namespace HangmanWPF.Interfaces
{
    public interface IHangmanOptionsSettings : ISettings
    {
        GraphicsOption GraphicsOption { get; set; }
        List<byte[]> SelectedImageSetData { get; set; }
    }
}