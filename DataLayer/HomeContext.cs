using HJotelManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace HJotelManagement.DataLayer
{
    public class HotelContext : DbContext
    {

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookRoom> BookRooms { get; set; }
    }
}
