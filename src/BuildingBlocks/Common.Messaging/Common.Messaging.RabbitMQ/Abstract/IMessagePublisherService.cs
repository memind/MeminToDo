namespace Common.Messaging.RabbitMQ.Abstract
{
    public interface IMessagePublisherService
    {
        public void ConsumeConnectedInfo(string factoryUri);
        public void PublishBackUpInfo(string factoryUri);
        public void PublishStartTest(string factoryUri);
    }
}
