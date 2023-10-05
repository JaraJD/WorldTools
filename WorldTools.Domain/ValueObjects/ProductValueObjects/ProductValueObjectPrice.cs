using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectPrice
    {
        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del producto debe ser mayor que cero.")]
        public double ProductPrice { get; set; }

        public ProductValueObjectPrice(double price)
        {
            ProductPrice = price;
        }
    }
}
