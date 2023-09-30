using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WorldTools.SqlAdapter.DataEntity
{
    public class RegisterBranchData
    {
        public RegisterBranchData(string branchName, string branchCountry, string branchCity)
        {
            BranchName = branchName;
            BranchCountry = branchCountry;
            BranchCity = branchCity;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchId { get; set; }

        [Required] public string BranchName { get; set; }

        [Required] public string BranchCountry { get; set; }

        [Required] public string BranchCity { get; set; }

        public virtual List<RegisterProductData> BranchProducts { get; set; }

        public virtual List<RegisterUserData> BranchEmployees { get; set; }


    }
}
