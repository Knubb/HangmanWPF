using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IGameRecordUploader
    {
        void InsertHistoryRecord(HangmanGameRecord records);
    }
}
