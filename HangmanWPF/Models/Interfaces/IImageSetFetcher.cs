using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IImageSetFetcher
    {
        int ImageSetCount { get; }

        IEnumerable<byte[]> FetchRandomImageSetData();

        IEnumerable<IEnumerable<byte[]>> FetchAllImageSetsAsData();
     
    }
}
