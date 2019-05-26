using Hangman.DesktopClient.Enums;

namespace Hangman.DesktopClient.ViewModels
{
    public class LetterViewModel : BaseViewModel
    {
        public char Letter { get; }

        private LetterState _state;
        public LetterState State
        {
            get { return _state;  }
            private set
            {
                _state = value;
                NotifyPropertyChanged(this, nameof(State));
            }
        }

        public LetterViewModel() { }

        public LetterViewModel(char letter)
        {
            State = LetterState.NoGuess;
            Letter = letter;
        }

        public void UpdateState(LetterState state)
        {
            State = state;
        }
    }
}
