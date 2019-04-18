using HangmanWPF.Commands;
using HangmanWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HangmanWPF.ViewModels
{
    public class HangmanGameViewModel : BaseViewModel
    {

        #region Debug display

        private string _Word;
        public string Word
        {
            get { return _Word; }
            set
            {
                _Word = value;
                NotifyPropertyChanged(this, nameof(Word));
            }
        }

        #endregion


        public const int Tries = 8;
        private HangmanRoundManager _RoundManager { get; set; }

        private Queue<BitmapImage> _ProgressImages;

        public ObservableCollection<LetterViewModel> LettersCollection { get; set; }

        private string _MaskedWord;
        public string MaskedWord
        {
            get { return _MaskedWord; }
            set
            {
                _MaskedWord = value;
                NotifyPropertyChanged(this, nameof(MaskedWord));
            }
        }

        private BitmapImage _ProgressImage;
        public BitmapImage ProgressImage
        {
            get
            {
                return _ProgressImage;
            }
            set
            {
                _ProgressImage = value;
                this.NotifyPropertyChanged(this, nameof(ProgressImage));
            }
        }

        private string _Difficulty;
        public string Difficulty
        {
            get { return _Difficulty; }
            set
            {
                _Difficulty = value;
                NotifyPropertyChanged(this, nameof(Difficulty));
            }
        }

        public ICommand GuessLetterCmnd { get; set; }
        public ICommand NewRoundCommand { get; set; }

        public HangmanGameViewModel()
        {
            GuessLetterCmnd = new GuessLetterCommand(this.GuessLetter);
            NewRoundCommand = new ActionCommand(this.StartNewRound);

            InitializeRound();
        }

        //We could move these methods to the HangmanRoundManager object, but that would make the command-object dependendent on an object whose lifespan is unknown
        private void GuessLetter(char character)
        {

            if (_RoundManager.MakeGuess(character))
            {

                LettersCollection.Single((x) => x.Letter == character).UpdateState(LetterState.Correct);

                UpdateMaskedWord(character);
            }
            else
            {
                LettersCollection.Single((x) => x.Letter == character).UpdateState(LetterState.Wrong);

                SetNextProgressImage();
            }

            CheckWinCondition();
        }

        private void InitializeRound()
        {     
            InitializeRoundManager();
            InitializeMaskedWord();
            InitializeProgressImages();
            InitializeLettersCollection();
        }

        private void InitializeRoundManager()
        {
            //Setup round manager object
            _RoundManager = new HangmanRoundManager(new WordFetcher(new HangmanDatabase()).FetchRandomWord(), Tries);

            Word = _RoundManager.WordToGuess; //DEV PROP
        }

        private void InitializeLettersCollection()
        {
            this.LettersCollection = new ObservableCollection<LetterViewModel>();

            var letters = Application.Current.FindResource("Letters");

            foreach (var letter in letters as string[])
            {
                LettersCollection.Add(new LetterViewModel(char.Parse(letter)));
            }
        }

        private void InitializeProgressImages()
        {
            //TODO Throw exception if the number of images is not equal to number of tries
            _ProgressImages = new Queue<BitmapImage>();

            foreach (var item in this.LoadImagesInFolder("C:\\Users\\knubb\\OneDrive\\Egna projekt\\Git\\Repositories\\HangmanWPF\\HangmanWPF\\HangmanData\\Images"))
            {
                _ProgressImages.Enqueue(item);
            };

            SetNextProgressImage();
        }

        private void InitializeMaskedWord()
        {

            var sb = new StringBuilder();

            for (int i = 0; i < _RoundManager.WordToGuess.Length; i++)
            {
                sb.Append("-");
            }

            this.MaskedWord = sb.ToString();
        }

        private void UpdateMaskedWord(char chartoinsert)
        {
            var sb = new StringBuilder(MaskedWord);

            foreach (var index in this.FindAllIndexesOf(_RoundManager.WordToGuess, (chartoinsert)))
            {

                sb.Remove(index, 1);
                sb.Insert(index, chartoinsert.ToString());
            }

            this.MaskedWord = sb.ToString();
        }

        private void SetNextProgressImage()
        {
            //Update progress image
            try
            {
                ProgressImage = _ProgressImages.Dequeue();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CheckWinCondition()
        {
            if (_RoundManager.TriesLeft < 1)
            {
                OnRoundLost();
            }
            else if (MaskedWord == _RoundManager.WordToGuess)
            {
                OnRoundWon();
            }
        }

        private void StartNewRound()
        {
            _RoundManager.StartNew(new WordFetcher(new HangmanDatabase()).FetchRandomWord(), Tries);

            Word = _RoundManager.WordToGuess; //DEV PROP

            foreach (var lettervm in LettersCollection)
            {
                lettervm.UpdateState(LetterState.NoGuess);
            }

            InitializeMaskedWord();
            InitializeProgressImages();
        }

        private void OnRoundWon()
        {
            MessageBox.Show("Round won!");

            StartNewRound();
        }

        private void OnRoundLost()
        {
            MessageBox.Show("Round lost");

            StartNewRound();
        }

        #region Helpers

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
        #endregion

    }
}
