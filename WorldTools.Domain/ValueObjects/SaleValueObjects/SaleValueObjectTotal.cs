using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.SaleValueObjects
{
    public class SaleValueObjectTotal
    {
        [Required(ErrorMessage = "El valor total es requerido.")]
        [Range(0.01, 1000000, ErrorMessage = "El valor total debe estar entre 0.01 y 1,000,000.")]
        public double TotalPrice { get; set; }

        public SaleValueObjectTotal(double totalPrice)
        {
            if (totalPrice < 0.01 || totalPrice > 1000000)
            {
                throw new ArgumentOutOfRangeException(nameof(totalPrice), "El valor total debe estar entre 0.01 y 1,000,000.");
            }

            TotalPrice = totalPrice;
        }
    }
}
