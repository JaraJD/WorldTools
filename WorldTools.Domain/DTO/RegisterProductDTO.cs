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
    public class RegisterProductDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required] public string? ProductName { get; set; }
        [Required] public string? ProductDescription { get; set; }
        [Required] public double ProductPrice { get; set; }
        [Required] public int ProductInventoryStock { get; set; }
        [Required] public string? ProductCategory { get; set; }
        [Required] public int BranchId { get; set; }

        [Required]
        [ForeignKey("BranchId")]
        public virtual RegisterBranchDTO Branch { get; set; }
    }
}
