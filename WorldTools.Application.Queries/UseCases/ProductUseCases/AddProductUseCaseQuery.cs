using Newtonsoft.Json;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Application.Queries.UseCases.ProductUseCases
{
    public class AddProductUseCaseQuery : IProductUseCaseQuery
    {
        private readonly IProductRepository _repository;

        public AddProductUseCaseQuery(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponseVm> RegisterProduct(string product)
        {
            ProductEntity productToCreate = JsonConvert.DeserializeObject<ProductEntity>(product);
            var productName = new ProductValueObjectName(productToCreate.ProductName.ProductName);
            var productDescription = new ProductValueObjectDescription(productToCreate.ProductDescription.ProductDescription);
            var productPrice = new ProductValueObjectPrice(productToCreate.ProductPrice.ProductPrice);
            var productStock = new ProductValueObjectInventoryStock(productToCreate.ProductInventoryStock.ProductInventoryStock);
            var productCategory = new ProductValueObjectCategory(productToCreate.ProductCategory.ProductCategory);
            var productEntity = new ProductEntity(productName, productDescription, productPrice, productStock, productCategory, productToCreate.BranchId);

            await _repository.RegisterProductAsync(productEntity);

            var responseVm = new ProductResponseVm();
            responseVm.ProductName = productEntity.ProductName.ProductName;
            responseVm.ProductId = productEntity.ProductId;
            responseVm.ProductCategory = productEntity.ProductCategory.ProductCategory;
            responseVm.ProductPrice = productEntity.ProductPrice.ProductPrice;
            responseVm.ProductDescription = productEntity.ProductDescription.ProductDescription;
            responseVm.ProductInventoryStock = productEntity.ProductInventoryStock.ProductInventoryStock;
            responseVm.BranchId = productEntity.BranchId;

            return responseVm;
        }

    }
}
