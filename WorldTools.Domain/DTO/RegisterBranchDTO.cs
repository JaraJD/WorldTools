using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;

namespace WorldTools.Domain.DTO
{
    public class RegisterBranchDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchId { get; set; }

        [Required] public string BranchName { get; set; }

        [Required] public string BranchCountry { get; set; }

        [Required] public string BranchCity { get; set; }

        public virtual List<RegisterProductDTO> BranchProducts { get; set; }

        public virtual List<RegisterUserDTO> BranchEmployees { get; set; }
    }
}
