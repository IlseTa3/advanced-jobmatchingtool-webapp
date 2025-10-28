using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public class EmailService : IEmailService, IEmailSender
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _smtpHost = configuration["Smtp:Host"];
            _smtpPort = int.Parse(configuration["Smtp:Port"]);
            _smtpUser = configuration["Smtp:Username"];
            _smtpPass = configuration["Smtp:Password"];
            _senderEmail = configuration["Smtp:SenderEmail"];
            _senderName = configuration["Smtp:SenderName"];
            _logger = logger;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string message)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(_senderEmail, _senderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mail.To.Add(recipientEmail);

                using var smtp = new SmtpClient(_smtpHost, _smtpPort)
                {
                    Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mail);
                _logger.LogInformation("E-mail succesvol verzonden naar {RecipientEmail}", recipientEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij het verzenden van e-mail via SMTP.");
                throw;
            }
        }
    }

}
