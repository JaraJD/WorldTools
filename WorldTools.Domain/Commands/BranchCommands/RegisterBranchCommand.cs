using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Commands.BranchCommands
{
    public class RegisterBranchCommand
    {
        [Required] public string BranchName { get; set; }

        [Required] public string BranchCountry { get; set; }

        [Required] public string BranchCity { get; set; }

    }
}
