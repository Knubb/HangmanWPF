using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Hangman.DesktopClient.Models
{
    // Suspisious need of interface IEnumerator implementation
    public class ImageSetEnumerator : IEnumerator<ImageSource>
    {
        private ImageSource[] _images;
        private int _currentIndex = -1;

        public ImageSource Current { get; private set; }

        public bool IsInitialized { get; set; }

        object IEnumerator.Current => Current;

        public void InitializeCollection(IEnumerable<ImageSource> imageset)
        {
            _images = imageset.ToArray();
            Reset();
            IsInitialized = true;
        }
     
        public bool MoveNext()
        {
            var isEndOfCollection = ++_currentIndex >= _images.Length;
            if (isEndOfCollection)
            {
                return false;
            }
            else
            {
                Current = _images[_currentIndex];
            }
            return true;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public void Dispose() { }
    }
}
