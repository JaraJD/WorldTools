using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Domain.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }

        public UserValueObjectName Name { get; set; }

        public UserValueObjectPassword UserPassword { get; set; }

        public UserValueObjectEmail Email { get; set; }

        public UserValueObjectRole Role { get; set; }

        [Required(ErrorMessage = "El BranchId es obligatorio.")]
        public Guid BranchId { get; set; }

        public virtual BranchEntity BranchEntity { get; set; }

        public UserEntity(UserValueObjectName name, UserValueObjectPassword password, UserValueObjectEmail email, UserValueObjectRole role, Guid branchId)
        {
            UserId = Guid.NewGuid();
            Name = name;
            UserPassword = password;
            Email = email;
            Role = role;
            BranchId = branchId;
        }
    }
}
