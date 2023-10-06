using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.SaleValueObjects
{
    public class SaleValueObjectType
    {
        [Required(ErrorMessage = "El tipo de venta es requerido.")]
        public string SaleType { get; set; }

        public SaleValueObjectType(string saleType)
        {
            if (string.IsNullOrWhiteSpace(saleType))
            {
                throw new ArgumentException("El tipo de venta no puede estar vacío o ser nulo.", nameof(saleType));
            }

            SaleType = saleType;
        }
    }
}
