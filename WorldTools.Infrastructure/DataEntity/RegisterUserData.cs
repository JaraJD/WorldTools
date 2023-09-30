using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.SqlAdapter.DataEntity
{
    public class RegisterUserData
    {
        public RegisterUserData(string name, string userPassword, string email, UserValueObjectRole.roles role, int branchId)
        {
            Name = name;
            UserPassword = userPassword;
            Email = email;
            Role = role;
            BranchId = branchId;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string UserPassword { get; set; }

        [Required] public string Email { get; set; }

        [Required] public UserValueObjectRole.roles Role { get; set; }

        [Required] public int BranchId { get; set; }

        [Required]
        [ForeignKey("BranchId")]
        public virtual RegisterBranchData Branch { get; set; }
    }
}
