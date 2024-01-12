namespace Common.Messaging.RabbitMQ.Abstract
{
    public interface IMessageConsumerService
    {
        public void PublishConnectedInfo(string serviceName, string factoryUri);
        public void ConsumeBackUpInfo(string factoryUri);
        public void ConsumeStartTest(string factoryUri);
    }
}
