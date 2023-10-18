﻿using Newtonsoft.Json;
using WorldTools.Domain.Events.Product;
using WorldTools.Domain.Ports;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class RegisterProductInventoryStockUseCaseQuery : IProductRegisterStockUseCaseQuery
    {
        private readonly IProductRepository _repository;
        private readonly IWebSocketPort _webSocket;

        public RegisterProductInventoryStockUseCaseQuery(IProductRepository repository, IWebSocketPort webSocket)
        {
            _repository = repository;
            _webSocket = webSocket;
        }

        public async Task<ProductResponseVm> RegisterProductInventoryStock(string product)
        {
            RegisterStockEvent productStock = JsonConvert.DeserializeObject<RegisterStockEvent>(product);
            var quatity = new ProductValueObjectInventoryStock(productStock.ProductQuantity);

            var productResponse = await _repository.RegisterProductInventoryStockAsync(quatity, productStock.ProductId);
            await _webSocket.UpdateStock(productResponse);
            return productResponse;
        }
    }
}
