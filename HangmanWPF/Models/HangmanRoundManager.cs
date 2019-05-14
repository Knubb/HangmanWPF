using System.Linq;

namespace HangmanWPF.Models
{
    public class HangmanRoundManager
    {

        public string WordToGuess { get; private set; }
        public int TriesLeft { get; set; }

        /// <summary>
        /// Starts a new round of hangman
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
