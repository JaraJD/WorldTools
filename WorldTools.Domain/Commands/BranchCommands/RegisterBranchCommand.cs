using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Domain.Commands.BranchCommands
{
    public class RegisterBranchCommand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchId { get; set; }

        [Required] public string BranchName { get; set; }

        [Required] public string BranchCountry { get; set; }

        [Required] public string BranchCity { get; set; }

    }
}
