using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IWordDataBase
    {
        int WordCount { get; }

        IEnumerable<string> GetRandomSetOfWords(int amount);
    }
}
