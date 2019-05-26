using Hangman.DesktopClient.Interfaces;
using System.Windows;

namespace Hangman.DesktopClient.Models
{
    public class BaseDialogWindow<T> : Window where T : IDialogViewModel
    {

        public T ViewModel => GetViewModel();

        private T GetViewModel()
        {
            var resources = Resources.Keys;

            foreach (var key in resources)
            {
                var value = Resources[key];

                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
            }
            // Maybe it's better to throw an exception. Maybe it is an exceptional situation.
            return default;
        }
    }
}
