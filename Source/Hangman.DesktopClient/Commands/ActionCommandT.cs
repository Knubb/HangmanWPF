using System;
using System.Windows.Input;

namespace Hangman.DesktopClient.Commands
{
    class ActionCommand<T> : BaseActionCommand, ICommand
    {
        private Action<T> _methodToExecute;

        public ActionCommand(Action<T> methodToExecute, Func<bool> canExecuteEvaluator):base(canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
        }
        public ActionCommand(Action<T> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public void Execute(object parameter)
        {
            _methodToExecute.Invoke((T)parameter);
        }
    }
}
