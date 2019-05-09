using HangmanWPF.Models;
using System;
using System.Windows;

namespace HangmanWPF.Models
{
    public class BaseDialogWindow<T> : Window where T : IDialogViewModel
    {

        public T ViewModel { get => GetViewModel(); }

        private T GetViewModel()
        {
            var resources = this.Resources.Keys;

            foreach (var key in resources)
            {
                var value = this.Resources[key];

                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
            }

            return default;
        }
    }
}
