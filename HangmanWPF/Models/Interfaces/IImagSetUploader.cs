using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF.Models
{
    public interface IImagSetUploader
    {
        void InsertImageSet(IList<byte[]> images);
    }
}
