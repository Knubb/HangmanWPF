using System.Collections.Generic;

namespace HangmanWPF.Models
{

    public class WordRepository_LocalFile : IWordRepository
    {
        //string _Filepath;
        //MemoryReader _Reader;

        public IEnumerable<string> FetchRandomSetOfWords(int size)
        {
            return new[] { "local1", "local2", "local3" };
        }

        public void Dispose()
        {
        }
    }
}
