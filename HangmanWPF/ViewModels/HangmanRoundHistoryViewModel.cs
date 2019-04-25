using HangmanWPF.Models;
using System.Collections.ObjectModel;

namespace HangmanWPF.ViewModels
{
    class HangmanRoundHistoryViewModel
    {

        public ObservableCollection<HangmanGameRecord> GameHistory { get; set; } = new ObservableCollection<HangmanGameRecord>();


        public HangmanRoundHistoryViewModel()
        {
            MessageBus.Instance.Subscribe<HangmanRoundMessage>(HandleMessage);
        }

        private void HandleMessage(HangmanRoundMessage message)
        {

            GameHistory.Add(new HangmanGameRecord(message.Word, message.Won));
        }
    }

    public struct HangmanGameRecord
    {
        public string Word { get; set; }
        public bool Won { get; set; }

        public HangmanGameRecord(string word, bool won)
        {
            Word = word;
            Won = won;
        }
    }
}
