using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IWordRepository
    {
        IEnumerable<string> FetchRandomSetOfWords(int size);
    }
}
