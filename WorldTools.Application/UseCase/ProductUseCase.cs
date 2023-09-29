using Newtonsoft.Json;
using WorldTools.Application.Gateway;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Commands.BranchCommands;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Application.UseCase
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _repository;
        private readonly IStoredEventRepository _storedEvent;

        public ProductUseCase(IProductRepository repository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _storedEvent = storedEvent;
        }

        public async Task<int> RegisterProduct(RegisterProductCommand product)
        {
            var productName = new ProductValueObjectName(product.ProductName);
            var productDescription = new ProductValueObjectDescription(product.ProductDescription);
            var productPrice = new ProductValueObjectPrice(product.ProductPrice);
            var productStock = new ProductValueObjectInventoryStock(product.ProductInventoryStock);
            var productEntity = new ProductEntity(productName, productDescription, productPrice, productStock, product.ProductCategory, product.BranchId);

            var productId = await _repository.RegisterProductAsync(productEntity);
            await RegisterAndPersistEvent("ProductRegistered", productId, product);

            return productId;
        }

        public Task<string> RegisterProductFinalCustomerSale(RegisterSaleProductCommand product)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterProductInventoryStock(RegisterProductInventoryCommand product)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterResellerSale(RegisterSaleProductCommand product)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAndPersistEvent(string eventName, int aggregateId, RegisterProductCommand eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}
