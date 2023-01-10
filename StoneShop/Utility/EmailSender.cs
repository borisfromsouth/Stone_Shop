using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StoneShop.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("testdriveborisenko@yandex.ru", "Alex"); // от кого
                message.To.Add(new MailAddress(email));  // список адресов кому отправляется письмо balabin.nv@gmail.com
                message.Subject = subject;  // тема письма
                message.Body = body;  // сообщение в письме (в данном случае html-код, потому что message.IsBodyHtml = true;)  
                //message.Attachments.Add(new Attachment("video.mp4"));  //... путь к файлу ... пркрепленный файл к письму

                using (SmtpClient client = new SmtpClient("smtp.yandex.ru"))
                {
                    client.Credentials = new NetworkCredential("testdriveborisenko@yandex.ru", "fakkbvvvsbmzxoip");  // пароль: fakkbvvvsbmzxoip
                    client.Port = 587;
                    client.EnableSsl = true;

                    client.Send(message);
                }
            }
            catch (Exception ex) { }
        }
    }
}
