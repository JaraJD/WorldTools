using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Domain.Commands.UserCommands
{
    public class RegisterUserCommand
    {

        [Required] public UserValueObjectName Name { get; set; }

        [Required] public string UserPassword { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; }

        [Required] public string Role { get; set; }

        [Required] public Guid BranchId { get; set; }

    }
}
