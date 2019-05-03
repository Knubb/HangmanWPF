using HangmanWPF.Commands;
using System.Windows;
using System.Windows.Input;

namespace HangmanWPF.ViewModels
{

    public enum Pages
    {
        HomeMenu,
        Hangman,
        Settings
    }

    public class HomeMenuViewModel : BaseViewModel
    {

        private Pages _CurrentPage;
        public Pages CurrentPage
        {
            get { return _CurrentPage; }
            set
            {
                _CurrentPage = value;
                NotifyPropertyChanged(this, nameof(CurrentPage));
            }
        }

        public ICommand NavigateToHangmanCommand { get; set; }
        public ICommand NavigatHomeCommand { get; set; }
        public ICommand CloseApplicationCommand { get; set; }

        public ICommand InsertImageCommand { get; set; }

        public HomeMenuViewModel()
        {
            NavigateToHangmanCommand = new ActionCommand(this.NavigateToHangman);
            NavigatHomeCommand = new ActionCommand(this.NavigateHome);
            CloseApplicationCommand = new ActionCommand(this.CloseApplication);

            //InsertImageCommand = new ActionCommand(this.InsertImageSet);

        }

        //TODO: Move method to appropriate viewmodel (once created)
        
        //private void InsertImageSet()
        //{

        //    List<byte[]> files = new List<byte[]>();

        //    foreach (var filepath in Directory.GetFiles("C:\\Users\\knubb\\OneDrive\\Egna projekt\\Git\\Repositories\\HangmanWPF\\HangmanWPF\\HangmanData\\Images"))
        //    {
        //        files.Add(File.ReadAllBytes(filepath));
        //    }

        //    var db = new HangmanDataFetcherSQLite();

        //    db.InsertImageSet(files);

        //    MessageBox.Show("Imageset inserted");

        //}

        private void CloseApplication()
        {
            //TODO
            // Refactor to some central closing mechanism so we can make sure to do all neccessary closing stuff no matter from where the app is closed

            Application.Current.Shutdown();
        }

        public void NavigateToHangman()
        {
            CurrentPage = Pages.Hangman;
        }

        public void NavigateHome()
        {
            CurrentPage = Pages.HomeMenu;
        }
    }
}
