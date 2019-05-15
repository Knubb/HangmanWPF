namespace HangmanWPF.Models
{
    public static class RepositoryContainer
    {
        public static IWordRepository Words { get; }
        public static IImageSetRepository ImageSets { get; }
        public static IHangmanGameRecordRepository GameRecords { get; }

        static RepositoryContainer()
        {
            Words = new WordRepository_SQLite();
            ImageSets = new ImageSetRepository_SQLite();
            GameRecords = new HangmanGameRecordRepository_SQLite();
        }
    }
}
