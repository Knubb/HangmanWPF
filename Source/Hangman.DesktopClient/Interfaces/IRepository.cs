using System.Collections.Generic;

namespace HangmanWPF.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get();
        void Create(T entity);
    }
}
