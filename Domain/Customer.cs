using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HJotelManagement.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public bool IsCheckin { get; set; }
        public bool IsCheckout { get; set; }
        [Required]
        public List<Room> Rooms { get; set; }
        public string RegistrationDate { get; set; }
    }
}
