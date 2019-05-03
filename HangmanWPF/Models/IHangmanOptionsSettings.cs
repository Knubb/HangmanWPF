using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IHangmanOptionsSettings : ISettings
    {
        GraphicsOption GraphicsOption { get; set; }
        List<byte[]> SelectedImageSetData { get; set; }
    }
}