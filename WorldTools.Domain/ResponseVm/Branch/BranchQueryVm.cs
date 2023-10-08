using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ResponseVm.Sale;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Domain.ResponseVm.Branch
{
    public class BranchQueryVm
    {
        public Guid BranchId { get; set; }

        [Required] public string BranchName { get; set; }

        [Required] public string BranchCountry { get; set; }
        [Required] public string BranchCity { get; set; }

        public virtual List<ProductResponseVm> BranchProducts { get; set; }

        public virtual List<UserQueryVm> BranchEmployees { get; set; }

        public virtual List<SaleResponseVm> BranchSales { get; set; }
    }
}
