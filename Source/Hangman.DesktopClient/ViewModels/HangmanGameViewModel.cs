using Hangman.DesktopClient.Commands;
using Hangman.DesktopClient.Enums;
using Hangman.DesktopClient.Models;
using Hangman.DesktopClient.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Hangman.DesktopClient.ViewModels
{
    // This class has more than one reason to change. Split it. Split it so that 
    // by changing some related functionlity, the place where unrelated functionality exists
    // doesn't get effected.
    public class HangmanGameViewModel : BaseViewModel
    {
        private const int Tries = 8;

        private Stack<string> _cachedWords = new Stack<string>();
        public Stack<string> CachedWords
        {
            get
            {
                if (_cachedWords.Count < 1)
                {
                    PopulateCache();
                }

                return _cachedWords;
            }
        }

        private void PopulateCache()
        {
            foreach (var word in RepositoryContainer.Words.GetRandomSet(50))
            {
                _cachedWords.Push(word.ToUpper());
            }
        }

        private HangmanRoundManager RoundManager { get; set; } = new HangmanRoundManager();
        private ImageSetEnumerator _imageSetProgresser = new ImageSetEnumerator();

        public ObservableCollection<LetterViewModel> LettersCollection { get; set; }

        private string _maskedWord;
        public string MaskedWord
        {
            get { return _maskedWord; }
            set
            {
                _maskedWord = value;
                NotifyPropertyChanged(this, nameof(MaskedWord));
            }
        }

        private BitmapSource _progressImage;
        public BitmapSource ProgressImage
        {
            get
            {
                return _progressImage;
            }
            set
            {
                _progressImage = value;
                NotifyPropertyChanged(this, nameof(ProgressImage));
            }
        } 

        public ICommand GuessLetterCommand { get; set; }
        public ICommand NewRoundCommand { get; set; }
        public ICommand ViewHistoryCommand { get; set; }
        public ICommand ViewOptionsCommand { get; set; }

        public HangmanGameViewModel()
        {
            GuessLetterCommand = new ActionCommand<char>(GuessLetter);
            NewRoundCommand = new ActionCommand(StartNewRound);
            ViewHistoryCommand = new ActionCommand(OpenHistoryWindow);
            ViewOptionsCommand = new ActionCommand(OpenOptionsWindow);

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
            if (RoundManager.MakeGuess(character))
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

            RoundManager.Start(CachedWords.Pop(), Tries);
            InitializeMaskedWord();
            InitializeProgressImages();
        }

        private void InitializeLettersCollection()
        {
            LettersCollection = new ObservableCollection<LetterViewModel>();

            var letters = Application.Current.FindResource("Letters");

            foreach (var letter in letters as string[])
            {
                LettersCollection.Add(new LetterViewModel(char.Parse(letter)));
            }
        }

        private void InitializeProgressImages()
        {
            _imageSetProgresser.Reset();

            switch (SettingsContainer.HangmanOptions.GraphicsOption)
            {
                case GraphicsOption.RandomizeOnce:

                    if (!_imageSetProgresser.IsInitialized)
                    {
                        _imageSetProgresser.InitializeCollection(ImageDataTransformHelper.CreateImageCollectionFromData(RepositoryContainer.ImageSets.GetRandom()));
                    }
                    //Else: Use the same imageset again.


                    break;
                case GraphicsOption.RandomizeEachRound:

                    _imageSetProgresser.InitializeCollection(ImageDataTransformHelper.CreateImageCollectionFromData(RepositoryContainer.ImageSets.GetRandom()));

                    break;
                case GraphicsOption.UseSelected:

                    _imageSetProgresser.InitializeCollection(ImageDataTransformHelper.CreateImageCollectionFromData(SettingsContainer.HangmanOptions.SelectedImageSetData));

                    break;
            }

            SetNextImageInSet();
        }

        private void InitializeMaskedWord()
        {

            var sb = new StringBuilder();

            for (int i = 0; i < RoundManager.WordToGuess.Length; i++)
            {
                sb.Append("-");
            }

            MaskedWord = sb.ToString();
        }

        private void UpdateMaskedWord(char chartoinsert)
        {
            var sb = new StringBuilder(MaskedWord);

            foreach (var index in FindAllIndexesOf(RoundManager.WordToGuess, (chartoinsert)))
            {

                sb.Remove(index, 1);
                sb.Insert(index, chartoinsert.ToString());
            }

            MaskedWord = sb.ToString();
        }

        private void SetNextImageInSet()
        {
            _imageSetProgresser.MoveNext();
            ProgressImage = _imageSetProgresser.Current as BitmapSource;
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
            if (MaskedWord == RoundManager.WordToGuess)
            {
                return true;
            }

            return false;
        }

        private bool CheckLoseCondition()
        {
            if (RoundManager.TriesLeft < 1)
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
            RepositoryContainer.GameRecords.Create(new HangmanGameRecord(RoundManager.WordToGuess, CheckWinCondition()));
        }

        #region Helpers

        private int[] FindAllIndexesOf(string str, char character)
        {
            var res = new List<int>();

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
