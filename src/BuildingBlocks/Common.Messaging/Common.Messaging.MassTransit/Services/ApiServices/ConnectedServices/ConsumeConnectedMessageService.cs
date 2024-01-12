using Common.Messaging.MassTransit.Messages.Abstract;
using MassTransit;

namespace Common.Messaging.MassTransit.Services.ApiServices.Connected
{
    public class ConsumeConnectedMessageService : IConsumer<IMessage>
    {
        public async Task Consume(ConsumeContext<IMessage> context) => await Console.Out.WriteLineAsync(context.Message.Result);
    }
}
