﻿
using Newtonsoft.Json;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Events.Product;
using WorldTools.Domain.Ports;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class RegisterProductInventoryStockUseCase
    {
        private readonly IProductRepository _repository;
        private readonly IPublishEventRepository _publishEventRepository;
        private readonly IStoredEventRepository _storedEvent;

        public RegisterProductInventoryStockUseCase(IProductRepository repository, IPublishEventRepository publishEventRepository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _publishEventRepository = publishEventRepository;
            _storedEvent = storedEvent;
        }

        public async Task<ProductResponseVm> RegisterProductInventoryStock(RegisterProductInventoryCommand product)
        {
            var quatity = new ProductValueObjectInventoryStock(product.ProductQuantity);

            var productResponse = await _repository.GetProductByIdAsync(product.ProductId);

            var eventStockResgitered = new RegisterStockEvent(product.ProductQuantity, product.ProductId);

            var eventResponse = await RegisterAndPersistEvent("ProductStockRegistered", productResponse.BranchId, eventStockResgitered);

            _publishEventRepository.PublishRegisterProductStock(eventResponse);
            return productResponse;
        }

        public async Task<StoredEvent> RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));
            await _storedEvent.RegisterEvent(storedEvent);
            return storedEvent;
        }
    }
}
