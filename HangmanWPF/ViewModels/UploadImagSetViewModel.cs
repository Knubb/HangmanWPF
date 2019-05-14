using HangmanWPF.Commands;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using HangmanWPF.Models;

namespace HangmanWPF.ViewModels
{
    public class UploadImagSetViewModel : BaseViewModel
    {
        private const int ImageSetSize = 9;

        public ObservableCollection<ImageSource> ImageCollection { get; private set; } = new ObservableCollection<ImageSource>();

        public ICommand SelectImageFromExplorerCommand { get; set; }
        public ICommand UploadImagesCommand { get; set; }

        public UploadImagSetViewModel()
        {
            SelectImageFromExplorerCommand = new ActionCommand<int>(this.OpenExplorerAndSetImage);
            UploadImagesCommand = new ActionCommand(this.Upload, this.IsImageSetComplete);

            //We need to pre-initialize the elements so we can work wíth indices right off the bat, our view binds to 0-7, and we assign new values to these elements by number.
            //Note: We already know what size this collection needs to be, but can't use arrays since they don't implement INotifyCollectionChanged
            for (int i = 0; i < ImageSetSize; i++)
            {
                ImageCollection.Add(null);
            }
        }

        private void OpenExplorerAndSetImage(int imagenumber)
        {
            var filedialog = new OpenFileDialog();

            filedialog.Filter = "Image files(*.png;*.jpg;*.bmp;*.jpeg )| *.png;*.jpg;*.bmp;*.jpeg| All files| *.*";

            if (filedialog.ShowDialog() == true)
            {
                ImageSource selectedimage;

                var imagedata = File.ReadAllBytes(filedialog.FileName);

                selectedimage = (ImageSource)new ImageSourceConverter().ConvertFrom(imagedata);

                ImageCollection[imagenumber] = selectedimage;
                
            }          
        }

        private void Upload()
        {
            //Do some validation shenanigans
            //TODO: Check for duplicate images (Easiest solution requires NET Core 2.1 or .NET Standard 2.1)
            // https://stackoverflow.com/questions/43289/comparing-two-byte-arrays-in-net
            // https://docs.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=netstandard-2.1

            //TODO: Invert dependecy
            IImagSetUploader uploader = new HangmanDataUploaderSQLite();

            uploader.InsertImageSet(ImageDataTransformHelper.CreateDataCollectionFromImages(ImageCollection));

            MessageBox.Show("Imagset uploaded (hopefully)");
        }

        private bool IsImageSetComplete()
        {
            foreach (var item in ImageCollection)
            {
                if (item == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
