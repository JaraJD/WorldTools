using System;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Commands.ProductCommands
{
    public class RegisterSaleProductCommand
    {
        [Required] public List<ProductSaleCommand> Products { get; set; }
    }
}
