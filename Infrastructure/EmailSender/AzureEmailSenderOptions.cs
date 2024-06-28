namespace SocialEmpires.Infrastructure.EmailSender
{
    public class AzureEmailSenderOptions
    {
        public string ConnectionString { get; init; } = null!;
        public string SenderAddress { get; init; } = null!;

        public AzureEmailSenderOptions()
        {
            // for configure options
        }
    }
}
