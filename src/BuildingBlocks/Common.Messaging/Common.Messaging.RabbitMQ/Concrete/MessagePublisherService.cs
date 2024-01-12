using Common.Messaging.RabbitMQ.Abstract;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Common.Messaging.RabbitMQ.Consts;

namespace Common.Messaging.RabbitMQ.Concrete
{
    public class MessagePublisherService : IMessagePublisherService
    {

        public void ConsumeConnectedInfo(string factoryUri)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new(factoryUri);

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string queue = MessagingConsts.GetConnectedInfoQueue();
            EventingBasicConsumer consumer = new(channel);

            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
            channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

            consumer.Received += (sender, e) => Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
        }

        public void PublishBackUpInfo(string factoryUri)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new(factoryUri);

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string exchange = MessagingConsts.SendBackUpInfoExchange();
            string message = MessagingConsts.SendBackUpInfo();
            byte[] body = Encoding.UTF8.GetBytes(message);

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);
            channel.BasicPublish(exchange: exchange, routingKey: string.Empty, body: body);
        }

        public void PublishStartTest(string factoryUri)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new(factoryUri);

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string queueName = MessagingConsts.StartTestQueue();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true);

            for (int count = 1; count <= 100; count++)
            {
                string message = MessagingConsts.StartTest(count);
                byte[] body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: body);
            }
        }
    }
}
