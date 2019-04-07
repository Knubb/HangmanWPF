using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(object sender, string propertyname)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyname));
        }
    }
}
