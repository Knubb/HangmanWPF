using System.Collections.Generic;

namespace Hangman.DesktopClient.Interfaces
{
    public interface IImageSetRepository
    {
        IEnumerable<byte[]> GetRandom();

        IEnumerable<IEnumerable<byte[]>> GetAll();

        void Create(IList<byte[]> images);
    }
}
