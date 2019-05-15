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
    public class UploadImageSetViewModel : BaseViewModel, IDialogViewModel
    {
        private const int ImageSizeLimitInBytes = 8388608;
        private const int MinimumImageHeigth = 250;
        private const int MinimumImageWidth = 250;
        private const int ImageSetSize = 9;

        public ObservableCollection<ImageSource> ImageCollection { get; private set; } = new ObservableCollection<ImageSource>();

        public ICommand SelectImageFromExplorerCommand { get; set; }
        public ICommand ReturnResultsCommand { get; set; }

        public UploadImageSetViewModel()
        {
            SelectImageFromExplorerCommand = new ActionCommand<int>(this.OpenExplorerAndSetImage);
            ReturnResultsCommand = new ActionCommand<Window>(this.CloseWindowAndReturnTrue, this.CanReturnResult);

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
            filedialog.Multiselect = false;

            if (filedialog.ShowDialog() == true)
            {
                if (!IsImageValidForUpload(filedialog.FileName))
                {
                    return;
                }

                var imagedata = File.ReadAllBytes(filedialog.FileName);

                ImageSource selectedimage = (ImageSource)new ImageSourceConverter().ConvertFrom(imagedata);

                ImageCollection[imagenumber] = selectedimage;
            }
        }

        private bool IsImageValidForUpload(string filepath)
        {
            var sizeok = IsFileSizeUnderLimit(filepath);
            var dimensionsok = IsImageDimensionsOverMinimumLimit(filepath);

            if (!sizeok)
            {
                MessageBox.Show($"File is too big! Please keep the size under {ImageSizeLimitInBytes / 1048576 }MB");
            }
            else if (!dimensionsok)
            {
                MessageBox.Show($"Image dimensions are too small ! Please make the image bigger than {MinimumImageWidth} x {MinimumImageHeigth}");
            }

            return sizeok && dimensionsok;
        }

        private bool IsFileSizeUnderLimit(string filepath)
        {
            return new FileInfo(filepath).Length < ImageSizeLimitInBytes;
        }

        private bool IsImageDimensionsOverMinimumLimit(string filepath)
        {
            int width;
            int height;

            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bitmapFrame = BitmapFrame.Create(stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                width = bitmapFrame.PixelWidth;
                height = bitmapFrame.PixelHeight;
            }

            return width >= MinimumImageWidth && height >= MinimumImageHeigth;
        }

        public void CloseWindowAndReturnTrue(Window window)
        {
            Upload();

            window.DialogResult = true;
        }

        public bool CanReturnResult()
        {
            return IsImageSetComplete();
        }

        private void Upload()
        {
            //Do some validation shenanigans
            //TODO: Check for duplicate images (Easiest solution requires NET Core 2.1 or .NET Standard 2.1)
            // https://stackoverflow.com/questions/43289/comparing-two-byte-arrays-in-net
            // https://docs.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=netstandard-2.1

            RepositoryContainer.ImageSets.InsertImageSet(ImageDataTransformHelper.CreateDataCollectionFromImages(ImageCollection));

            MessageBox.Show("Imagset uploaded");
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
