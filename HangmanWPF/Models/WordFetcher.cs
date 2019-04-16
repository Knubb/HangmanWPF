
using System.Collections.Generic;

namespace HangmanWPF.Models
{

    //TODO 
    public class WordFetcher
    {

        //Query the DB for words and cache them
        private HangmanDatabase _Database = new HangmanDatabase();
        private Stack<string> _CachedWords;


        public WordFetcher()
        {
            _Database = new HangmanDatabase();

            _CachedWords = new Stack<string>();

            PopulateCache();
        }

        private void PopulateCache()
        {

            //Query the DB for words and cache them

            foreach (var word in _Database.GetRandomSetOfWords(100))
            {
                _CachedWords.Push(word);
            }
        }

        public string FetchRandomWord()
        {
            if (_CachedWords.Count < 1)
            {
                PopulateCache();
            }

            return _CachedWords.Pop().ToUpper();
        }
    }
}
