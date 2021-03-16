using DataAccess.Interface; 
using HJotelManagement.DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly HotelContext _context;

        public BaseRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }
        public void CreateList(List<T> entity)
        {
            _context.AddRange(entity);
        }
         

        public void DeleteRange(List<T> entity)
        {
            _context.RemoveRange(entity);
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
         
    }
}
