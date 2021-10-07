using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace WinnersIndy.Common
{
    public class SendEmail
    {
        public void SendNotification( string to, string body, string subject )
        {

            
           
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("foluso.o.adegboye@gmail.com");
                string[] multEmail = to.Split(';');
                foreach (string item in multEmail)
                {
                    message.To.Add(new MailAddress(item));
                }
                //message.To.Add(new MailAddress(to));

                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("foluso.o.adegboye@gmail.com", "Olasehinde1$");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Send(message);

            }
            catch (Exception ex)
            {
            }
        }

}   }