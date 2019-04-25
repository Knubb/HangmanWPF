using HangmanWPF.Commands;
using HangmanWPF.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HangmanWPF.ViewModels
{
    class HangmanGameHistoryViewModel
    {

        public ObservableCollection<HangmanGameRecord> GameHistory { get; set; } = new ObservableCollection<HangmanGameRecord>();

        public ICommand OpenInGoogleCommand { get; set; }


        public HangmanGameHistoryViewModel()
        {
            OpenInGoogleCommand = new ActionCommand<string>(this.OpenInGoogle);

            MessageBus.Instance.Subscribe<HangmanRoundMessage>(HandleMessage);
        }

        private void HandleMessage(HangmanRoundMessage message)
        {

            GameHistory.Add(new HangmanGameRecord(message.Word, message.Won));
        }

        private void OpenInGoogle(string word)
        {


            if (string.IsNullOrWhiteSpace(word))
            {
                return;
            }

            string searchterm = $"DEFINE: {word}";

            string url = $"http://google.com/search?q={searchterm}";

            System.Diagnostics.Process.Start(url);
        }
    }

}
