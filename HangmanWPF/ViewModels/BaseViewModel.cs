using System.ComponentModel;

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
