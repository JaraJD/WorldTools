using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Domain.Commands.ProductCommands
{
    public class RegisterProductCommand
    {
        [Required] public string? ProductName { get; set; }
        [Required] public string? ProductDescription { get; set; }
        [Required] public double ProductPrice { get; set; }
        [Required] public int ProductInventoryStock { get; set; }   
        [Required] public string ProductCategory { get; set; }
        [Required] public Guid BranchId { get; set; }

    }
}
