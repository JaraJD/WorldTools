using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectName
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre del producto debe tener entre 2 y 50 caracteres.")]
        public string ProductName { get; set; }

        public ProductValueObjectName(string name)
        {
            ProductName = name;
        }
    }
}
