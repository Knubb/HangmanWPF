using HangmanWPF.Commands;
using HangmanWPF.Models;
using HangmanWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HangmanWPF.ViewModels
{
    public class HangmanGameViewModel : BaseViewModel
    {
        private const int _Tries = 8;

        private Stack<string> _CachedWords = new Stack<string>();
        protected Stack<string> CachedWords
        {
            get
            {
                if (_CachedWords.Count < 1)
                {
                    PopulateCache();
                }

                return _CachedWords;
            }
        }

        private void PopulateCache()
        {
            foreach (var word in RepositoryContainer.Words.FetchRandomSetOfWords(50))
            {
                _CachedWords.Push(word.ToUpper());
            }
        }

        //Helper classes with state
        private HangmanRoundManager _RoundManager { get; set; } = new HangmanRoundManager();
        private ImageSetEnumerator _ImageSetProgresser = new ImageSetEnumerator();

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

        public ICommand GuessLetterCommand { get; set; }
        public ICommand NewRoundCommand { get; set; }
        public ICommand ViewHistoryCommand { get; set; }
        public ICommand ViewOptionsCommand { get; set; }

        public HangmanGameViewModel()
        {
            GuessLetterCommand = new ActionCommand<char>(this.GuessLetter);
            NewRoundCommand = new ActionCommand(this.StartNewRound);
            ViewHistoryCommand = new ActionCommand(this.OpenHistoryWindow);
            ViewOptionsCommand = new ActionCommand(this.OpenOptionsWindow);

            InitializeLettersCollection();
            StartNewRound();
        }

        private void OpenOptionsWindow()
        {
            var v = new HangmanOptionsWindow();
            v.ShowDialog();
        }

        private void OpenHistoryWindow()
        {
            var v = new WordHistoryWindow();
            v.ShowDialog();
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

                SetNextImageInSet();
            }

            CheckWinOrLoss();
        }

        private void StartNewRound()
        {
            foreach (var lettervm in LettersCollection)
            {
                lettervm.UpdateState(LetterState.NoGuess);
            }

            _RoundManager.StartNew(CachedWords.Pop(), _Tries);
            InitializeMaskedWord();
            InitializeProgressImages();
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

            _ImageSetProgresser.Reset();

            switch (SettingsContainer.HangmanOptions.GraphicsOption)
            {
                case GraphicsOption.RandomizeOnce:

                    if (!_ImageSetProgresser.IsInitialized)
                    {
                        _ImageSetProgresser.InitializeNewCollection(ImageDataTransformHelper.CreateImageCollectionFromData(RepositoryContainer.ImageSets.FetchRandomImageSet()));
                    }
                    //Else: Use the same imageset again.


                    break;
                case GraphicsOption.RandomizeEachRound:

                    _ImageSetProgresser.InitializeNewCollection(ImageDataTransformHelper.CreateImageCollectionFromData(RepositoryContainer.ImageSets.FetchRandomImageSet()));

                    break;
                case GraphicsOption.UseSelected:

                    _ImageSetProgresser.InitializeNewCollection(ImageDataTransformHelper.CreateImageCollectionFromData(SettingsContainer.HangmanOptions.SelectedImageSetData));

                    break;
            }

            SetNextImageInSet();
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

        private void SetNextImageInSet()
        {
            _ImageSetProgresser.MoveNext();
            ProgressImage = _ImageSetProgresser.Current as BitmapSource;
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
            RepositoryContainer.GameRecords.InsertHistoryRecord(new HangmanGameRecord(_RoundManager.WordToGuess, CheckWinCondition()));
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
