using HangmanWPF.Commands;
using System.Windows;
using System.Windows.Input;

namespace HangmanWPF.ViewModels
{

    public enum Pages
    {
        HomeMenu,
        Hangman
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

        public HomeMenuViewModel()
        {

            NavigateToHangmanCommand = new ActionCommand(this.NavigateTo);
            NavigatHomeCommand = new ActionCommand(this.NavigateHome);
            CloseApplicationCommand = new ActionCommand(this.CloseApplication);
        }


        private void CloseApplication()
        {
            //TODO
            // Refactor to some central closing mechanism so we can make sure to do all neccessary closing stuff no matter from where the app is closed

            Application.Current.Shutdown();
        }

        public void NavigateTo()
        {
            CurrentPage = Pages.Hangman;
        }

        public void NavigateHome()
        {
            CurrentPage = Pages.HomeMenu;
        }

    }
}
