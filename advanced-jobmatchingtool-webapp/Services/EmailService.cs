
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class EmailService : IEmailService, IEmailSender
    {
        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _apiKey = configuration["SendGrid:ApiKey"];
            _senderEmail = configuration["SendGrid:SenderEmail"];
            _senderName = configuration["SendGrid:SenderName"];
            _logger = logger;
            _logger.LogInformation("EmailService geconfigureerd. Sender:{SenderEmail}, API Key Length:{ApiKeyLength}, API KEY: {ApiKey}",
                _senderEmail, _apiKey?.Length ?? 0, _apiKey);
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string message)
        {
            try
            {
                var client = new SendGridClient(_apiKey);
                var from = new EmailAddress(_senderEmail, _senderName);
                var to = new EmailAddress(recipientEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

                var response = await client.SendEmailAsync(msg);

                if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                {
                    _logger.LogInformation("E-mail succesvol verzonden naar {RecipientEmail}", recipientEmail);
                }
                else
                {
                    var errorMessage = $"Fout bij het verzenden van e-mail: {response.StatusCode}";
                    _logger.LogError(errorMessage);
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Er trad een fout op tijdens het verzenden van een e-mail.");
                throw;
            }
        }
    }
}
