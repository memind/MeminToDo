using Common.Messaging.MassTransit.Messages.Abstract;

namespace Common.Messaging.MassTransit.Messages.Concrete
{
    public class Message : IMessage
    {
        public string Result { get; set; }
    }
}
