namespace Common.Messaging.RabbitMQ.Abstract
{
    public interface IMessagePublisherService
    {
        public void ConsumeConnectedInfo();
        public void PublishBackUpInfo();
        public void PublishStartTest();
    }
}
