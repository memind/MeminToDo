
using Common.Messaging.MassTransit.Consts;
using Common.Messaging.MassTransit.Messages.Concrete;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Common.Messaging.MassTransit.Services.ApiServices.Connected
{
    public class PublishConnectedMessageService : BackgroundService
    {
        readonly IPublishEndpoint _endpoint;
        private string _serviceName;
        public PublishConnectedMessageService(IPublishEndpoint endpoint, string serviceName)
        {
            _endpoint = endpoint;
            _serviceName = serviceName;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _endpoint.Publish(new Message() { Result = MessagingConsts.GetConnectedInfo(_serviceName) });
        }
    }
}
