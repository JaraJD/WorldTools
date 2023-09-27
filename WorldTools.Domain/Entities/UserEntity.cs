using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Domain.Entities
{
    public class UserEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required] UserValueObjectName Name { get; set; }

        [Required] UserValueObjectPassword UserPassword { get; set; }

        [Required] UserValueObjectEmail Email { get; set; }

        [Required] UserValueObjectRole.roles Role { get; set; }

        [Required] public int BranchId { get; set; }

        [Required]
        [ForeignKey("BranchId")]
        public virtual BranchEntity BranchEntity { get; set; }

        public UserEntity(UserValueObjectName name, UserValueObjectPassword password, UserValueObjectEmail email, UserValueObjectRole.roles role, int branchId)
        {
            Name = name;
            UserPassword = password;
            Email = email;
            Role = role;
            BranchId = branchId;
        }
    }
}
