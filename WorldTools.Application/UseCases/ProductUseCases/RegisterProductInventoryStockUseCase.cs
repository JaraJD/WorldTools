
using Newtonsoft.Json;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Events.Product;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class RegisterProductInventoryStockUseCase
    {
        private readonly IProductRepository _repository;
        private readonly IStoredEventRepository _storedEvent;

        public RegisterProductInventoryStockUseCase(IProductRepository repository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _storedEvent = storedEvent;
        }

        public async Task<ProductResponseVm> RegisterProductInventoryStock(RegisterProductInventoryCommand product, Guid idProduct)
        {
            var quatity = new ProductValueObjectInventoryStock(product.ProductQuantity);

            var productResponse = await _repository.RegisterProductInventoryStockAsync(quatity, idProduct);

            var eventStockResgitered = new RegisterStockEvent("ProductStockRegistered", product.ProductQuantity, productResponse.ProductInventoryStock, idProduct, productResponse.BranchId);

            await RegisterAndPersistEvent("ProductStockRegistered", productResponse.BranchId, eventStockResgitered);
            return productResponse;
        }

        public async Task RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}
