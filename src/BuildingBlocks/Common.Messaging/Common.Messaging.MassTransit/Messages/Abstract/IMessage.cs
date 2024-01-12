namespace Common.Messaging.MassTransit.Messages.Abstract
{
    public interface IMessage
    {
        public string Result { get; set; }
    }
}
