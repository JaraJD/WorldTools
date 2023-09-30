using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Domain.ResponseVm.User
{
    public class UserResponseVm
    {
        public Guid UserId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string UserPassword { get; set; }

        [Required] public string Email { get; set; }

        [Required] public UserValueObjectRole.roles Role { get; set; }

        [Required] public Guid BranchId { get; set; }
    }
}
