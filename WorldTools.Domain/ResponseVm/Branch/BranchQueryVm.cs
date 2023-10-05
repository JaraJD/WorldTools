using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Domain.ResponseVm.Branch
{
    public class BranchQueryVm
    {
        public Guid BranchId { get; set; }

        [Required] public string BranchName { get; set; }

        [Required] public string Location { get; set; }

        public virtual List<ProductQueryVm> BranchProducts { get; set; }

        public virtual List<UserQueryVm> BranchEmployees { get; set; }
    }
}
