using Hangman.DesktopClient.Interfaces;
using Hangman.DesktopClient.Repositories;

namespace Hangman.DesktopClient.Models
{
    public static class RepositoryContainer
    {
        public static IWordRepository Words { get; }
        public static IImageSetRepository ImageSets { get; }
        public static IGameRecordRepository GameRecords { get; }

        static RepositoryContainer()
        {
            Words = new WordRepositorySqLite();
            ImageSets = new ImageSetRepositorySqLite();
            GameRecords = new HangmanGameRecordRepositorySqLite();
        }
    }
}
