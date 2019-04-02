
using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public class WordFetcher
    {
        private Stack<string> _CachedWords;


        public WordFetcher()
        {
            //Query the DB for words and cache them
            HangmanDatabase db = new HangmanDatabase();

            _CachedWords = new Stack<string>();

            foreach (var word in db.GetRandomSetOfWords(100))
            {
                _CachedWords.Push(word);
            }
        }
        public string FetchRandomWord()
        {
            return _CachedWords.Pop().ToUpper();
        }
    }
}
