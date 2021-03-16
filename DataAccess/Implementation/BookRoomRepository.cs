using DataAccess.Implementation;
using HJotelManagement.DataAccess.Interface;
using HJotelManagement.DataLayer;
using HJotelManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJotelManagement.DataAccess.Implementation
{
    public class BookRoomRepository : BaseRepository<BookRoom>, IBookRoomRepository
    {
        public BookRoomRepository(HotelContext context) : base(context)
        {
        }

        public async Task<List<BookRoom>> orderList(int orderId)
        {
            var items = await _context.BookRooms.Where(x => x.Id == orderId).ToListAsync();
            return items;
        }

        public int AllOrdersCount()
        {
            var count = _context.BookRooms.Count();
            return count;
        }

        public async Task<List<BookRoom>> GetAllOrderList()
        {
            var items = await _context.BookRooms.OrderBy(x => x.Id).ToListAsync();
            return items;
        }

            public async Task<List<int>> GetAllOrderAmount()
        {
            var items = await _context.BookRooms.Select(x => x.Room.Amount).ToListAsync();
            return items;
        }

        public async Task<BookRoom> GetOrderById(int Id)
        {
            var item = await _context.BookRooms.FirstOrDefaultAsync(x => x.Id == Id);
            return item;
        }
         
    }
}
