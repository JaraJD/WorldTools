using AutoMapper;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ValueObjects.ProductValueObjects;
using WorldTools.Infrastructure;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.SqlAdapter.Adapters
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public ProductRepository(Context dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }


        public async Task<ProductEntity> RegisterProductAsync(ProductEntity product)
        {
            var productToRegister = new RegisterProductData(
                product.ProductName.ProductName,
                product.ProductDescription.ProductDescription,
                product.ProductPrice.ProductPrice,
                product.ProductInventoryStock.ProductInventoryStock,
                product.ProductCategory.ProductCategory,
                product.BranchId
                );

            _context.Add(productToRegister);
            await _context.SaveChangesAsync();

            product.ProductId = productToRegister.ProductId;
            return product;
        }

        public async Task<ProductResponseVm> RegisterProductFinalCustomerSaleAsync(ProductSaleCommand product)
        {
            var existingProduct = await _context.Product.FindAsync(product.ProductId);

            if (existingProduct == null)
            {
                throw new ArgumentNullException("El producto no se encontro.");
            }
            if (existingProduct.ProductInventoryStock < product.ProductQuantity)
            {
                throw new Exception($"No hay suficiente stock para el producto: {existingProduct.ProductName}");
            }
            existingProduct.ProductInventoryStock -= product.ProductQuantity;

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponseVm>(existingProduct);
        }

        public async Task<ProductResponseVm> RegisterProductInventoryStockAsync(ProductValueObjectInventoryStock product, Guid productId)
        {
            var existingProduct = await _context.Product.FindAsync(productId);

            if (existingProduct == null)
            {
                throw new ArgumentNullException("El producto no se encontro.");
            }

            existingProduct.ProductInventoryStock += product.ProductInventoryStock;

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponseVm>(existingProduct);
        }

        public async Task<ProductResponseVm> RegisterResellerSaleAsync(ProductSaleCommand product)
        {
            var existingProduct = await _context.Product.FindAsync(product.ProductId);

            if (existingProduct == null)
            {
                throw new ArgumentNullException("El producto no se encontro.");
            }
            if (existingProduct.ProductInventoryStock < product.ProductQuantity)
            {
                throw new Exception($"No hay suficiente stock para el producto: {existingProduct.ProductName}");
            }

            existingProduct.ProductInventoryStock -= product.ProductQuantity;

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponseVm>(existingProduct);
        }

        public async Task<ProductResponseVm> GetProductById(Guid productId)
        {
            var existingProduct = await _context.Product.FindAsync(productId);

            if (existingProduct == null)
            {
                throw new ArgumentNullException("El producto no se encontro.");
            }

            return _mapper.Map<ProductResponseVm>(existingProduct);
        }
    }
}
