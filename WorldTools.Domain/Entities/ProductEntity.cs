﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; }

        [Required] public ProductValueObjectName ProductName { get; set; }

        [Required] public ProductValueObjectDescription ProductDescription { get; set; }

        [Required] public ProductValueObjectPrice ProductPrice { get; set; }

        [Required] public ProductValueObjectInventoryStock ProductInventoryStock { get; set; }

        [Required] public ProductValueObjectCategory ProductCategory { get; set; }

        [Required] public Guid BranchId { get; set; }

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
