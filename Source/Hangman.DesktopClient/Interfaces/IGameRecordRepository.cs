using Hangman.DesktopClient.Models;
using System.Collections.Generic;

namespace Hangman.DesktopClient.Interfaces
{
    public interface IGameRecordRepository
    {
        IEnumerable<HangmanGameRecord> GetAll();

        void Create(HangmanGameRecord record);
    }
}
