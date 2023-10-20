using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Application.Queries.UseCases.ProductUseCases;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.API.Queries.Controllers
{
    [Route("api/v1/product/")]
    [ApiController]
    [Authorize]
    public class ProductQueriesController : ControllerBase
    {
        private readonly GetProductByIdUseCaseQuery _getProductByIdUseCase;
        private readonly GetAllProductsUseCaseQuery _getAllProductsUseCase;
        private readonly GetProductsByBranchIdUseCaseQuery _getProductsByBranchIdUseCase;

        public ProductQueriesController(
            GetProductByIdUseCaseQuery getProductByIdUseCase,
            GetAllProductsUseCaseQuery getAllProductsUseCase,
            GetProductsByBranchIdUseCaseQuery getProductsByBranchIdUseCase
            )
        {
            _getProductByIdUseCase = getProductByIdUseCase;
            _getAllProductsUseCase = getAllProductsUseCase;
            _getProductsByBranchIdUseCase = getProductsByBranchIdUseCase;
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<ProductResponseVm> GetProductById(Guid id)
        {
            return await _getProductByIdUseCase.GetProductById(id);
        }

        [HttpGet("GetProducts/{id}")]
        public async Task<List<ProductResponseVm>> GetProducstByBranchId(Guid id)
        {
            return await _getProductsByBranchIdUseCase.GetAllProductsByBranchId(id);
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<ProductResponseVm>> GetAllProducts()
        {
            return await _getAllProductsUseCase.GetAllProducts();
        }
    }
}
