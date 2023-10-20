﻿using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WorldTools.Domain.Factory;

namespace WorldTools.Rabbit.SubscribeAdapter
{
    public class SubscribeEvent : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IBranchUseCaseQueryFactory _branchFactory;
        private readonly IProductUseCaseQueryFactory _productFactory;
        private readonly IUserUseCaseQueryFactory _userFactory;

        public SubscribeEvent(
            IBranchUseCaseQueryFactory factoryBranch,
            IProductUseCaseQueryFactory productFactory,
            IUserUseCaseQueryFactory userFactory
            )
        {
            _branchFactory = factoryBranch;
            _productFactory = productFactory;
            _userFactory = userFactory;

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
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var registerBranchUseCase = _branchFactory.Create();
            var consumerTopic1 = new EventingBasicConsumer(_channel);
            consumerTopic1.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await registerBranchUseCase.RegisterBranch(message);
                Console.WriteLine($"Recibido en Topic 1: '{message}'");
            };

            var registerProductUseCase = _productFactory.Create();
            var consumerTopic2 = new EventingBasicConsumer(_channel);
            consumerTopic2.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await registerProductUseCase.RegisterProduct(message);
                Console.WriteLine($"Recibido en Topic 2: '{message}'");
            };

            var registerCustomerSaleProductUseCase = _productFactory.RegisterCustomerSale();
            var consumerTopic3 = new EventingBasicConsumer(_channel);
            consumerTopic3.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await registerCustomerSaleProductUseCase.RegisterProductFinalCustomerSale(message);
                Console.WriteLine($"Recibido en Topic 3: '{message}'");
            };

            var registerResellerSaleProductUseCase = _productFactory.RegisterResellerSale();
            var consumerTopic4 = new EventingBasicConsumer(_channel);
            consumerTopic4.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await registerResellerSaleProductUseCase.RegisterResellerSale(message);
                Console.WriteLine($"Recibido en Topic 4: '{message}'");
            };

            var registerProductStockUseCase = _productFactory.RegisterProductStock();
            var consumerTopic5 = new EventingBasicConsumer(_channel);
            consumerTopic5.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await registerProductStockUseCase.RegisterProductInventoryStock(message);
                Console.WriteLine($"Recibido en Topic 5: '{message}'");
            };

            var registerUserUseCase = _userFactory.Create();
            var consumerTopic6 = new EventingBasicConsumer(_channel);
            consumerTopic6.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await registerUserUseCase.RegisterUser(message);
                Console.WriteLine($"Recibido en Topic 6: '{message}'");
            };

            _channel.BasicConsume(queue: "queue.branch.register.query",
                                     autoAck: true,
                                     consumer: consumerTopic1);

            _channel.BasicConsume(queue: "queue.product.add.query",
                                     autoAck: true,
                                     consumer: consumerTopic2);

            _channel.BasicConsume(queue: "queue.product.customerSale.query",
                                     autoAck: true,
                                     consumer: consumerTopic3);

            _channel.BasicConsume(queue: "queue.product.resellerSale.query",
                                     autoAck: true,
                                     consumer: consumerTopic4);

            _channel.BasicConsume(queue: "queue.product.inventoryStock.query",
                                     autoAck: true,
                                     consumer: consumerTopic5);

            _channel.BasicConsume(queue: "queue.user.add.query",
                                     autoAck: true,
                                     consumer: consumerTopic6);

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            await base.StopAsync(cancellationToken);
        }

    }
}
