﻿
using Azure.Communication.Email;
using Microsoft.Extensions.Options;

namespace SocialEmpires.Infrastructure.EmailSender
{
    public class AzureEmailSender : IEmailSender
    {
        private readonly AzureEmailSenderOptions _options;
        private readonly ILogger<AzureEmailSender> _logger;
        private readonly EmailClient _emailClient;

        public AzureEmailSender(
            IOptions<AzureEmailSenderOptions> options,
            ILogger<AzureEmailSender> logger)
        {
            _options = options.Value;
            _logger = logger;

            _emailClient = new EmailClient(_options.ConnectionString);
        }

        public void Send(string address, string subject, string content)
        {
            _emailClient.Send(
                Azure.WaitUntil.Completed,
                _options.SenderAddress,
                address,
                subject,
                content);
        }

        public async Task SendAsync(string address, string subject, string content, CancellationToken cancellationToken = default)
        {
            var result = await _emailClient.SendAsync(
                Azure.WaitUntil.Completed,
                _options.SenderAddress,
                address,
                subject,
                content,
                null,
                cancellationToken);
            _logger.LogInformation("[subject:{subject}][address:{address}][status:{status}]", subject, address, result.Value.Status);
        }
    }
}
