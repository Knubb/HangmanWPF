using System;
using System.Windows.Input;

namespace Hangman.DesktopClient.Commands
{
    class BaseActionCommand
    {
        private Func<bool> _canExecuteEvaluator;

        public BaseActionCommand(Func<bool> canExecuteEvaluator)
        {
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteEvaluator == null)
            {
                return true;
            }

            return _canExecuteEvaluator.Invoke();
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
