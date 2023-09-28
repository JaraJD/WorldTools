using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.ProductCommands;

namespace WorldTools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductUseCase _productUseCase;

        public ProductController(IProductUseCase productUseCase)
        {
            _productUseCase = productUseCase;
        }

        [HttpPost]
        public async Task<string> RegisterProduct([FromBody] RegisterProductCommand command)
        {
            return await _productUseCase.RegisterProduct(command);
        }
    }
}
