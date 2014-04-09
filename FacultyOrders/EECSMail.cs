using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace FacultyOrders
{
    public class EECSMail
    {
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
        MailMessage message = new MailMessage();
        MailAddress fromAddress = new MailAddress("FacultyOrders@eecs.ucf.edu");

        public EECSMail(String to, String subject, String body)
        {
            message.From = fromAddress;
            message.Subject = subject;
            message.Body = body;
            message.To.Add(to);

            SmtpServer.Port = 25;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("facultyorders.eecs", "eecsorders");
            SmtpServer.EnableSsl = true;

            
        }

        public void sendMail()
        {
            SmtpServer.Send(message);
        }
            
    }
}