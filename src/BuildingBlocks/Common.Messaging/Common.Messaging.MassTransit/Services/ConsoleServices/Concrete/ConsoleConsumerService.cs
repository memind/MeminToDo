using Common.Messaging.MassTransit.Consts;
using Common.Messaging.MassTransit.Consumer;
using Common.Messaging.MassTransit.Messages.Abstract;
using Common.Messaging.MassTransit.Messages.Concrete;
using Common.Messaging.MassTransit.Services.ConsoleServices.Abstract;
using MassTransit;
using MassTransit.Testing;

namespace Common.Messaging.MassTransit.Services.ConsoleServices.Concrete
{
    public class ConsoleConsumerService : IConsumerService
    {
        private readonly string _rabbitMqHost;
        public ConsoleConsumerService(string rabbitMqHost) => _rabbitMqHost = rabbitMqHost;
        public async Task ConsumeBackUpInfo()
        {
            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f =>
            {
                f.Host(_rabbitMqHost);
                f.ReceiveEndpoint(MessagingConsts.BackUpQueue(), e => { e.Consumer<ConsoleMessageConsumer>(); });
            });

            await bus.StartAsync();
        }

        public async Task ConsumeStartTest()
        {
            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f =>
            {
                f.Host(_rabbitMqHost);
                f.ReceiveEndpoint(MessagingConsts.StartTestQueue(), e => { e.Consumer<ConsoleMessageConsumer>(); });
            });

            await bus.StartAsync();
        }

        public async Task PublishConnectedInfo(string serviceName)
        {
            string endpointUri = $"{_rabbitMqHost}/{MessagingConsts.GetConnectedInfoQueue()}";

            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f => { f.Host(_rabbitMqHost); });
            ISendEndpoint endpoint = await bus.GetSendEndpoint(new(endpointUri));

            await endpoint.Send<IMessage>(new Message() { Result = MessagingConsts.GetConnectedInfo(serviceName) });
        }

    }
}
