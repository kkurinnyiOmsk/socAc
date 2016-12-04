using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace CommonC.Helpers
{
    public class MailHelper
    {
        private readonly SmtpClient client;
        private readonly List<string> adminEmails;
        private readonly string fromEmail = "andrey45779@gmail.com"; 
        private readonly string passwordEmail = "susalo17111989";
        public MailHelper()
        {
            client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmail, passwordEmail);
            client.EnableSsl = true;

            adminEmails = new List<string>
            {
                "gragbd@mail.ru"
            };  
        }

        public void SendMail(string mailText)
        {
            foreach (var email in adminEmails)
            {
                client.Send(fromEmail, email, "error", mailText);
            }
        }
    }
}
