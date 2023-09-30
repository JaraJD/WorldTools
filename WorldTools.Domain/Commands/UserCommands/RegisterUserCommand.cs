using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Domain.Commands.UserCommands
{
    public class RegisterUserCommand
    {

        [Required] public UserValueObjectName Name { get; set; }

        [Required] public string UserPassword { get; set; }

        [Required] public string Email { get; set; }

        [Required] public UserValueObjectRole.roles Role { get; set; }

        [Required] public Guid BranchId { get; set; }

    }
}
