using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.Commands.ProductCommands
{
    public class RegisterProductInventoryCommand
    {
        [Required] public int ProductId { get; set;}

        [Required] public int ProductStock { get; set;}
    }
}
