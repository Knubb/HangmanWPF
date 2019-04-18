using HangmanWPF.Models;

namespace HangmanWPF.ViewModels
{
    public class LetterViewModel : BaseViewModel
    {

        public char Letter { get; }

        private LetterState _State;
        public LetterState State
        {
            get { return _State;  }
            private set
            {
                _State = value;
                NotifyPropertyChanged(this, nameof(State));
            }
        }

        public LetterViewModel() { }

        public LetterViewModel(char letter)
        {
            this.State = LetterState.NoGuess;
            this.Letter = letter;
        }


        public void UpdateState(LetterState state)
        {
            this.State = state;
        }
    }
}
