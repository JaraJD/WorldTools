using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Domain.Commands.UserCommands
{
    public class RegisterUserCommand
    {

        [Required] public string Name { get; set; }

        [Required] public string UserPassword { get; set; }

        [Required] public string Email { get; set; }

        [Required] public UserValueObjectRole.roles Role { get; set; }

        [Required] public int BranchId { get; set; }

    }
}
