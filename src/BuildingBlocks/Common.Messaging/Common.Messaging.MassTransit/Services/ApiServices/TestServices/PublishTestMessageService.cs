using Common.Messaging.MassTransit.Consts;
using Common.Messaging.MassTransit.Messages.Concrete;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Common.Messaging.MassTransit.Services.ApiServices.Test
{
    public class PublishTestMessageService : BackgroundService
    {
        readonly IPublishEndpoint _endpoint;

        public PublishTestMessageService(IPublishEndpoint endpoint) => _endpoint = endpoint;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (int counter = 1; counter <= 100; counter++)
                await _endpoint.Publish(new Message() { Result = MessagingConsts.StartTest(counter) });

        }
    }
}
