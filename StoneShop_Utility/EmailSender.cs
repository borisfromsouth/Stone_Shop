using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StoneShop.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSettings _emailSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            _emailSettings = _configuration.GetSection("EmailConfirm").Get<EmailSettings>();
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(_emailSettings.SmtpServer, "Alex"); // от кого
                message.To.Add(new MailAddress(email));  // список адресов кому отправляется письмо balabin.nv@gmail.com
                message.Subject = subject;  // тема письма
                message.Body = body;  // сообщение в письме (в данном случае html-код, потому что message.IsBodyHtml = true;)  
                //message.Attachments.Add(new Attachment("video.mp4"));  //... путь к файлу ... пркрепленный файл к письму

                using (SmtpClient client = new SmtpClient("smtp.yandex.ru"))
                {
                    client.Credentials = new NetworkCredential(_emailSettings.SmtpServer, _emailSettings.SecretKey);  // пароль: fakkbvvvsbmzxoip
                    client.Port = 587;
                    client.EnableSsl = true;

                    client.Send(message);
                }
            }
            catch (Exception ex) { }
        }
    }
}
