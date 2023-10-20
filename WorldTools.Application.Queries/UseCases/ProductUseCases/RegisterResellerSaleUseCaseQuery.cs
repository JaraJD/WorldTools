using Newtonsoft.Json;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Events.Product;
using WorldTools.Domain.Ports;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ResponseVm.Sale;
using WorldTools.Domain.ValueObjects.ProductValueObjects;
using WorldTools.Domain.ValueObjects.SaleValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class RegisterResellerSaleUseCaseQuery : IProductResellerSaleUseCaseQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleProductRepository _saleProductRepository;

        public RegisterResellerSaleUseCaseQuery(
            IProductRepository repository,
            ISaleProductRepository saleProductRepository)
        {
            _productRepository = repository;
            _saleProductRepository = saleProductRepository;
        }

        public async Task<SaleResponseVm> RegisterResellerSale(string product)
        {
            RegisterSaleProductCommand ResellerSaleToCreate = JsonConvert.DeserializeObject<RegisterSaleProductCommand>(product);
            double totalPrice = 0;
            foreach (var item in ResellerSaleToCreate.Products)
            {
                var productResponse = await _productRepository.RegisterResellerSaleAsync(item);

                if (productResponse.ProductInventoryStock < item.ProductQuantity)
                {
                    throw new Exception($"No hay suficiente stock para el producto: {productResponse.ProductName}");
                }

                var discount = productResponse.ProductPrice * 0.25;
                var price = (productResponse.ProductPrice - discount) * item.ProductQuantity;
                totalPrice += price;
            }

            var saleNumber = new SaleValueObjectNumber(ResellerSaleToCreate.Number);
            var saleQuantity = new SaleValueObjectQuantity(ResellerSaleToCreate.Products.Count);
            var saleTotal = new SaleValueObjectTotal(totalPrice);
            var saleType = new SaleValueObjectType("ResellerSale");

            var saleEntity = new SaleEntity(saleNumber, saleQuantity, saleTotal, saleType, ResellerSaleToCreate.BranchId);
            var saleEntityResponse = await _saleProductRepository.RegisterSaleAsync(saleEntity);

            var saleResponse = new SaleResponseVm();
            saleResponse.BranchId = saleEntity.BranchId;
            saleResponse.SaleNumber = saleEntity.SaleValueNumber.Number;
            saleResponse.SaleTotal = saleEntity.saleValueObjectTotal.TotalPrice;
            saleResponse.SaleQuantity = saleEntity.SaleValueQuantity.Quantity;
            saleResponse.SaleType = saleEntity.saleValueObjectType.SaleType;
            saleResponse.SaleId = saleEntityResponse.SaleId;

            return saleResponse;
        }

    }
}
