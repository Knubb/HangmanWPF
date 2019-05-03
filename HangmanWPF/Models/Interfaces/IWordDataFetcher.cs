using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IWordFetcher
    {
        int WordCount { get; }

        string FetchRandomWord();
    }
}
