using HangmanWPF.Commands;
using HangmanWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HangmanWPF.ViewModels
{
    public class HangmanGameViewModel : BaseViewModel
    {

        public const int Tries = 8;
        private HangmanRoundManager _RoundManager { get; set; }

        //Hard dependencies
        private IHangmanDataFetcher _DataFetcher = new HangmanDataFetcherSQLite();

        private Queue<ImageSource> _ProgressImages;

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

        private BitmapSource _ProgressImage;
        public BitmapSource ProgressImage
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

        public ICommand GuessLetterCommand { get; set; }
        public ICommand NewRoundCommand { get; set; }
        public ICommand ViewHistoryCommand { get; set; }

        public HangmanGameViewModel()
        {
            GuessLetterCommand = new ActionCommand<char>(this.GuessLetter);
            NewRoundCommand = new ActionCommand(this.StartNewRound);
            ViewHistoryCommand = new ActionCommand<Window>(this.OpenHistoryWindow);

            InitializeRound();
        }

        private void OpenHistoryWindow(Window view)
        {
            view.Show();
        }

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

            CheckWinOrLoss();
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
            _RoundManager = new HangmanRoundManager(_DataFetcher.FetchRandomWord(), Tries);
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
            _ProgressImages = new Queue<ImageSource>();

            foreach (var dataset in _DataFetcher.FetchRandomImageSet())
            {
                _ProgressImages.Enqueue((ImageSource)new ImageSourceConverter().ConvertFrom(dataset));
            }

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
                ProgressImage = (BitmapSource)_ProgressImages.Dequeue();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CheckWinOrLoss()
        {
            if (CheckWinCondition())
            {
                OnRoundWon();
            }

            if (CheckLoseCondition())
            {
                OnRoundLost();
            }
        }

        private bool CheckWinCondition()
        {
            if (MaskedWord == _RoundManager.WordToGuess)
            {
                return true;
            }

            return false;
        }

        private bool CheckLoseCondition()
        {
            if (_RoundManager.TriesLeft < 1)
            {
                return true;
            }

            return false;
        }

        private void StartNewRound()
        {
            _RoundManager.StartNew(_DataFetcher.FetchRandomWord(), Tries);

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

            PublishRoundResults();

            StartNewRound();
        }

        private void OnRoundLost()
        {
            MessageBox.Show("Round lost");

            PublishRoundResults();

            StartNewRound();
        }

        private void PublishRoundResults()
        {
            var message = new HangmanRoundMessage
            {
                Word = _RoundManager.WordToGuess,
                Won = CheckWinCondition()
            };

            MessageBus.Instance.Publish<HangmanRoundMessage>(message);
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

        #endregion

    }
}
