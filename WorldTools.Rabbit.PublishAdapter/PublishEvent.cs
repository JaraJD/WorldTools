using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Rabbit.PublishAdapter
{
    public class PublishEvent
    {
        public void publish(Object eventToPublished)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("topic_exchange", ExchangeType.Topic, true);
                channel.QueueDeclare("queue.topic1", true, false, false);
                channel.QueueDeclare("queue.topic2", true, false, false);
                channel.QueueBind("queue.topic1", "topic_exchange", "topic.routing.key1");
                channel.QueueBind("queue.topic2", "topic_exchange", "topic.routing.queue.key2");
                string eventString = eventToPublished.ToString();
                var message1 = "Mensaje para el exchange de tipo topic1";
                var message2 = "Mensaje para el exchange de tipo topic2";
                var message3 = "Mensaje para ambas colas que estan conectadas al exchange de tipo topic";
                var body1 = Encoding.UTF8.GetBytes(eventString);
                var body2 = Encoding.UTF8.GetBytes(message2);
                var body3 = Encoding.UTF8.GetBytes(message3);
                channel.BasicPublish("topic_exchange", "topic.routing.key1", null, body1);
                channel.BasicPublish("topic_exchange", "topic.routing.queue.key2", null, body2);
                channel.BasicPublish("topic_exchange", "topic.routing.*", null, body3);
                Console.WriteLine($"Enviado: '{message1}'");
                Console.WriteLine($"Enviado: '{message2}'");
                Console.WriteLine($"Enviado: '{message3}'");
            }
        }
        
    }
}
