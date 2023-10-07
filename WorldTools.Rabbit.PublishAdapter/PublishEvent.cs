using RabbitMQ.Client;
using System.Text;
using WorldTools.Domain.Entities;
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
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare("topic_exchange", ExchangeType.Topic, true);
            _channel.QueueDeclare("queue.branch.register", true, false, false);
            _channel.QueueDeclare("queue.product.add", true, false, false);
            _channel.QueueDeclare("queue.product.customerSale", true, false, false);
            _channel.QueueDeclare("queue.product.resellerSale", true, false, false);
            _channel.QueueDeclare("queue.product.inventoryStock", true, false, false);
            _channel.QueueDeclare("queue.user.register", true, false, false);
            _channel.QueueBind("queue.branch.register", "topic_exchange", "topic.routing.branch");
            _channel.QueueBind("queue.product.add", "topic_exchange", "topic.routing.product.add");
            _channel.QueueBind("queue.product.customerSale", "topic_exchange", "topic.routing.product.customerSale");
            _channel.QueueBind("queue.product.resellerSale", "topic_exchange", "topic.routing.product.resellerSale");
            _channel.QueueBind("queue.product.inventoryStock", "topic_exchange", "topic.routing.product.inventoryStock");
            _channel.QueueBind("queue.user.register", "topic_exchange", "topic.routing.user");
        }
        public void PublishRegisterBranchEvent(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.branch", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishAddProduct(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.product.add", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterProductSaleCustomer(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.product.customerSale", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterProductSaleReseller(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.product.resellerSale", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterProductStock(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.product.inventoryStock", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterUser(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.user", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }
    }
}
