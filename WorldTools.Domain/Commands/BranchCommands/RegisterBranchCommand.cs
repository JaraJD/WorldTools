using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Domain.Commands.BranchCommands
{
    public class RegisterBranchCommand
    {
        [Required] public string BranchName { get; set; }

        [Required] public BranchValueObjectLocation BranchLocation { get; set; }

    }
}
