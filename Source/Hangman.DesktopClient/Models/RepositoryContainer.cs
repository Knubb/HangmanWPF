using System.Collections.Generic;
using HangmanWPF.Interfaces;
using HangmanWPF.Repositories;

namespace HangmanWPF.Models
{
    public static class RepositoryContainer
    {
        public static IWordRepository Words { get; }
        public static IRepository<IEnumerable<byte[]>> ImageSets { get; }
        public static IRepository<HangmanGameRecord> GameRecords { get; }

        static RepositoryContainer()
        {
            Words = new WordRepositorySqLite();
            ImageSets = new ImageSetRepositorySqLite();
            GameRecords = new HangmanGameRecordRepositorySqLite();
        }
    }
}
