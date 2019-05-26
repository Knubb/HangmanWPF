using Hangman.DesktopClient.Commands;
using Hangman.DesktopClient.Enums;
using System.Windows;
using System.Windows.Input;

namespace Hangman.DesktopClient.ViewModels
{

    public class HomeMenuViewModel : BaseViewModel
    {

        private Pages _currentPage;
        public Pages CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                NotifyPropertyChanged(this, nameof(CurrentPage));
            }
        }

        public ICommand CloseApplicationCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        public HomeMenuViewModel()
        {
            CloseApplicationCommand = new ActionCommand(CloseApplication);
            NavigateCommand = new ActionCommand<Pages>(NavigateTo);
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private void NavigateTo(Pages page)
        {
            CurrentPage = page;
        }
    }
}
