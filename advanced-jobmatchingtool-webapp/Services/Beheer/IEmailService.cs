namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message);

      
    }
}
