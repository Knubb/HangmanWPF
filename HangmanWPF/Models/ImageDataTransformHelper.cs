using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HangmanWPF.Models
{
    public static class ImageDataTransformHelper
    {

        public static List<ImageSource> CreateImageCollectionFromData(IEnumerable<byte[]> dataset)
        {
            List<ImageSource> imagearray = new List<ImageSource>();

            foreach (byte[] data in dataset)
            {
                imagearray.Add(ConvertDataToImageSource(data));
            }

            return imagearray;
        }

        public static List<byte[]> CreateDataCollectionFromImages(IEnumerable<ImageSource> images)
        {
            List<byte[]> datacollection = new List<byte[]>();

            foreach (var image in images)
            {
                datacollection.Add(ConvertImageSourceToData(image));
            }

            return datacollection;
        }

        public static ImageSource ConvertDataToImageSource(byte[] imagedata)
        {
            return (ImageSource)new ImageSourceConverter().ConvertFrom(imagedata);
        }

        public static byte[] ConvertImageSourceToData(ImageSource image)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();

            byte[] bytes = null;


            if (image is BitmapSource bitmapSource)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }
    }
}
