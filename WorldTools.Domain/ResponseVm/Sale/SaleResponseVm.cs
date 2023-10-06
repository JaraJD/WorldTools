using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ValueObjects.SaleValueObjects;

namespace WorldTools.Domain.ResponseVm.Sale
{
    public class SaleResponseVm
    {
        public Guid SaleId { get; set; }

        public int SaleValueNumber { get; set; }

        public int SaleValueQuantity { get; set; }

        public double saleValueObjectTotal { get; set; }

        public string saleValueObjectType { get; set; }

        public Guid BranchId { get; set; }
    }
}
