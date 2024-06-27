namespace SocialEmpires.Infrastructure.EmailSender
{
    public interface IEmailSender
    {
        public void Send(string address, string subject, string content);

        public Task SendAsync(string address, string subject, string content, CancellationToken cancellationToken = default);
    }
}
