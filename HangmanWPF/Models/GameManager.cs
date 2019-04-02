using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF.Models
{
    public class GameManager
    {
        public string WordToGuess { get; set; }

        public StringBuilder UserWord { get; set; }

        public int TriesLeft { get; set; }

        public int TotalTries { get; set; }

        public char[] LettersGuessed { get; set; }
    }
}
