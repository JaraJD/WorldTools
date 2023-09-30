using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.ProductCommands;

namespace WorldTools.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductUseCase _productUseCase;

        public ProductController(IProductUseCase productUseCase)
        {
            _productUseCase = productUseCase;
        }

        [HttpPost("register")]
        public async Task<int> RegisterProduct([FromBody] RegisterProductCommand command)
        {
            return await _productUseCase.RegisterProduct(command);
        }

        [HttpPost("FinalCustomerSale")]
        public async Task<string> RegisterProductFinalCustomerSale([FromBody] RegisterSaleProductCommand command)
        {
            return await _productUseCase.RegisterProductFinalCustomerSale(command);
        }

        [HttpPost("InventoryStock")]
        public async Task<string> RegisterProductInventoryStock([FromBody] RegisterProductInventoryCommand command)
        {
            return await _productUseCase.RegisterProductInventoryStock(command);
        }

        [HttpPost("ResellerSale")]
        public async Task<string> RegisterResellerSale([FromBody] RegisterSaleProductCommand command)
        {
            return await _productUseCase.RegisterResellerSale(command);
        }
    }
}
