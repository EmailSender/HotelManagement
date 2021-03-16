using DataAccess.Interface;
using HJotelManagement.DataLayer;
using HJotelManagement.Domain; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace DataAccess.Implementation
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(HotelContext context) : base(context)
        {
        }

        public async Task<Customer> GetLastCustomer()
        { 
            var user = await _context.Customers.OrderBy(x => x.Id).LastOrDefaultAsync();
            return user;
        }

        public int CustomerCount()
        {
            var count = _context.Customers.Count();
            return count;
        }

        public async Task<Customer> GetCustomerById(int Id)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }
         

        public async Task<Customer> GetCustomerByCheckin(bool isCheckin)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(x => x.IsCheckin == isCheckin);
            return user;
        }

        public async Task<BookRoom> GetOrderByEmailAndDate(string email, DateTime datetime)
        {
            var date = datetime.ToShortDateString();
            var order = await _context.BookRooms.FirstOrDefaultAsync(x => x.Email == email && x.DateBooked == date);
            return order;
        }

    }
}
