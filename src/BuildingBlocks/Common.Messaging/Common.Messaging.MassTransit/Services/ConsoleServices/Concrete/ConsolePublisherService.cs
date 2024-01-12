using Common.Messaging.MassTransit.Consts;
using Common.Messaging.MassTransit.Consumer;
using Common.Messaging.MassTransit.Messages.Abstract;
using Common.Messaging.MassTransit.Messages.Concrete;
using Common.Messaging.MassTransit.Services.ConsoleServices.Abstract;
using MassTransit;

namespace Common.Messaging.MassTransit.Services.ConsoleServices.Concrete
{
    public class ConsolePublisherService : IPublisherService
    {
        private readonly string _rabbitMqHost;
        public ConsolePublisherService(string rabbitMqHost) => _rabbitMqHost = rabbitMqHost;

        public async Task ConsumeConnectedInfo(string serviceName)
        {
            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f =>
            {
                f.Host(_rabbitMqHost);
                f.ReceiveEndpoint(MessagingConsts.GetConnectedInfoQueue(), e => { e.Consumer<ConsoleMessageConsumer>(); });
            });

            await bus.StartAsync();
        }


        public async Task PublishBackUpInfo()
        {
            string endpointUri = $"{_rabbitMqHost}/{MessagingConsts.BackUpQueue()}";

            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f => { f.Host(_rabbitMqHost); });
            ISendEndpoint endpoint = await bus.GetSendEndpoint(new(endpointUri));

            await endpoint.Send<IMessage>(new Message() { Result = MessagingConsts.SendBackUpInfo() });
        }

        public async Task PublishStartTest()
        {
            string endpointUri = $"{_rabbitMqHost}/{MessagingConsts.StartTestQueue()}";

            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(f => { f.Host(_rabbitMqHost); });
            ISendEndpoint endpoint = await bus.GetSendEndpoint(new(endpointUri));

            for (int counter = 1; counter <= 100; counter++)
                await endpoint.Send<IMessage>(new Message() { Result = MessagingConsts.StartTest(counter) });

        }
    }
}
