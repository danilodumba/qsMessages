using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using qs.Messages.Domains.Entities;
using qs.Messages.Domains.Services;

namespace qs.Messages.Mail
{
    public class MailService : IEmailService
    {
        readonly MailSettings _settings;
        public MailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task Send(Email email)
        {
            using var mailMessage = new MailMessage(email.From, email.To, email.Subject, email.Body);
            mailMessage.IsBodyHtml = true;

            using var client = new SmtpClient(_settings.Smtp, _settings.Port);
            client.EnableSsl = true;

            var credential = new NetworkCredential(_settings.User, _settings.Password);
            client.Credentials = credential;

            await client.SendMailAsync(mailMessage);
        }
    }
}