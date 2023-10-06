using System;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Commands.ProductCommands
{
    public class RegisterSaleProductCommand
    {
        [Required] public int Number { get; set; }
        [Required] public Guid BranchId { get; set; }
        [Required] public List<ProductSaleCommand> Products { get; set; }
    }
}
