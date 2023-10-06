using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ValueObjects.SaleValueObjects;

namespace WorldTools.Domain.Entities
{
    public class SaleEntity
    {
        public SaleEntity(
            SaleValueObjectNumber saleValueNumber,
            SaleValueObjectQuantity saleValueQuantity,
            SaleValueObjectTotal saleValueObjectTotal,
            SaleValueObjectType saleValueObjectType,
            Guid branchId)
        {
            SaleId = Guid.NewGuid();
            SaleValueNumber = saleValueNumber;
            SaleValueQuantity = saleValueQuantity;
            this.saleValueObjectTotal = saleValueObjectTotal;
            this.saleValueObjectType = saleValueObjectType;
            BranchId = branchId;
        }

        public Guid SaleId { get; set; }

        public SaleValueObjectNumber SaleValueNumber { get; set; }

        public SaleValueObjectQuantity SaleValueQuantity { get; set; }

        public SaleValueObjectTotal saleValueObjectTotal { get; set; }

        public SaleValueObjectType saleValueObjectType { get; set; }

        [Required(ErrorMessage = "El BranchId es obligatorio.")]
        public Guid BranchId { get; set; }
    }
}
