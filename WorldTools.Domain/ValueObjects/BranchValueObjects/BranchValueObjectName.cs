using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.BranchValueObjects
{
    public class BranchValueObjectName
    {
        [Required] public string BranchName { get; set; }

        
    }
}
