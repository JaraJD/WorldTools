using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectCategory
    {

        [Required(ErrorMessage = "La categoría del producto es obligatoria.")]
        public string? ProductCategory { get; private set; }
        public ProductValueObjectCategory(string productCategory)
        {
            Validate(productCategory);
        }

       
        private void Validate(string category)
        {
            if (category == null || category.Length < 5)
            {
                throw new ArgumentNullException("Categoria invalida");
            }
            
            ProductCategory = category;
        }

        public void SetValue(string category)
        {
            Validate(category);
        }

    }
}
