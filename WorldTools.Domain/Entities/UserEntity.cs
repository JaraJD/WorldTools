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
        public Guid UserId { get; set; }

        [Required] public UserValueObjectName Name { get; set; }

        [Required] public UserValueObjectPassword UserPassword { get; set; }

        [Required] public UserValueObjectEmail Email { get; set; }

        [Required] public UserValueObjectRole Role { get; set; }

        [Required] public Guid BranchId { get; set; }

        public virtual BranchEntity BranchEntity { get; set; }

        public UserEntity(UserValueObjectName name, UserValueObjectPassword password, UserValueObjectEmail email, UserValueObjectRole role, Guid branchId)
        {
            Name = name;
            UserPassword = password;
            Email = email;
            Role = role;
            BranchId = branchId;
        }
    }
}
