using Newtonsoft.Json;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Sale;
using WorldTools.Domain.ValueObjects.SaleValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class RegisterProductFinalCustomerSaleUseCaseQuery : IProductCustomerSaleUseCaseQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleProductRepository _saleProductRepository;

        public RegisterProductFinalCustomerSaleUseCaseQuery(
            IProductRepository repository,
            ISaleProductRepository saleProductRepository
            )
        {
            _productRepository = repository;
            _saleProductRepository = saleProductRepository;
        }

        public async Task<SaleResponseVm> RegisterProductFinalCustomerSale(string product)
        {
            RegisterSaleProductCommand customerSaleToCreate = JsonConvert.DeserializeObject<RegisterSaleProductCommand>(product);
            double totalPrice = 0.0;
            foreach (var item in customerSaleToCreate.Products)
            {
                var productResponse = await _productRepository.RegisterProductFinalCustomerSaleAsync(item);

                if (productResponse.ProductInventoryStock < item.ProductQuantity)
                {
                    throw new Exception($"No hay suficiente stock para el producto: {productResponse.ProductName}");
                }

                var discount = productResponse.ProductPrice * 0.15;
                var price = (productResponse.ProductPrice - discount) * item.ProductQuantity;
                totalPrice += price;
            }

            var saleNumber = new SaleValueObjectNumber(customerSaleToCreate.Number);
            var saleQuantity = new SaleValueObjectQuantity(customerSaleToCreate.Products.Count);
            var saleTotal = new SaleValueObjectTotal(totalPrice);
            var saleType = new SaleValueObjectType("FinalCustomerSale");

            var saleEntity = new SaleEntity(saleNumber, saleQuantity, saleTotal, saleType, customerSaleToCreate.BranchId);
            var saleEntityResponse = await _saleProductRepository.RegisterSaleAsync(saleEntity);

            var saleResponse = new SaleResponseVm();
            saleResponse.BranchId = saleEntityResponse.BranchId;
            saleResponse.SaleNumber = saleEntityResponse.SaleValueNumber.Number;
            saleResponse.SaleTotal = saleEntityResponse.saleValueObjectTotal.TotalPrice;
            saleResponse.SaleQuantity = saleEntityResponse.SaleValueQuantity.Quantity;
            saleResponse.SaleType = saleEntityResponse.saleValueObjectType.SaleType;
            saleResponse.SaleId = saleEntityResponse.SaleId;

            return saleResponse;
        }
    }
}
