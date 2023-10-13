﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Application.Queries.UseCases.ProductUseCases;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.API.Queries.Controllers
{
    [Route("api/v1/product/")]
    [ApiController]
    public class ProductQueriesController : ControllerBase
    {
        private readonly GetProductByIdUseCaseQuery _getProductByIdUseCase;
        private readonly GetAllProductsUseCaseQuery _getAllProductsUseCase;

        public ProductQueriesController(GetProductByIdUseCaseQuery getProductByIdUseCase, GetAllProductsUseCaseQuery getAllProductsUseCase)
        {
            _getProductByIdUseCase = getProductByIdUseCase;
            _getAllProductsUseCase = getAllProductsUseCase;
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<ProductResponseVm> GetProductById(Guid id)
        {
            return await _getProductByIdUseCase.GetProductById(id);
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<ProductResponseVm>> GetAllProducts()
        {
            return await _getAllProductsUseCase.GetAllProducts();
        }
    }
}