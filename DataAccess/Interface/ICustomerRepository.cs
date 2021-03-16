
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HJotelManagement.Domain; 

namespace DataAccess.Interface
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        int CustomerCount();
         
        Task<Customer> GetCustomerByCheckin(bool checkin);
        Task<Customer> GetLastCustomer();
        Task<BookRoom> GetOrderByEmailAndDate(string email, DateTime datetime);
        Task<Customer> GetCustomerById(int Id); 
    }
}
