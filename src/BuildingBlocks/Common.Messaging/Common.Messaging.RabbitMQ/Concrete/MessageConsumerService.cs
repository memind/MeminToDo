using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Consts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Common.Messaging.RabbitMQ.Concrete
{
    public class MessageConsumerService : IMessageConsumerService
    {
        public void PublishConnectedInfo(string serviceName, string factoryUri)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new(factoryUri);

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string message = MessagingConsts.GetConnectedInfo(serviceName);
            byte[] body = Encoding.UTF8.GetBytes(message);

            string queue = MessagingConsts.GetConnectedInfoQueue();

            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
            channel.BasicPublish(exchange: string.Empty, routingKey: queue, body: body);
        }

        public void ConsumeBackUpInfo(string factoryUri)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new(factoryUri);

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string exchange = MessagingConsts.SendBackUpInfoExchange();

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);

            string queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName, exchange: exchange, routingKey: string.Empty);

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            consumer.Received += (sender, e) => Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
        }

        public void ConsumeStartTest(string factoryUri)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new(factoryUri);

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();


            string queueName = MessagingConsts.StartTestQueue();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true);

            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            consumer.Received += (sender, e) => Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
           
        }
    }
}
