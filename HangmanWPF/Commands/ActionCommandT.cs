using System;
using System.Windows.Input;

namespace HangmanWPF.Commands
{
    class ActionCommand<T> : ICommand
    {
        private Action<T> _MethodToExecute;
        private Func<bool> _CanExecuteEvaluator;

        public ActionCommand(Action<T> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _MethodToExecute = methodToExecute;
            _CanExecuteEvaluator = canExecuteEvaluator;
        }
        public ActionCommand(Action<T> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (_CanExecuteEvaluator == null)
            {
                return true;
            }

            return _CanExecuteEvaluator.Invoke();
        }

        public void Execute(object parameter)
        {
            _MethodToExecute.Invoke((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
