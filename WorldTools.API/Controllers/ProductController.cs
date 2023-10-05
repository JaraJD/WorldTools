using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.UseCases.ProductUseCases;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.API.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AddProductUseCase _AddProductUseCase;
        private readonly RegisterProductFinalCustomerSaleUseCase _registerProductSaleUseCase;
        private readonly RegisterProductInventoryStockUseCase _registerProductStockUseCase;
        private readonly RegisterResellerSaleUseCase _registerProductResellerSaleUseCase;

        public ProductController(
            AddProductUseCase addProductUseCase,
            RegisterProductFinalCustomerSaleUseCase registerProductSaleUseCase, 
            RegisterProductInventoryStockUseCase registerProductStockUseCase, 
            RegisterResellerSaleUseCase registerProductResellerSaleUseCase
            )
        {
            _AddProductUseCase = addProductUseCase;
            _registerProductSaleUseCase = registerProductSaleUseCase;
            _registerProductStockUseCase = registerProductStockUseCase;
            _registerProductResellerSaleUseCase = registerProductResellerSaleUseCase;
        }

        [HttpPost("register")]
        public async Task<RegisterProductCommand> RegisterProduct([FromBody] RegisterProductCommand command)
        {
            return await _AddProductUseCase.RegisterProduct(command);
        }

        [HttpPatch("customer-sale/{idProduct}")]
        public async Task<ProductResponseVm> RegisterProductFinalCustomerSale(Guid idProduct, [FromBody] ProductSaleCommand command)
        {
            return await _registerProductSaleUseCase.RegisterProductFinalCustomerSale(command, idProduct);
        }

        [HttpPost("purchase/{idProduct}")]
        public async Task<ProductResponseVm> RegisterProductInventoryStock([FromBody] RegisterProductInventoryCommand command, Guid idProduct)
        {
            return await _registerProductStockUseCase.RegisterProductInventoryStock(command, idProduct);
        }

        [HttpPatch("seller-sale/{idProduct}")]
        public async Task<ProductResponseVm> RegisterResellerSale(Guid idProduct, [FromBody] ProductSaleCommand command)
        {
            return await _registerProductResellerSaleUseCase.RegisterResellerSale(command, idProduct);
        }
    }
}
