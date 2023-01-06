using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;

namespace StoneShop.Utility
{
    public class Service
    {
        private readonly ILogger<Service> _logger;

        public Service(ILogger<Service> logger)
        {
            _logger = logger;
        }

        public void SendEmailDefault()
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("sashaborisenko@tut.by", "Саша"); // от кого
                message.To.Add("sashaborisenko@tut.by");  // список адресов кому отправляется письмо
                message.Subject = "Сообщение от System.Net.Mail";  // тема письма
                message.Body = "<div style=\"color: red;\"></div>";  // сообщение в письме (в данном случае html-код, потому что message.IsBodyHtml = true;)  
                //message.Attachments.Add(new Attachment("... путь к файлу ..."));  // пркрепленный файл к письму

                using (SmtpClient client = new SmtpClient("smtp.yandex.ru"))
                {
                    client.Credentials = new NetworkCredential("testdriveborisenko@yandex.by", "xxqhxznarpiymrsa");  // пароль: xxqhxznarpiymrsa
                    client.Port = 465;
                    client.EnableSsl = true;

                    client.Send(message);
                    _logger.LogInformation("Сообщение отправлено успешно");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message);
            }
        }

        public void SendEmailCustom()
        {
            try
            {
                _logger.LogInformation("Сообщение отправлено успешно");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Сообщение отправлено успешно");
            }
        }
    }
}
