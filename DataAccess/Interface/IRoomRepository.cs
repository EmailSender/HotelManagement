
using HJotelManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 

namespace DataAccess.Interface
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<List<Room>> RoomList(int customerId);
        Task<Room> GetRoomById(int Id);
        Task<List<Room>> GetAllRoomList();
        Task<Room> GetRoomByStatus(bool isFree);
        int RoomCount();

    }
}
