using HangmanWPF.Commands;
using HangmanWPF.Views;
using System.Windows;
using System.Windows.Input;

namespace HangmanWPF.ViewModels
{

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

        public ICommand CloseApplicationCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        public HomeMenuViewModel()
        {
            CloseApplicationCommand = new ActionCommand(this.CloseApplication);
            NavigateCommand = new ActionCommand<Pages>(this.NavigateTo);
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
