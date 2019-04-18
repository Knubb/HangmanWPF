
using System.Collections.Generic;

namespace HangmanWPF.Models
{

    //TODO 
    public class WordFetcher
    {

        //Query the DB for words and cache them
        private IWordDataBase _Database;
        private Stack<string> _CachedWords;


        public WordFetcher(IWordDataBase worddataBase)
        {
            _Database = worddataBase;

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
