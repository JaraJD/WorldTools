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
            #region "Configuracion Vistas materializadas"
            _channel.QueueDeclare("queue.branch.register.query", true, false, false);
            _channel.QueueDeclare("queue.product.add.query", true, false, false);
            _channel.QueueDeclare("queue.product.customerSale.query", true, false, false);
            _channel.QueueDeclare("queue.product.resellerSale.query", true, false, false);
            _channel.QueueDeclare("queue.product.inventoryStock.query", true, false, false);
            _channel.QueueDeclare("queue.user.add.query", true, false, false);
            _channel.QueueBind("queue.branch.register.query", "topic_exchange", "topic.routing.register.branch.query");
            _channel.QueueBind("queue.product.add.query", "topic_exchange", "topic.routing.product.add.query");
            _channel.QueueBind("queue.product.customerSale.query", "topic_exchange", "topic.routing.product.customerSale.query");
            _channel.QueueBind("queue.product.resellerSale.query", "topic_exchange", "topic.routing.product.resellerSale.query");
            _channel.QueueBind("queue.product.inventoryStock.query", "topic_exchange", "topic.routing.product.inventoryStock.query");
            _channel.QueueBind("queue.user.add.query", "topic_exchange", "topic.routing.user.add.query");
            #endregion
            #region "Configuracion WebSocket"
            _channel.QueueDeclare("queue.product.add.socket", true, false, false);
            _channel.QueueDeclare("queue.product.customerSale.socket", true, false, false);
            _channel.QueueDeclare("queue.product.resellerSale.socket", true, false, false);
            _channel.QueueDeclare("queue.product.inventoryStock.socket", true, false, false);
            _channel.QueueBind("queue.product.add.socket", "topic_exchange", "topic.routing.product.add.socket");
            _channel.QueueBind("queue.product.customerSale.socket", "topic_exchange", "topic.routing.product.customerSale.socket");
            _channel.QueueBind("queue.product.resellerSale.socket", "topic_exchange", "topic.routing.product.resellerSale.socket");
            _channel.QueueBind("queue.product.inventoryStock.socket", "topic_exchange", "topic.routing.product.inventoryStock.socket");
            #endregion
        }
        public void PublishRegisterBranchEvent(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.register.branch.query", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishAddProduct(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);

            //Publica en vistas materializadas
            _channel.BasicPublish("topic_exchange", "topic.routing.product.add.query", null, body);
            Console.WriteLine($"Enviado a vistas materializadas: '{eventToPublished.EventBody}'");

            //Publica en socket
            _channel.BasicPublish("topic_exchange", "topic.routing.product.add.socket", null, body);
            Console.WriteLine($"Enviado a vistas materializadas: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterProductSaleCustomer(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.product.customerSale.query", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");

            _channel.BasicPublish("topic_exchange", "topic.routing.product.customerSale.socket", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterProductSaleReseller(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.product.resellerSale.query", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");

            _channel.BasicPublish("topic_exchange", "topic.routing.product.resellerSale.socket", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterProductStock(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.product.inventoryStock.query", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");

            _channel.BasicPublish("topic_exchange", "topic.routing.product.inventoryStock.socket", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }

        public void PublishRegisterUser(StoredEvent eventToPublished)
        {
            var body = Encoding.UTF8.GetBytes(eventToPublished.EventBody);
            _channel.BasicPublish("topic_exchange", "topic.routing.user.add.query", null, body);
            Console.WriteLine($"Enviado: '{eventToPublished.EventBody}'");
        }
    }
}
