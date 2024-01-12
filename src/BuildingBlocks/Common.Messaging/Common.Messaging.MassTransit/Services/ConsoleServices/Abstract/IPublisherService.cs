namespace Common.Messaging.MassTransit.Services.ConsoleServices.Abstract
{
    public interface IPublisherService
    {
        public Task ConsumeConnectedInfo(string serviceName);
        public Task PublishBackUpInfo();
        public Task PublishStartTest();
    }
}
