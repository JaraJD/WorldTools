using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.SqlAdapter.DataEntity
{
    public class RegisterUserData
    {
        public RegisterUserData(string name, string userPassword, string email, string role, Guid branchId, string storeSalt)
        {
            Name = name;
            UserPassword = userPassword;
            Email = email;
            Role = role;
            BranchId = branchId;
            StoreSalt = storeSalt;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string UserPassword { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Role { get; set; }

        [Required] public Guid BranchId { get; set; }

        [Required] public string StoreSalt { get; set; }

        [ForeignKey("BranchId")]
        public RegisterBranchData Branch { get; set; }
    }
}
