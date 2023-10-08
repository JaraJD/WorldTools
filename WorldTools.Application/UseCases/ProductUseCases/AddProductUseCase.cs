using Newtonsoft.Json;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class AddProductUseCase
    {
        private readonly IPublishEventRepository _publishEventRepository;
        private readonly IStoredEventRepository _storedEvent;

        public AddProductUseCase(IPublishEventRepository publishEventRepository, IStoredEventRepository storedEvent)
        {
            _publishEventRepository = publishEventRepository;
            _storedEvent = storedEvent;
        }

        public async Task<RegisterProductCommand> RegisterProduct(RegisterProductCommand product)
        {
            var productName = new ProductValueObjectName(product.ProductName);
            var productDescription = new ProductValueObjectDescription(product.ProductDescription);
            var productPrice = new ProductValueObjectPrice(product.ProductPrice);
            var productStock = new ProductValueObjectInventoryStock(product.ProductInventoryStock);
            var productCategory = new ProductValueObjectCategory(product.ProductCategory);
            var productEntity = new ProductEntity(productName, productDescription, productPrice, productStock, productCategory, product.BranchId);

            var eventResponse = await RegisterAndPersistEvent("ProductRegistered", productEntity.BranchId, product);

            _publishEventRepository.PublishAddProduct(eventResponse);
            return product;
        }

        public async Task<StoredEvent> RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));
            await _storedEvent.RegisterEvent(storedEvent);
            return storedEvent;
        }
    }
}
