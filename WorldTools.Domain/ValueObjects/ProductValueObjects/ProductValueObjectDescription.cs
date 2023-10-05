using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectDescription
    {
        [Required(ErrorMessage = "La descripción del producto es obligatoria.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción del producto debe tener entre 10 y 500 caracteres.")]
        public string ProductDescription { get; set; }

        public ProductValueObjectDescription(string description)
        {
            ProductDescription = description;
        }
    }
}
