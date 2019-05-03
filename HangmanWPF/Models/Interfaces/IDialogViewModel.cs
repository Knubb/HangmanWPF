using System.Windows;
using System.Windows.Input;

namespace HangmanWPF.Models
{
    public interface IDialogViewModel
    {

        ICommand ReturnResultsCommand { get; set; }

        void CloseWindowAndReturnResults(Window window);

        bool CanReturnResult();

    }
}
