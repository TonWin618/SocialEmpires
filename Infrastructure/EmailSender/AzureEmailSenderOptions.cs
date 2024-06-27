namespace SocialEmpires.Infrastructure.EmailSender
{
    public class AzureEmailSenderOptions
    {
        public string ConnectionString { get; init; }
        public string SenderAddress { get; init; }

        public AzureEmailSenderOptions()
        {
            // for configure options
            ArgumentNullException.ThrowIfNull(ConnectionString, nameof(ConnectionString));
            ArgumentNullException.ThrowIfNull(SenderAddress, nameof(SenderAddress));
        }
    }
}
