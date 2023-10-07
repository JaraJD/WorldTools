using RabbitMQ.Client;
using System.Text;
using WorldTools.Domain.Ports;

namespace WorldTools.Rabbit.PublishAdapter
{
    public class PublishEvent : IPublishEventRepository
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public PublishEvent()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            }; // Reemplaza con la dirección del servidor RabbitMQ
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            //string exchangeName = "mi_exchange"; // Reemplaza con el nombre del intercambio RabbitMQ
            //_channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
            _channel.ExchangeDeclare("topic_exchange", ExchangeType.Topic, true);
            _channel.QueueDeclare("queue.topic1", true, false, false);
            _channel.QueueDeclare("queue.topic2", true, false, false);
            _channel.QueueBind("queue.topic1", "topic_exchange", "topic.routing.key1");
            _channel.QueueBind("queue.topic2", "topic_exchange", "topic.routing.queue.key2");
        }
        public void Publish(Object eventToPublished)
        {
            //var factory = new ConnectionFactory()
            //{
            //    HostName = "localhost",
            //    Port = 5672,
            //    UserName = "guest",
            //    Password = "guest"
            //};

            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            
                string eventString = eventToPublished.ToString();
                var message1 = "Mensaje para el exchange de tipo topic1";
                var message2 = "Mensaje para el exchange de tipo topic2";
                var body1 = Encoding.UTF8.GetBytes(eventString);
                var body2 = Encoding.UTF8.GetBytes(message2);
            _channel.BasicPublish("topic_exchange", "topic.routing.key1", null, body1);
            _channel.BasicPublish("topic_exchange", "topic.routing.queue.key2", null, body2);
                Console.WriteLine($"Enviado: '{message1}'");
                Console.WriteLine($"Enviado: '{message2}'");
            //}
        }
        
    }
}
