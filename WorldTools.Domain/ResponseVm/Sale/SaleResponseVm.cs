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

        public int SaleNumber { get; set; }

        public int SaleQuantity { get; set; }

        public double SaleTotal { get; set; }

        public string SaleType { get; set; }

        public Guid BranchId { get; set; }
    }
}
