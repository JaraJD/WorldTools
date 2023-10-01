using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Domain.Commands.BranchCommands
{
    public class RegisterBranchCommand
    {
        [Required(ErrorMessage = "El nombre de la sucursal es obligatorio.")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "La ubicación de la sucursal es obligatoria.")]
        public BranchValueObjectLocation BranchLocation { get; set; }

    }
}
