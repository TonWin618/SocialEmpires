namespace SocialEmpires.Infrastructure.EmailSender
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string address, string subject, string content, CancellationToken cancellationToken = default);
    }
}
