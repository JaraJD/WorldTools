using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Domain.Entities
{
    public class BranchEntity
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
