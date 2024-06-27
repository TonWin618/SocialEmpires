namespace SocialEmpires.Infrastructure.EmailSender
{
    public class AzureEmailSenderOptions
    {
        public string ConnectionString { get; init; }
        public string SenderAddress { get; init; }

        public AzureEmailSenderOptions() 
        {
            // for ConfigureOptions
        }
    }
}
