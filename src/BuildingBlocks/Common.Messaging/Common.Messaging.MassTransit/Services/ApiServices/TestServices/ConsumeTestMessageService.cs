using Common.Messaging.MassTransit.Messages.Abstract;
using MassTransit;

namespace Common.Messaging.MassTransit.Services.ApiServices.Test
{
    public class ConsumeTestMessageService : IConsumer<IMessage>
    {
        async Task IConsumer<IMessage>.Consume(ConsumeContext<IMessage> context) => await Console.Out.WriteLineAsync(context.Message.Result);
        
    }
}
