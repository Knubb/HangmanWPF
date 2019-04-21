using System.Linq;

namespace HangmanWPF.Models
{
    public class HangmanRoundManager
    {

        public string WordToGuess { get; private set; }
        public int TriesLeft { get; set; }

        /// <summary>
        /// Set up a round of Hangman
        /// </summary>
        public HangmanRoundManager(string word, int tries)
        {
            WordToGuess = word;
            TriesLeft = tries;
        }

        /// <summary>
        /// Reuse and reset this object with new settings
        /// </summary>
        public void StartNew(string word, int tries)
        {
            WordToGuess = word;
            TriesLeft = tries;
        }

        public bool MakeGuess(char letter)
        {

            if (WordToGuess.Contains(letter))
            {
                return true;
            }
            else
            {
                TriesLeft -= 1;
                return false;
            }
        }
    }
}
