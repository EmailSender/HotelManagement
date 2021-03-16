using DataAccess.Interface;
using HJotelManagement.DataAccess.Interface;
using HJotelManagement.Domain;
using HJotelManagement.Infrastructure.EmailService; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJotelManagement.Controllers
{
     
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookRoomRepository _orderRepository;
        private readonly IEmailService _emailService;

        public HotelController(
                           ICustomerRepository customerRepository, IEmailService emailService,
                           IRoomRepository roomRepository,
                           IBookRoomRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _roomRepository = roomRepository;
            _emailService = emailService;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(Customer customer)
        {

            if (ModelState.IsValid)
            {
                if (customer.Fullname != null || customer.EmailAddress != null)
                {

                    var checkIfEmailExist = _customerRepository.Find(x => x.EmailAddress == customer.EmailAddress);
                    if (checkIfEmailExist != null)
                    {
                        return BadRequest(new { message = "Customer with this email already Exist!!!" });
                    }

                    var date = DateTime.Now;

                    var user = new Customer()
                    {
                        Fullname = customer.Fullname,
                        EmailAddress = customer.EmailAddress,
                        RegistrationDate = DateTime.Now.ToLocalTime().ToShortDateString(),
                        PhoneNumber = customer.PhoneNumber,
                        IsCheckin = false,
                        IsCheckout = false
                    };

                    _customerRepository.Create(user);
                    await _customerRepository.Save();

                    var emailContent = new EmailModel()
                    {
                        To = user.EmailAddress,
                        Email = "emailsender9174@gmail.com",
                        Password = "0987654321C.",
                        Subject = "Your Uploaded Files",
                        Body = "Dear " + user.Fullname + "\n\n\n  You are receiving this mail because you have just been registered as customer at our Hotel." +
                                          " \n\n " + "Fullname: " + user.Fullname + "\n" + "Email Address: " + user.EmailAddress + "\n" + "Check-in: " + user.IsCheckin
                                          + "Phone number: " + user.PhoneNumber + "\n" + "\n\n Regards. \n\n Adminstrator"

                    };
                    var send = _emailService.SendEmail(emailContent);
                    return Ok(new { user, send });



                }
                return BadRequest(new { message = "Error occurred, Customer Email and Fullname are required" });
            }
            return BadRequest(new { message = "Please fill in required fields" });
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoom(Room room)
        {

            if (ModelState.IsValid)
            {
                if (room.RoomNumber != null || room.Amount != 0)
                {

                    var checkIfRoomExist = _roomRepository.Find(x => x.RoomNumber == room.RoomNumber);
                    if (checkIfRoomExist != null)
                    {
                        return BadRequest(new { message = "A room with this Room number exist already Exist!!!" });
                    }

                    var date = DateTime.Now;

                    var newRoom = new Room()
                    {
                        RoomNumber = room.RoomNumber,
                        Amount = room.Amount,
                        DateCreated = DateTime.Now.ToLocalTime().ToShortDateString(),
                        DateBooked = "",
                        IsFree = true
                    };

                    _roomRepository.Create(newRoom);
                    await _roomRepository.Save();

                    return Ok(room);



                }
                return BadRequest(new { message = "Error occurred, Customer Email and Fullname are required" });
            }
            return BadRequest(new { message = "Please fill in required fields" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CustomerCharge(string email, DateTime dateTime)
        {
            var date = DateTime.Now;
            var customer = await _customerRepository.GetOrderByEmailAndDate(email, dateTime);
            var days = dateTime - date;
            var room = await _roomRepository.GetRoomById(customer.RoomId);
            var roomCharge = room.Amount;
            var customerCharge = roomCharge * days;

            return Ok(new { customer, customerCharge} );
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var totalRooms = await _roomRepository.GetAllRoomList();
            var freeRooms = await _roomRepository.GetRoomByStatus(true);
            var revenue = await _orderRepository.GetAllOrderAmount();
            var checkin = await _customerRepository.GetCustomerByCheckin(true);
            var checkiout = await _customerRepository.GetCustomerByCheckin(false);

            return Ok(new { totalRooms, freeRooms, revenue, checkin, checkiout });
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(BookRoom order)
        {

            if (ModelState.IsValid)
            {
                if (order.RoomNumber != null || order.Email != null)
                {

                    var checkIfRoomExist = _roomRepository.Find(x => x.RoomNumber == order.RoomNumber);
                    var customerbyId = _customerRepository.Find(x => x.EmailAddress == order.Email);
                    if (checkIfRoomExist == null)
                    {
                        return BadRequest(new { message = "the room with this room number doesn't exist" });

                    }

                    var newOrder = new BookRoom()
                    {
                        RoomNumber = order.RoomNumber,
                        CustomerName = order.CustomerName,
                        Email = order.Email,
                        DateBooked = DateTime.Now.ToLocalTime().ToShortDateString()
                    };

                    var updateRoom = await _roomRepository.GetRoomById(checkIfRoomExist.Id);

                    updateRoom.IsFree = false;
                    updateRoom.DateBooked = DateTime.Now.ToLocalTime().ToShortDateString();
                    updateRoom.Occupancy = true;

                    var customer = await _customerRepository.GetCustomerById(customerbyId.Id);
                    customer.IsCheckin = true;

                    _orderRepository.Create(newOrder);
                    await _orderRepository.Save();

                    var emailContent = new EmailModel()
                    {
                        To = order.Email,
                        Email = "emailsender9174@gmail.com",
                        Password = "0987654321C.",
                        Subject = "Your Uploaded Files",
                        Body = "Dear " + order.CustomerName + "\n\n\n  You are receiving this mail because you have just booked a room at our Hotel." +
                                         " \n\n " + "Email Address: " + order.Email + "\n" + "Room Number : " + order.RoomNumber + "\n" + "Check-in: " + customer.IsCheckin
                                         +   "\n\n Regards. \n\n Adminstrator"

                    };
                    var send = _emailService.SendEmail(emailContent);

                    return Ok(new { response = newOrder, updateRoom } );



                }
                return BadRequest(new { message = "Error occurred, The information does not match" });
            }
            return BadRequest(new { message = "Please fill in required fields" });
        }

    }
}
