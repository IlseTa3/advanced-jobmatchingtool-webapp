namespace advanced_jobmatchingtool_webapp.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message);
    }
}
