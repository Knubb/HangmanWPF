using HangmanWPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public HomeMenuViewModel()
        {

            NavigateToHangmanCommand = new ActionCommand(this.NavigateTo);
            NavigatHomeCommand = new ActionCommand(this.NavigateHome);
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
