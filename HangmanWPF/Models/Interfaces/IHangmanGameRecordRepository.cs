using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IHangmanGameRecordRepository
    {
        IEnumerable<HangmanGameRecord> FetchCompleteHistory();

        void InsertHistoryRecord(HangmanGameRecord record);
    }
}
