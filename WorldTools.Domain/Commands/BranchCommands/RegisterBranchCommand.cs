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

        public BranchValueObjectName BranchName { get; set; }

        public BranchValueObjectLocation BranchLocation { get; set; }

        public virtual List<ProductEntity> BranchProducts { get; set; }

        public virtual List<UserEntity> BranchEmployees { get; set; }
    }
}
