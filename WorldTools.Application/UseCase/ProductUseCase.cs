using Newtonsoft.Json;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Product;
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

        public async Task<RegisterProductCommand> RegisterProduct(RegisterProductCommand product)
        {
            var productName = new ProductValueObjectName(product.ProductName);
            var productDescription = new ProductValueObjectDescription(product.ProductDescription);
            var productPrice = new ProductValueObjectPrice(product.ProductPrice);
            var productStock = new ProductValueObjectInventoryStock(product.ProductInventoryStock);
            var productCategory = new ProductValueObjectCategory(product.ProductCategory);
            var productEntity = new ProductEntity(productName, productDescription, productPrice, productStock, productCategory, product.BranchId);

            var productResponse = await _repository.RegisterProductAsync(productEntity);
            await RegisterAndPersistEvent("ProductRegistered", productResponse.BranchId, product);

            return product;
        }

        public Task<ProductResponseVm> RegisterProductFinalCustomerSale(RegisterSaleProductCommand product, string idProduct)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseVm> RegisterProductInventoryStock(RegisterProductInventoryCommand product, string idProduct)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseVm> RegisterResellerSale(RegisterSaleProductCommand product, string idProduct)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAndPersistEvent(string eventName, Guid aggregateId, RegisterProductCommand eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}
