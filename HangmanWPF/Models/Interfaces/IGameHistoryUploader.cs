using System.Collections.Generic;

namespace HangmanWPF.Models
{
    public interface IGameHistoryUploader
    {
        void InsertHistoryRecord(HangmanGameRecord records);
    }
}
