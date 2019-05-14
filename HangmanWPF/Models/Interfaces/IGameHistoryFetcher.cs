using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IGameHistoryFetcher
    {
        IEnumerable<HangmanGameRecord> FetchHistory();
    }
}
