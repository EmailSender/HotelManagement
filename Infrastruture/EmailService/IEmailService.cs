using HJotelManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace HJotelManagement.Infrastructure.EmailService
{
    public interface IEmailService 
    {
        string SendEmail(EmailModel model); 
    }
}
