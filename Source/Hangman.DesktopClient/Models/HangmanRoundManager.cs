using System.Linq;

namespace Hangman.DesktopClient.Models
{
    public class HangmanRoundManager
    {

        public string WordToGuess { get; private set; }
        public int TriesLeft { get; set; }

        public void Start(string word, int tries)
        {
            WordToGuess = word;
            TriesLeft = tries;
        }

        public bool MakeGuess(char letter)
        {
            var isCorrect = WordToGuess.Contains(letter);
            if (!isCorrect)
            {
                const int liesLostDueToIncorrectGuess = 1;
                TriesLeft -= liesLostDueToIncorrectGuess;
            }

            return isCorrect;
        }
    }
}
