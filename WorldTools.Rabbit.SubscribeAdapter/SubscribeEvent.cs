using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WorldTools.Rabbit.SubscribeAdapter
{
    public class SubscribeEvent : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public SubscribeEvent()
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
            //string queueName = "mi_cola"; // Reemplaza con el nombre de tu cola
            //_channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.ExchangeDeclare("topic_exchange", ExchangeType.Topic, true);
            var queueNameTopic1 = _channel.QueueDeclare("queue.topic1", true, false, false).QueueName;
            var queueNameTopic2 = _channel.QueueDeclare("queue.topic2", true, false, false).QueueName;
            _channel.QueueBind(queueNameTopic1, "topic_exchange", "topic.routing.*");
            _channel.QueueBind(queueNameTopic2, "topic_exchange", "*.routing.key");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
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
            // Tipo topic

                var consumerTopic1 = new EventingBasicConsumer(_channel);
                consumerTopic1.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Recibido en Topic 1: '{message}'");
                };

                var consumerTopic2 = new EventingBasicConsumer(_channel);
                consumerTopic2.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Recibido en Topic 2: '{message}'");
                };

            _channel.BasicConsume(queue: "queue.topic1",
                                     autoAck: true,
                                     consumer: consumerTopic1);

            _channel.BasicConsume(queue: "queue.topic2",
                                     autoAck: true,
                                     consumer: consumerTopic2);
            //}


            //var consumer = new EventingBasicConsumer(_channel);

            //consumer.Received += (model, ea) =>
            //{
            //    var body = ea.Body.ToArray();
            //    var message = Encoding.UTF8.GetString(body);
            //    Console.WriteLine("Mensaje recibido: {0}", message);
            //};

            //_channel.BasicConsume(queue: "mi_cola", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

    }
}
