using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.ResponseVm.Product
{
    public class ProductQueryVm
    {
        public Guid ProductId { get; set; }
        [Required] public string? ProductName { get; set; }
        [Required] public string? ProductDescription { get; set; }
        [Required] public double ProductPrice { get; set; }
        [Required] public int ProductInventoryStock { get; set; }
        [Required] public string ProductCategory { get; set; }
        [Required] public Guid BranchId { get; set; }


    }
}
