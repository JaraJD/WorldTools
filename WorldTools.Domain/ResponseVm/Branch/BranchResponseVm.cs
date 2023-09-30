using System.ComponentModel.DataAnnotations;

namespace WorldTools.Domain.ResponseVm.Branch
{
    public class BranchResponseVm
    {
        public Guid BranchId { get; set; }

        [Required] public string BranchName { get; set; }

        [Required] public string BranchLocation { get; set; }
    }
}
