using System.Windows;
using System.Windows.Input;

namespace HangmanWPF.Interfaces
{
    // Probably not needed.
    public interface IDialogViewModel
    {
        ICommand ReturnResultsCommand { get; set; }

        void CloseWindowAndReturnTrue(Window window);

        bool CanReturnResult();

    }
}
