using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorldTools.SqlAdapter.DataEntity
{
    public class RegisterSaleData
    {
        public RegisterSaleData(int saleNumber, int saleQuantity, double saleTotal, string saleType, Guid branchId)
        {
            SaleNumber = saleNumber;
            SaleQuantity = saleQuantity;
            SaleTotal = saleTotal;
            SaleType = saleType;
            BranchId = branchId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required] public Guid SaleId { get; set; }

        [Required] public int SaleNumber { get; set; }

        [Required] public int SaleQuantity { get; set; }

        [Required] public double SaleTotal { get; set; }

        [Required] public string SaleType { get; set; }

        [Required] public Guid BranchId { get; set; }

        [ForeignKey("BranchId")]
        public RegisterBranchData Branch { get; set; }
    }
}
