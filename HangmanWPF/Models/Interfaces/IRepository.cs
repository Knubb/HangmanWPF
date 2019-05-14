using System;
using System.Collections.Generic;

namespace HangmanWPF.Models
{

    public interface IRepository : IDisposable
    {

    }

    public interface IWordRepository : IRepository
    {
        string GetRandomWord();
    }

    public interface IImageSetRepository : IRepository
    {
        IEnumerable<byte[]> GetRandomImageSet();

        IEnumerable<IEnumerable<byte[]>> GetAllImageSets();
    }

    public interface IHangmanGameRecordRepository : IRepository
    {
        IEnumerable<HangmanGameRecord> FetchCompleteHistory();
    }

    public class WordRepository_SQLite : IWordRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string GetRandomWord()
        {
            throw new NotImplementedException();
        }
    }

    public class ImageSetRepository_SQLite : IImageSetRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<byte[]>> GetAllImageSets()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<byte[]> GetRandomImageSet()
        {
            throw new NotImplementedException();
        }
    }

    public class HangmanGameRecordRepository_SQLite : IHangmanGameRecordRepository
    {
        public void Dispose()
        {
        }

        public IEnumerable<HangmanGameRecord> FetchCompleteHistory()
        {
            return new List<HangmanGameRecord> { new HangmanGameRecord("Repository", false) };
        }
    }

    public static class RepositoryContainer
    {
        public static IWordRepository Words { get; }
        public static IImageSetRepository ImageSets { get; }
        public static IHangmanGameRecordRepository GameRecords { get; }
    }


}
