using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF.Models
{
    public interface ISettings
    {
        /// <summary>
        /// Saves this objects members
        /// </summary>
        void Save();

        /// <summary>
        /// Poppulate this objects members with the saved values
        /// </summary>
        void LoadFromSource();
    }
}
