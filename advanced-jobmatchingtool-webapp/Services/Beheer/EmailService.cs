using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

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
            email.From.Add(new MailboxAddress(
                _config["EmailSettings:From"],
                _config["EmailSettings:From"]
            ));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = message };

            using var smtp = new SmtpClient();
            

            var host = _config["EmailSettings:SmtpServer"];
            var port = _config.GetValue<int>("EmailSettings:Port");
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];
            var useSSL = _config.GetValue<bool>("EmailSettings:UseSSL");

            _logger.LogInformation("Verbinding maken met SMTP server {Host}:{Port}", host, port);
            await smtp.ConnectAsync(host, port,
                useSSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);

            _logger.LogInformation("Authenticatie met SMTP gebruiker {User}", username);
            await smtp.AuthenticateAsync(username, password);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

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