using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IBaseRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        void Create(T entity); 
        Task<int> Save(); 
        IEnumerable<T> Filter(Func<T, bool> predicate);
        T Find(Func<T, bool> predicate);
        void CreateList(List<T> entity); 
    }
}
