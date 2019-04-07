using HangmanWPF.Commands;
using HangmanWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HangmanWPF.ViewModels
{
    
    public class GameManagerVM : BaseViewModel
    {
        private Queue<BitmapImage> _ProgressImages = new Queue<BitmapImage>();

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
                this.NotifyPropertyChanged(this, nameof(TriesLeft));
            }
        }

        public int TotalTries { get; set; }



        private BitmapImage _StateAsImage;
        public BitmapImage StateAsImage
        {
            get
            {
                return _StateAsImage;
            }
            set
            {
                _StateAsImage = value;
                this.NotifyPropertyChanged(this, nameof(StateAsImage));
            }
        }


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

            _ProgressImages.Clear();

            //Load our images into the queue
            foreach (var item in this.LoadImagesInFolder("C:\\Users\\knubb\\OneDrive\\Egna projekt\\Git\\Repositories\\HangmanWPF\\HangmanWPF\\HangmanData\\Images"))
            {
                _ProgressImages.Enqueue(item);
            };

            //Add heightens to mask out the word
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
                SetNextImage();
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

        private void SetNextImage()
        {
            try
            {
                StateAsImage = _ProgressImages.Dequeue();
            }
            catch (Exception)
            {
                SetupRound();
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

        private IEnumerable<BitmapImage> LoadImagesInFolder(string folderpath)
        {
            List<BitmapImage> images = new List<BitmapImage>();

            

            foreach (var filepath in Directory.GetFiles(folderpath))
            {
                images.Add(new BitmapImage(new Uri(filepath, UriKind.Absolute)));
            }


            return images;
        }
    }
}
