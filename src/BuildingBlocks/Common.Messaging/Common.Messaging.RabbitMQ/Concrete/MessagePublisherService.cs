using Common.Messaging.RabbitMQ.Abstract;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Common.Messaging.RabbitMQ.Consts;

namespace Common.Messaging.RabbitMQ.Concrete
{
    public class MessagePublisherService : IMessagePublisherService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _conn;
        private readonly IModel _channel;

        public MessagePublisherService(string factoryUri)
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new(factoryUri);

            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
        }

        public void ConsumeConnectedInfo()
        {
            string queue = MessagingConsts.GetConnectedInfoQueue();
            EventingBasicConsumer consumer = new(_channel);

            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

            consumer.Received += (sender, e) => Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
        }

        public void PublishBackUpInfo()
        {
            string exchange = MessagingConsts.SendBackUpInfoExchange();
            string message = MessagingConsts.SendBackUpInfo();
            byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);
            _channel.BasicPublish(exchange: exchange, routingKey: string.Empty, body: body);
        }

        public void PublishStartTest()
        {
            string queueName = MessagingConsts.StartTestQueue();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true);

            for (int count = 1; count <= 100; count++)
            {
                string message = MessagingConsts.StartTest(count);
                byte[] body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: body);
            }
        }
    }
}
