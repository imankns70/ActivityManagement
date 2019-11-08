using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ActivityManagement.Services.EfInterfaces;
using ActivityManagement.Services.EfInterfaces.Identity;
using ActivityManagement.ViewModels.SiteSettings;

namespace ActivityManagement.Services.EfServices.Identity
{
    public class EmailSender : IEmailSender
    {
        private readonly IWritableOptions<SiteSettings> _writableLocations;
        public EmailSender(IWritableOptions<SiteSettings> writableLocations)
        {
            _writableLocations = writableLocations;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _writableLocations.Value.SiteEmail.Username,
                    Password = _writableLocations.Value.SiteEmail.Password,
                };

                client.Credentials = credential;
                client.Host = _writableLocations.Value.SiteEmail.Host;
                client.Port = _writableLocations.Value.SiteEmail.Port;
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_writableLocations.Value.SiteEmail.Email);
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = message;

                    client.Send(emailMessage);
                };

                await Task.CompletedTask;
            }
        }
    }
}
