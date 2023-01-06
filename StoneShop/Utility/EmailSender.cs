using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
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
            MailjetClient client = new MailjetClient("1849179c639712b25373747961d13956", "069c8c5c0158d0d29c5a3ac900c9dae6")
            {
                //Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
             new JObject {
             {
               "From", new JObject {
                {"Email", "ben.spark90@yahoo.com"},
                {"Name", "Ben"}
               }}, {
               "To",
               new JArray {
                new JObject {
                 {
                  "Email",
                  email
                 }, {
                  "Name",
                  "DotNetMastery"
                 }
                }
               }
              }, {
               "Subject",
               subject
              }, {
               "HTMLPart",
               body
              }
             }
             });
            await client.PostAsync(request);
        }
    }
}

// API key: 922338801F5627C217E4DBA1C3D51EB4D5D130C5DDE1AFDF270B717F090CF28ADBDFBB116A1818514D169F099EE8E0BD
// SMTP Password: 0DB8AD6646EF18CDCE9210D06C8F311956DA
