using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.DTO;

namespace WorldTools.Domain.Commands.ProductCommands
{
    public class RegisterSaleProductCommand
    {
        [Required] public List<ProductSaleDTO> Products { get; set; }
    }
}
