using Newtonsoft.Json;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Events.Product;
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

        public async Task<ProductResponseVm> RegisterProductInventoryStock(RegisterProductInventoryCommand product, Guid idProduct)
        {
            var quatity = new ProductValueObjectInventoryStock(product.ProductQuantity);

            var productResponse = await _repository.RegisterProductInventoryStockAsync(quatity, idProduct);

            var eventStockResgitered = new RegisterStockEvent("ProductStockRegistered", product.ProductQuantity, productResponse.ProductInventoryStock, idProduct, productResponse.BranchId);

            await RegisterAndPersistEvent("ProductStockRegistered", productResponse.BranchId, eventStockResgitered);
            return productResponse;
        }

        public async Task<ProductResponseVm> RegisterResellerSale(ProductSaleCommand product, Guid idProduct)
        {
            var quatity = new ProductValueObjectInventoryStock(product.ProductQuantity);

            var productResponse = await _repository.RegisterResellerSaleAsync(quatity, idProduct);

            var eventSaleRegistered = new RegisterSaleEvent("ProductResellerSaleRegistered", product.ProductQuantity, idProduct, productResponse.BranchId);

            var discount = productResponse.ProductPrice * 0.20;

            eventSaleRegistered.TotalPrice = (productResponse.ProductPrice - discount) * product.ProductQuantity;

            await RegisterAndPersistEvent("ProductResellerSaleRegistered", productResponse.BranchId, eventSaleRegistered);
            return productResponse;
        }

        public async Task RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}
