using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; }

        public ProductValueObjectName ProductName { get; set; }

        public ProductValueObjectDescription ProductDescription { get; set; }

        public ProductValueObjectPrice ProductPrice { get; set; }

        public ProductValueObjectInventoryStock ProductInventoryStock { get; set; }

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
            ProductId = Guid.NewGuid();
            ProductName = name;
            ProductDescription = description;
            ProductPrice = price;
            ProductInventoryStock = stock;
            ProductCategory = category;
            BranchId = branchId;
        }
    }
}
