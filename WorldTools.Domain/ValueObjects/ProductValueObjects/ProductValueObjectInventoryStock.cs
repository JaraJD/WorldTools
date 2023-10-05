using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectInventoryStock
    {
        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock del producto debe ser un número positivo.")]
        public int ProductInventoryStock { get; set; }

        public ProductValueObjectInventoryStock(int stock)
        {
            ProductInventoryStock = stock;
        }
    }
}
