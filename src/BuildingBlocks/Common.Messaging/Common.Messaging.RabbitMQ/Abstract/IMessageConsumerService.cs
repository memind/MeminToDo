namespace Common.Messaging.RabbitMQ.Abstract
{
    public interface IMessageConsumerService
    {
        public void PublishConnectedInfo(string serviceName);
        public void ConsumeBackUpInfo();
        public void ConsumeStartTest();
    }
}
