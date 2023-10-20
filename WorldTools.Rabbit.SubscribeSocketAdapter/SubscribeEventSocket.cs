using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Events.Product;
using WorldTools.Domain.Factory;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ResponseVm.Sale;
using WorldTools.Domain.ValueObjects.SaleValueObjects;
using WorldTools.WebSocketAdapter.Service;

namespace WorldTools.Rabbit.SubscribeSocketAdapter
{
    public class SubscribeEventSocket : BackgroundService
    {
        private readonly IProductRepositoryFactory _productRepository;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private IHubContext<WebSocketService> _hubContext;

        public SubscribeEventSocket(
            IProductRepositoryFactory repository,
            IHubContext<WebSocketService> hubContext
            )
        {
            _productRepository = repository;
            _hubContext = hubContext;

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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var getProductById = _productRepository.GetProductByIdAsync();

            var consumerTopic2 = new EventingBasicConsumer(_channel);
            consumerTopic2.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ProductEntity productToCreate = JsonConvert.DeserializeObject<ProductEntity>(message);
                var responseVm = new ProductResponseVm();
                responseVm.ProductName = productToCreate.ProductName.ProductName;
                responseVm.ProductId = productToCreate.ProductId;
                responseVm.ProductCategory = productToCreate.ProductCategory.ProductCategory;
                responseVm.ProductPrice = productToCreate.ProductPrice.ProductPrice;
                responseVm.ProductDescription = productToCreate.ProductDescription.ProductDescription;
                responseVm.ProductInventoryStock = productToCreate.ProductInventoryStock.ProductInventoryStock;
                responseVm.BranchId = productToCreate.BranchId;
                await _hubContext.Clients.All.SendAsync("createdProduct", responseVm);
                Console.WriteLine($"Recibido en Topic 2: '{message}'");
            };

            var consumerTopic3 = new EventingBasicConsumer(_channel);
            consumerTopic3.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                RegisterSaleProductCommand customerSaleToCreate = JsonConvert.DeserializeObject<RegisterSaleProductCommand>(message);
                double totalPrice = 0.0;
                foreach (var item in customerSaleToCreate.Products)
                {
                    var productResponse = await getProductById.GetProductByIdAsync(item.ProductId);

                    var discount = productResponse.ProductPrice * 0.15;
                    var price = (productResponse.ProductPrice - discount) * item.ProductQuantity;
                    totalPrice += price;
                }

                var saleNumber = new SaleValueObjectNumber(customerSaleToCreate.Number);
                var saleQuantity = new SaleValueObjectQuantity(customerSaleToCreate.Products.Count);
                var saleTotal = new SaleValueObjectTotal(totalPrice);
                var saleType = new SaleValueObjectType("FinalCustomerSale");

                var saleEntity = new SaleEntity(saleNumber, saleQuantity, saleTotal, saleType, customerSaleToCreate.BranchId);

                var saleResponse = new SaleResponseVm();
                saleResponse.BranchId = saleEntity.BranchId;
                saleResponse.SaleNumber = saleEntity.SaleValueNumber.Number;
                saleResponse.SaleTotal = saleEntity.saleValueObjectTotal.TotalPrice;
                saleResponse.SaleQuantity = saleEntity.SaleValueQuantity.Quantity;
                saleResponse.SaleType = saleEntity.saleValueObjectType.SaleType;
                saleResponse.SaleId = saleEntity.SaleId;

                await _hubContext.Clients.All.SendAsync("soldProduct", customerSaleToCreate);
                await _hubContext.Clients.All.SendAsync("updatedSales", saleResponse);
                Console.WriteLine($"Recibido en Topic 3: '{message}'");
            };

            var consumerTopic4 = new EventingBasicConsumer(_channel);
            consumerTopic4.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                RegisterSaleProductCommand ResellerSaleToCreate = JsonConvert.DeserializeObject<RegisterSaleProductCommand>(message);
                double totalPrice = 0.0;
                foreach (var item in ResellerSaleToCreate.Products)
                {
                    var productResponse = await getProductById.GetProductByIdAsync(item.ProductId);

                    var discount = productResponse.ProductPrice * 0.25;
                    var price = (productResponse.ProductPrice - discount) * item.ProductQuantity;
                    totalPrice += price;
                }

                var saleNumber = new SaleValueObjectNumber(ResellerSaleToCreate.Number);
                var saleQuantity = new SaleValueObjectQuantity(ResellerSaleToCreate.Products.Count);
                var saleTotal = new SaleValueObjectTotal(totalPrice);
                var saleType = new SaleValueObjectType("ResellerSale");

                var saleEntity = new SaleEntity(saleNumber, saleQuantity, saleTotal, saleType, ResellerSaleToCreate.BranchId);

                var saleResponse = new SaleResponseVm();
                saleResponse.BranchId = saleEntity.BranchId;
                saleResponse.SaleNumber = saleEntity.SaleValueNumber.Number;
                saleResponse.SaleTotal = saleEntity.saleValueObjectTotal.TotalPrice;
                saleResponse.SaleQuantity = saleEntity.SaleValueQuantity.Quantity;
                saleResponse.SaleType = saleEntity.saleValueObjectType.SaleType;
                saleResponse.SaleId = saleEntity.SaleId;

                await _hubContext.Clients.All.SendAsync("soldProduct", ResellerSaleToCreate);
                await _hubContext.Clients.All.SendAsync("updatedSales", saleResponse);
                Console.WriteLine($"Recibido en Topic 4: '{message}'");
            };

            var consumerTopic5 = new EventingBasicConsumer(_channel);
            consumerTopic5.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                RegisterStockEvent productStock = JsonConvert.DeserializeObject<RegisterStockEvent>(message);
                var productResponse = await getProductById.GetProductByIdAsync(productStock.ProductId);
                productResponse.ProductInventoryStock += productStock.ProductQuantity;
                await _hubContext.Clients.All.SendAsync("stockUpdate", productResponse);
                Console.WriteLine($"Recibido en Topic 5: '{message}'");
            };


            _channel.BasicConsume(queue: "queue.product.add.socket",
                                     autoAck: true,
                                     consumer: consumerTopic2);

            _channel.BasicConsume(queue: "queue.product.customerSale.socket",
                                     autoAck: true,
                                     consumer: consumerTopic3);

            _channel.BasicConsume(queue: "queue.product.resellerSale.socket",
                                     autoAck: true,
                                     consumer: consumerTopic4);

            _channel.BasicConsume(queue: "queue.product.inventoryStock.socket",
                                     autoAck: true,
                                     consumer: consumerTopic5);


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
