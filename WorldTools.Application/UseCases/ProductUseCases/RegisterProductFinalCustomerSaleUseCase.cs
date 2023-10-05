﻿

using Newtonsoft.Json;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Events.Product;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class RegisterProductFinalCustomerSaleUseCase
    {
        private readonly IProductRepository _repository;
        private readonly IStoredEventRepository _storedEvent;

        public RegisterProductFinalCustomerSaleUseCase(IProductRepository repository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _storedEvent = storedEvent;
        }

        public async Task<ProductResponseVm> RegisterProductFinalCustomerSale(ProductSaleCommand product, Guid idProduct)
        {
            var quatity = new ProductValueObjectInventoryStock(product.ProductQuantity);

            var productResponse = await _repository.RegisterProductFinalCustomerSaleAsync(quatity, idProduct);

            var eventSaleRegistered = new RegisterSaleEvent("ProductFinalCustomerSaleRegistered", product.ProductQuantity, idProduct, productResponse.BranchId);

            var discount = productResponse.ProductPrice * 0.15;

            eventSaleRegistered.TotalPrice = (productResponse.ProductPrice - discount) * product.ProductQuantity;

            await RegisterAndPersistEvent("ProductFinalCustomerSaleRegistered", productResponse.BranchId, eventSaleRegistered);
            return productResponse;
        }

        public async Task RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}