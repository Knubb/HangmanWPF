using Hangman.DesktopClient.Commands;
using Hangman.DesktopClient.Enums;
using Hangman.DesktopClient.Models;
using Hangman.DesktopClient.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Hangman.DesktopClient.ViewModels
{

    public class HangmanOptionsViewModel : BaseViewModel
    {
        public ObservableCollection<ImageSource> SelectedImageSetCollection { get; private set; } = new ObservableCollection<ImageSource>();

        private GraphicsOption _currentGraphicsOption; 
        public GraphicsOption CurrentGraphicsOption
        {
            get
            {
                return _currentGraphicsOption;
            }
            private set
            {
                _currentGraphicsOption = value;
                NotifyPropertyChanged(this, nameof(CurrentGraphicsOption));
            }
        }

        public ICommand GoToUploadGraphicsWindow { get; private set; }
        public ICommand GoToSelectGraphicsWindow { get; private set; }

        public ICommand ChangeGraphicsOptionCommand { get; private set; }


        public HangmanOptionsViewModel()
        {
            GoToUploadGraphicsWindow = new ActionCommand(OpenUploadGraphicsDialog);
            GoToSelectGraphicsWindow = new ActionCommand(OpenSelectGraphicsDialog);
            ChangeGraphicsOptionCommand = new ActionCommand<GraphicsOption>(SetGraphicsOption);

            LoadFromSavedSettings();
        }

        private void SetGraphicsOption(GraphicsOption option)
        {
            CurrentGraphicsOption = option;
            SettingsContainer.HangmanOptions.GraphicsOption = CurrentGraphicsOption;
        }

        private void OpenUploadGraphicsDialog()
        {
            var view = new UploadImageSetWindow();

            view.ShowDialog();
        }

        private void OpenSelectGraphicsDialog()
        {
            var view = new SelectImageSetWindow();

            if (view.ShowDialog() == true)
            {
                var result = view.ViewModel.SelectedImageSet;

                SetSelectedImageSet(result);
            }
        }

        private void SetSelectedImageSet(IEnumerable<ImageSource> imageset)
        {
            if (SelectedImageSetCollection.Count > 0)
            {
                SelectedImageSetCollection.Clear();
            }

            foreach (var item in imageset)
            {
                SelectedImageSetCollection.Add(item);
            }


            SettingsContainer.HangmanOptions.SelectedImageSetData = ImageDataTransformHelper.CreateDataCollectionFromImages(imageset);
            SetGraphicsOption(GraphicsOption.UseSelected);
        }

        private void LoadFromSavedSettings()
        {
            CurrentGraphicsOption = SettingsContainer.HangmanOptions.GraphicsOption;

            if (CurrentGraphicsOption == GraphicsOption.UseSelected && SettingsContainer.HangmanOptions.SelectedImageSetData != null)
            {
                SetSelectedImageSet(ImageDataTransformHelper.CreateImageCollectionFromData(SettingsContainer.HangmanOptions.SelectedImageSetData));
            }
        }
    }
}
