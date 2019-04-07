using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF.Models
{
    public class HangmanRoundManager
    {

        public string WordToGuess { get; private set; }

        private int _TriesLeft;
        public int TriesLeft
        {
            get { return _TriesLeft; }
            set
            {
                _TriesLeft = value;
            }
        }

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
