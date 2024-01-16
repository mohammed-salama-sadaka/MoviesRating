using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MoviesRating.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromMail = "aabdelhafeez55@outlook.com";
            var fromPass = "ZXCdsaqwe321!@#";
            var message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = htmlMessage;
            message.IsBodyHtml = true;
            var stmpClient=new SmtpClient("smtp-mail.outlook.com")
            {
                Port=587,
                Credentials=  new NetworkCredential(fromPass, fromPass),
                EnableSsl=true,
         };
            stmpClient.Send(message);
        }
    }
}
