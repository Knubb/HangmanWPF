using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace HangmanWPF.Models
{
    public class ImageSetEnumerator : IEnumerator<ImageSource>
    {
        private ImageSource[] _Images;
        private int _CurrentIndex = -1;

        public ImageSource Current { get; private set; } = default; //null

        public bool IsInitialized { get; set; } = false;

        object IEnumerator.Current => Current;

        public void InitializeNewCollection(IEnumerable<ImageSource> imageset)
        {
            _Images = imageset.ToArray();
            Reset();
            IsInitialized = true;
        }
     
        public bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++_CurrentIndex >= _Images.Length)
            {
                return false;
            }
            else
            {
                Current = _Images[_CurrentIndex];
            }
            return true;
        }

        public void Reset()
        {
            _CurrentIndex = -1;
        }
       

        public void Dispose() { }
    }
}
