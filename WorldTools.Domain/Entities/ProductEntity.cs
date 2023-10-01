using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre del producto debe tener entre 2 y 50 caracteres.")]
        public ProductValueObjectName ProductName { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatoria.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción del producto debe tener entre 10 y 500 caracteres.")]
        public ProductValueObjectDescription ProductDescription { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del producto debe ser mayor que cero.")]
        public ProductValueObjectPrice ProductPrice { get; set; }

        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock del producto debe ser un número positivo.")]
        public ProductValueObjectInventoryStock ProductInventoryStock { get; set; }

        [Required(ErrorMessage = "La categoría del producto es obligatoria.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$", ErrorMessage = "La categoría del producto debe cumplir con el patrón especificado.")]
        public ProductValueObjectCategory ProductCategory { get; set; }

        [Required(ErrorMessage = "El BranchId es obligatorio.")]
        public Guid BranchId { get; set; }

        public virtual BranchEntity BranchEntity { get; set; }

        public ProductEntity(
            ProductValueObjectName name,
            ProductValueObjectDescription description,
            ProductValueObjectPrice price,
            ProductValueObjectInventoryStock stock,
            ProductValueObjectCategory category,
            Guid branchId
        )
        {
            ProductName = name;
            ProductDescription = description;
            ProductPrice = price;
            ProductInventoryStock = stock;
            ProductCategory = category;
            BranchId = branchId;
        }
    }
}
