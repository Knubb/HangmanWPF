using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HangmanWPF.Commands
{
    public class GuessLetterCommand : ICommand
    {

        private Action<string> methodToExecute;
        private Func<string, bool> canExecuteEvaluator;

        public GuessLetterCommand(Action<string> methodToExecute, Func<string, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public GuessLetterCommand(Action<string> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecuteEvaluator.Invoke((string)parameter);
                return result;
            }
        }

        public void Execute(object parameter)
        {
            methodToExecute.Invoke((string)parameter);
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
