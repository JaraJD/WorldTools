using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.Commands.ProductCommands
{
    public class ProductSaleCommand
    {
        [Required] public int ProductQuantity { get; set; }
    }
}
