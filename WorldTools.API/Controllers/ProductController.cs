using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.API.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductUseCase _productUseCase;

        public ProductController(IProductUseCase productUseCase)
        {
            _productUseCase = productUseCase;
        }

        [HttpPost("register")]
        public async Task<RegisterProductCommand> RegisterProduct([FromBody] RegisterProductCommand command)
        {
            return await _productUseCase.RegisterProduct(command);
        }

        [HttpPatch("customer-sale/{idProduct}")]
        public async Task<ProductResponseVm> RegisterProductFinalCustomerSale(string idProduct, [FromBody] RegisterSaleProductCommand command)
        {
            return await _productUseCase.RegisterProductFinalCustomerSale(command, idProduct);
        }

        [HttpPost("purchase/{idProduct}")]
        public async Task<ProductResponseVm> RegisterProductInventoryStock([FromBody] RegisterProductInventoryCommand command, string idProduct)
        {
            return await _productUseCase.RegisterProductInventoryStock(command, idProduct);
        }

        [HttpPatch("seller-sale/{idProduct}")]
        public async Task<ProductResponseVm> RegisterResellerSale(string idProduct, [FromBody] RegisterSaleProductCommand command)
        {
            return await _productUseCase.RegisterResellerSale(command, idProduct);
        }
    }
}
