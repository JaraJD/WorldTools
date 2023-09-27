using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectDescription
    {
        [Required] public string ProductDescription { get; set; }

        
    }
}
