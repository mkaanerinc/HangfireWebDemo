
using MimeKit.Text;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace HangfireWebDemo.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Sender(string userId, string message)
        {
            // Find email address that has this userId. In this project we simplified.

            // Fake email account created from https://ethereal.email
            // Email Object

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailConfiguration").GetSection("UserName").Value));
            email.To.Add(MailboxAddress.Parse(_configuration.GetSection("EmailConfiguration").GetSection("UserName").Value));
            email.Subject = "Site bilgilendirme";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<strong>{message}</strong>"
            };

            // Sending Email

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration.GetSection("EmailConfiguration").GetSection("SmtpServer").Value,
                Convert.ToInt32(_configuration.GetSection("EmailConfiguration").GetSection("SmtpPort").Value),
                SecureSocketOptions.Auto);
            // for gmail => "smtp.gmail.com"
            await smtp.AuthenticateAsync(_configuration.GetSection("EmailConfiguration").GetSection("UserName").Value,
                _configuration.GetSection("EmailConfiguration").GetSection("Password").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
