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
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    { 
        public RoomRepository(HotelContext context) : base(context)
        { 
        }

        public int RoomCount()
        {
            var count = _context.Rooms.Count();
            return count;
        }

        public async Task<List<Room>> RoomList(int customerId)
        {
            var items = await _context.Rooms.Where(x => x.CustomerId == customerId).ToListAsync();
            return items;
        }

        public async Task<List<Room>> GetAllRoomList()
        {
            var items = await _context.Rooms.OrderBy(x => x.Id).ToListAsync();
            return items;
        }

        public async Task<Room> GetRoomById(int Id)
        {
            var item = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == Id);
            return item;
        }

        public async Task<Room> GetRoomByStatus(bool isFree)
        {
            var item = await _context.Rooms.FirstOrDefaultAsync(x => x.IsFree == isFree);
            return item;
        }
         



    }
}
