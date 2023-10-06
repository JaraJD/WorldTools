using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.Commands.ProductCommands
{
    public class ProductSaleCommand
    {
        [Required] public Guid ProductId { get; set; }
        [Required] public int ProductQuantity { get; set; }
    }
}
