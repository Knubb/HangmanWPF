using HangmanWPF.Commands;
using HangmanWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HangmanWPF.ViewModels
{

    //TODO:  Display all instances of correctly guessed letter.


    public class GameManagerVM : BaseViewModel
    {

        private List<string> _Alphabet;

        public string WordToGuess { get; set; }

        public StringBuilder UserWord { get; set; }

        public ObservableCollection<string> UserGuessedLetters { get; set; }

        private int _TriesLeft;
        public int TriesLeft
        {
            get
            {
                return _TriesLeft;
            }
            set
            {
                _TriesLeft = value;
                this.OnNotifyPropertyChanged(this, nameof(TriesLeft));
            }
        }

        public int TotalTries { get; set; }

        private int MaxWordSize { get; set; }

        public ICommand GuessLetterCmnd { get; set; }

        public GameManagerVM()
        {

            GuessLetterCmnd = new GuessLetterCommand(this.GuessLetter, this.CanGuessLetter);

            SetupRound();
        }

        public void SetupRound()
        {
            WordFetcher wf = new WordFetcher();

            var Letters = (string[])Application.Current.FindResource("Letters");
            _Alphabet = Letters.ToList();

            WordToGuess = wf.FetchRandomWord();
            TriesLeft = 5;

            UserGuessedLetters = new ObservableCollection<string>();

            for (int i = 0; i < WordToGuess.Length; i++)
            {
                this.UserGuessedLetters.Add("-");
            }
        }

        public void Reset()
        {
            
        }

        public void GuessLetter(string character)
        {
     
            if (WordToGuess.Contains(character))
            {

                foreach (var index in this.FindAllIndexesOf(WordToGuess, Char.Parse(character)))
                {
                    UserGuessedLetters.RemoveAt(index);
                    UserGuessedLetters.Insert(index, character);
                }

            }
            else
            {
                TriesLeft -= 1;
                _Alphabet.Remove(character);
            }
        }

        public bool CanGuessLetter(string character)
        {
            if (_Alphabet.Contains(character))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int[] FindAllIndexesOf(string str, char character)
        {
            List<int> res = new List<int>();

            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == character)
                {
                    res.Add(i);
                }
            }

            return res.ToArray();
        }
    }
}
