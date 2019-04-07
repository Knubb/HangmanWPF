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

        private Action<char> methodToExecute;
        private Func<char, bool> canExecuteEvaluator;

        public GuessLetterCommand(Action<char> methodToExecute, Func<char, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }
        public GuessLetterCommand(Action<char> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecuteEvaluator == null)
            {
                return true;
            }


            if (parameter is string s)
            {
                bool result = this.canExecuteEvaluator.Invoke(Char.Parse(s));
                return result;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            methodToExecute.Invoke((char)parameter);
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
