using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HJotelManagement.Domain
{
    public class BookRoom
    {
        public int Id { get; set; }
        [Required]
        public string RoomNumber { get; set; } 
        [Required]
        public string Email { get; set; }
        [Required]
        public string CustomerName { get; set; } 
        public int RoomId { get; set; }
        public string DateBooked { get; set; }
        public Room Room { get; set; }
    }
}
