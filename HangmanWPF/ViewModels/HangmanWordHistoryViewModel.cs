using HangmanWPF.Models;
using System.Collections.ObjectModel;

namespace HangmanWPF.ViewModels
{
    class HangmanWordHistoryViewModel
    {
        public ObservableCollection<string> WordHistory { get; set; } = new ObservableCollection<string>();



        public HangmanWordHistoryViewModel()
        {
            MessageBus.Instance.Subscribe<HangmanRoundMessage>(HandleMessage);
        }

        private void AddRecord(string word)
        {
            WordHistory.Add(word);
        }

        private void HandleMessage(HangmanRoundMessage message)
        {
            AddRecord(message.Word);
        }
    }
}
