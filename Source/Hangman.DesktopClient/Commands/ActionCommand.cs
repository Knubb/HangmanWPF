using System;
using System.Windows.Input;

namespace Hangman.DesktopClient.Commands
{
    class ActionCommand : BaseActionCommand, ICommand
    {
        private Action _methodToExecute;

        public ActionCommand(Action methodToExecute, Func<bool> canExecuteEvaluator):base(canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
        }

        public ActionCommand(Action methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public void Execute(object parameter)
        {
            _methodToExecute.Invoke();
        }

    }
}
