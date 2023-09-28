using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.DTO
{
    public class ProductSaleDTO
    {
        [Required] public int ProductId { get; set; }

        [Required] public double ProductPrice { get; set; }

        [Required] public int ProductStock { get; set; }
    }
}
