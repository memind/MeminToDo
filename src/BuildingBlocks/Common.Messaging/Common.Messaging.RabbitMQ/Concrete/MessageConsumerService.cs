using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Consts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Common.Messaging.RabbitMQ.Concrete
{
    public class MessageConsumerService : IMessageConsumerService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _conn;
        private readonly IModel _channel;

        public MessageConsumerService(string factoryUri)
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new(factoryUri);

            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
        }

        public void PublishConnectedInfo(string serviceName)
        {
            string message = MessagingConsts.GetConnectedInfo(serviceName);
            byte[] body = Encoding.UTF8.GetBytes(message);

            string queue = MessagingConsts.GetConnectedInfoQueue();

            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false);
            _channel.BasicPublish(exchange: string.Empty, routingKey: queue, body: body);
        }

        public void ConsumeBackUpInfo()
        {
            string exchange = MessagingConsts.SendBackUpInfoExchange();

            _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);

            string queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName, exchange: exchange, routingKey: string.Empty);

            EventingBasicConsumer consumer = new(_channel);
            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            consumer.Received += (sender, e) => Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
        }

        public void ConsumeStartTest()
        {
            string queueName = MessagingConsts.StartTestQueue();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true);

            EventingBasicConsumer consumer = new(_channel);

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            consumer.Received += (sender, e) => Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
           
        }
    }
}
