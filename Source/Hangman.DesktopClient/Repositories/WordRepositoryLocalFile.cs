using System.Collections.Generic;
using HangmanWPF.Interfaces;

namespace HangmanWPF.Repositories
{

    public class WordRepositoryLocalFile : IWordRepository
    {
        public IEnumerable<string> FetchRandomSetOfWords(int size)
        {
            return new[] { "local1", "local2", "local3" };
        }

        public void Dispose()
        {
        }
    }
}
