using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IImageSetRepository
    {
        IEnumerable<byte[]> FetchRandomImageSet();

        IEnumerable<IEnumerable<byte[]>> FetchAllImageSets();

        void InsertImageSet(IList<byte[]> images);
    }
}
