using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IImageSetFetcher
    {
        int ImageSetCount { get; }

        IEnumerable<byte[]> FetchRandomImageSet();
    }
}
