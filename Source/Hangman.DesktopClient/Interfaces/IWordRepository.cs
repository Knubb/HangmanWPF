using System.Collections.Generic;

namespace HangmanWPF.Interfaces
{
    public interface IWordRepository
    {
        IEnumerable<string> FetchRandomSetOfWords(int size);
    }
}
