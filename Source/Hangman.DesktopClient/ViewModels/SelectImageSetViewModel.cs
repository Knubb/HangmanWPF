using Hangman.DesktopClient.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Hangman.DesktopClient.Commands;
using Hangman.DesktopClient.Models;

namespace Hangman.DesktopClient.ViewModels
{
    public class SelectImageSetViewModel : BaseViewModel, IDialogViewModel
    {

        public ObservableCollection<List<ImageSource>> SelectableImageSetsCollection { get; set; } = new ObservableCollection<List<ImageSource>>();

        public List<ImageSource> SelectedImageSet { get; set; }

        public ICommand MarkSelectionCommand { get; set; }

        public ICommand ReturnResultsCommand { get; set; }

        public SelectImageSetViewModel()
        {
            MarkSelectionCommand = new ActionCommand<List<ImageSource>>(SetSelection);
            ReturnResultsCommand = new ActionCommand<Window>(CloseWindowAndReturnTrue, CanReturnResult);

            FetchImageSetsAndAddToCollection();
        }

        private void SetSelection(List<ImageSource> selectedimageset)
        {
            SelectedImageSet = selectedimageset;
        }

        private void FetchImageSetsAndAddToCollection()
        {
            foreach (IEnumerable<byte[]> dataset in RepositoryContainer.ImageSets.GetAll())
            {
                var imagearray = ImageDataTransformHelper.CreateImageCollectionFromData(dataset);

                SelectableImageSetsCollection.Add(imagearray);
            }
        }

        public void CloseWindowAndReturnTrue(Window window)
        {
            window.DialogResult = true;
        }

        public bool CanReturnResult()
        {
            if (SelectedImageSet == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
