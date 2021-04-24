using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Ultilities
{
    public class EmailSender
    {
        public void Send(string subject, string body, string receiver)
        {
            try
            {
                using(SmtpClient sender = new SmtpClient("smtp.gmail.com"))
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("trieunguyen.03072000@gmail.com");
                    mail.To.Add(receiver);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    sender.Port = 587;
                    sender.Credentials = new System.Net.NetworkCredential("trieunguyen.03072000@gmail.com", "frixnkhtxylydnxt");
                    sender.EnableSsl = true;
                    sender.Send(mail);
                }    
            }
            catch (Exception ex)
            {
                throw new Exception("gửi Email bị lỗi :" + ex.Message);
            }
        }
    }
}
