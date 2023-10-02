using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.BranchValueObjects
{
    public class BranchValueObjectLocation
    {
        [Required] public string Country { get; set; }

        [Required] public string City { get; set; }

        public BranchValueObjectLocation(string country, string city)
        {
            Country = country;
            City = city;
        }

        public BranchValueObjectLocation()
        {
            
        }
    }
}
