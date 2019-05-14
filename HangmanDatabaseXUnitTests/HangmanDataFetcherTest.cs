using HangmanWPF.Models;
using Xunit;

namespace HangmanWPF.Tests
{
    public class HangmanDataFetcherTest
    {
        private IHangmanDataFetcher _DataFetcher;

        public HangmanDataFetcherTest()
        {
            _DataFetcher = new HangmanDataFetcherSQLite();
        }

        [Fact]
        public void GetWordCountTest()
        {
            int actual = _DataFetcher.WordCount;

            Assert.True(actual > -1);
        }
    }
}
