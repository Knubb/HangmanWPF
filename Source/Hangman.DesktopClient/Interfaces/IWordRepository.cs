using System.Collections.Generic;

namespace Hangman.DesktopClient.Interfaces
{
    public interface IWordRepository
    {
        IEnumerable<string> GetRandomSet(int size);
    }
}
