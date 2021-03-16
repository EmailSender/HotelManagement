using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using MailKit.Net.Smtp;
using System.Threading.Tasks; 
using HJotelManagement.Domain;

namespace HJotelManagement.Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        public readonly IHostingEnvironment env = null;

        public EmailService(IHostingEnvironment env)
        {
            this.env = env;
        }
        public string SendEmail(EmailModel model)
        {
            try
            {
                MimeMessage mm = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Administrator", model.Email);
                mm.Subject = model.Subject; 
                mm.From.Add(from);

                MailboxAddress to = new MailboxAddress("User", model.To);
                mm.To.Add(to);

                BodyBuilder bodyBuilder = new BodyBuilder();
                //bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
                bodyBuilder.TextBody = model.Body;
                
                mm.Body = bodyBuilder.ToMessageBody();

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, true);
                    client.Authenticate(model.Email, model.Password);
                    client.Send(mm);
                    client.Disconnect(true); 
                // mm.IsBodyHtml = false;
                //using (SmtpClient smtp = new SmtpClient())
                //{
                //    smtp.Host = "smtp.gmail.com";
                //    smtp.EnableSsl = true;
                //    NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Credentials = NetworkCred;
                //    smtp.Port = 587;
                //    smtp.Send(mm); 
                //}

                return "Email sent, check you email for details";

            }
            catch (Exception ex)
            { 
                return "Could not send email";
            }

        }
    }
}
