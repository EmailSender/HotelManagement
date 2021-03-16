using DataAccess.Interface;
using HJotelManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJotelManagement.DataAccess.Interface
{
    public interface IBookRoomRepository : IBaseRepository<BookRoom>
    {
        Task<List<BookRoom>> orderList(int customerId);
        Task<BookRoom> GetOrderById(int Id);
        Task<List<BookRoom>> GetAllOrderList();
        int AllOrdersCount();
        Task<List<int>> GetAllOrderAmount();
    }
}
