using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Logging;

public class EmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(string toName, string toEmail, string subject, string message)
    {
        try
        {
            _logger.LogInformation("Start verzending van e-mail naar {ToEmail} met onderwerp '{Subject}'", toEmail, subject);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_config["SMTP_SENDERNAME"], _config["SMTP_SENDEREMAIL"]));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = message };

            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            _logger.LogInformation("Verbinding maken met SMTP server {Host}:{Port}", _config["SMTP_HOST"], _config["SMTP_PORT"]);
            await smtp.ConnectAsync(_config["SMTP_HOST"], int.Parse(_config["SMTP_PORT"]), MailKit.Security.SecureSocketOptions.SslOnConnect);

            _logger.LogInformation("Authenticatie met SMTP gebruiker {User}", _config["SMTP_USERNAME"]);
            await smtp.AuthenticateAsync(_config["SMTP_USERNAME"], _config["SMTP_PASSWORD"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogDebug("Sender: {Name} <{Email}>", _config["SMTP_SENDERNAME"], _config["SMTP_SENDEREMAIL"]);

            _logger.LogInformation("E-mail succesvol verzonden naar {ToEmail}", toEmail);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fout bij verzenden van e-mail naar {ToEmail}", toEmail);
            return false;
        }
    }
}