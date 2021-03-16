using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HJotelManagement.Domain
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public bool IsFree { get; set; }
        [Required]
        public string DateCreated { get; set; }
        public string DateBooked { get; set; }
        public bool Occupancy { get; set; }
        public int Amount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<BookRoom> bookRooms { get; set; }

    }
}
