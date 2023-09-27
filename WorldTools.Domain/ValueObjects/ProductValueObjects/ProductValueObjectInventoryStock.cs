using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectInventoryStock
    {
        [Required] public int ProductInventoryStock { get; set; }

        public ProductValueObjectInventoryStock(int stock)
        {
            ProductInventoryStock = stock;
        }
    }
}
