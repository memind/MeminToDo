namespace Common.Messaging.MassTransit.Services.ConsoleServices.Abstract
{
    public interface IConsumerService
    {
        public Task PublishConnectedInfo(string serviceName);
        public Task ConsumeBackUpInfo();
        public Task ConsumeStartTest();
    }
}
