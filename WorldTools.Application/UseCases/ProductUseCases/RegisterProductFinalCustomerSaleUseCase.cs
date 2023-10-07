﻿using Newtonsoft.Json;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Sale;
using WorldTools.Domain.ValueObjects.SaleValueObjects;

namespace WorldTools.Application.UseCases.ProductUseCases
{
    public class RegisterProductFinalCustomerSaleUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPublishEventRepository _publishEventRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IStoredEventRepository _storedEvent;

        public RegisterProductFinalCustomerSaleUseCase(IProductRepository repository, IPublishEventRepository publishEventRepository, IStoredEventRepository storedEvent, ISaleProductRepository saleProductRepository)
        {
            _productRepository = repository;
            _publishEventRepository = publishEventRepository;
            _storedEvent = storedEvent;
            _saleProductRepository = saleProductRepository;
        }

        public async Task<SaleResponseVm> RegisterProductFinalCustomerSale(RegisterSaleProductCommand product)
        {
            double totalPrice = 0;
            foreach (var item in product.Products)
            {
                var productResponse = await _productRepository.GetProductById(item.ProductId);

                if (productResponse.ProductInventoryStock < item.ProductQuantity)
                {
                    throw new Exception($"No hay suficiente stock para el producto: {productResponse.ProductName}");
                }

                var discount = productResponse.ProductPrice * 0.15;
                var price = (productResponse.ProductPrice - discount) * item.ProductQuantity;
                totalPrice += price;
            }

            var saleNumber = new SaleValueObjectNumber(product.Number);
            var saleQuantity = new SaleValueObjectQuantity(product.Products.Count);
            var saleTotal = new SaleValueObjectTotal(totalPrice);
            var saleType = new SaleValueObjectType("FinalCustomerSale");

            var saleEntity = new SaleEntity(saleNumber, saleQuantity, saleTotal, saleType, product.BranchId);

            var saleResponse = new SaleResponseVm();
            saleResponse.BranchId = saleEntity.BranchId;
            saleResponse.SaleValueNumber = saleEntity.SaleValueNumber.Number;
            saleResponse.saleValueObjectTotal = saleEntity.saleValueObjectTotal.TotalPrice;
            saleResponse.SaleValueQuantity = saleEntity.SaleValueQuantity.Quantity;
            saleResponse.saleValueObjectType = saleEntity.saleValueObjectType.SaleType;
            saleResponse.SaleId = saleEntity.SaleId;

            var eventResponse = await RegisterAndPersistEvent("ProductFinalCustomerSaleRegistered", product.BranchId, saleEntity);

            _publishEventRepository.PublishRegisterProductSaleCustomer(eventResponse);
            return saleResponse;
        }

        public async Task<StoredEvent> RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));
            await _storedEvent.RegisterEvent(storedEvent);
            return storedEvent;
        }
    }
}
