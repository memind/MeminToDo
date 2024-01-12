using Common.Messaging.MassTransit.Messages.Abstract;
using MassTransit;

namespace Common.Messaging.MassTransit.Consumer
{
    internal class ConsoleMessageConsumer : IConsumer<IMessage>
    {
        async Task IConsumer<IMessage>.Consume(ConsumeContext<IMessage> context) => await Console.Out.WriteLineAsync(context.Message.ToString());
    }
}
